using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;


// list of languages
public enum Languages
{
    English,
    Chinese
}

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance = null;
    
    Dictionary<Languages, TextAsset> m_LocalizationFiles = new Dictionary<Languages, TextAsset>();
    Dictionary<string, string> m_LocalizationData = new Dictionary<string, string>();


    [SerializeField] Languages m_CurrentLanguage = Languages.English;



    void Awake()
    {
        // apply singleton pattern
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SetupLocalizationFiles(); // collect and organize localization files
        SetUpLocalizationData();
        DontDestroyOnLoad(gameObject); // this instance of gameobject is the original for singleton pattern
    }



    // collect and organize localized files
    public void SetupLocalizationFiles()                                                     
    {                                          
        foreach(Languages language in Languages.GetValues(typeof(Languages))) // traverse through enum and do the following below  VVVV
        {
            string textAssetPath = "Localization/" + language; // store the resource folder plus the language I.D for correct XML file in folder
            TextAsset textAsset = (TextAsset)Resources.Load(textAssetPath); // load our path and I.D

            if (textAsset)
            {
                m_LocalizationFiles[language] = textAsset; // assign the correct XML file to each key in our Dictionary
                Debug.Log("Text Asset:" + textAsset.name);
                
            }
            else
            {
                Debug.Log("Text Asset Not found" + textAssetPath);
            }
        }
    }
    
    
    //Pin Point the data we want to retrieve
    public void SetUpLocalizationData()
    {
        TextAsset textAsset;

        if (m_LocalizationFiles.ContainsKey(m_CurrentLanguage))
        {
            Debug.Log("Select language found: " + m_CurrentLanguage);
            textAsset = m_LocalizationFiles[m_CurrentLanguage];
        }
        else
        {
            Debug.Log("Couldn't finad localization file for: " + m_CurrentLanguage);
            textAsset = m_LocalizationFiles[Languages.English];
        }

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textAsset.text);

        XmlNodeList entrylist = xmlDocument.GetElementsByTagName("Entry");

        foreach(XmlNode entry in entrylist)
        {
            if (!m_LocalizationData.ContainsKey(entry.FirstChild.InnerText)) // checks for the text within the entry brackets in our xml file if it doesn't already exist
            {
                Debug.Log("Add key" + entry.FirstChild.InnerText + "with value" + entry.LastChild.InnerText);
                m_LocalizationData.Add(entry.FirstChild.InnerText, entry.LastChild.InnerText);
            }
            else // if the key has already been added
            {
                Debug.Log("Duplicate found for:" + entry.FirstChild.InnerText);
            }
        }
       
    }



    // Retrieve the Key and Value in xml document
    public string GetLocalizedString(string key)
    {
        string localizedString = "";

        if (!m_LocalizationData.TryGetValue(key, out localizedString))
        {
            localizedString = key;
        }

        return localizedString;
    }
}
