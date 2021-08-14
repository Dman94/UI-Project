using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LocKey : MonoBehaviour
{
    [SerializeField] string m_LocKey;

    Text m_UIText;


  // Print out the text on to the UI from the file
    void Start()
    {
        m_UIText = GetComponent<Text>();

        if(m_UIText && m_LocKey != "")
        {
            m_UIText.text = LocalizationManager.instance.GetLocalizedString(m_LocKey); //Display the text in XML file in UI text
        }
    }

   
}
