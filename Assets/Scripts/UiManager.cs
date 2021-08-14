using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIState
{
    MainMenu,
    OptionsMenu
}

[System.Serializable]
public struct UIStateStruct
{
    public UIState m_UiState;
    public GameObject m_uiObject;
}


public class UiManager : MonoBehaviour
{

    public static UiManager instance = null;
    UIState m_currentState = UIState.MainMenu;

    [SerializeField] List<UIStateStruct> m_uiStates = new List<UIStateStruct>();
    Dictionary<UIState, GameObject> m_UIStateMap = new Dictionary<UIState, GameObject>();

   
  
    void Awake()
    {
       if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
       // fill up dictionary
       foreach( UIStateStruct uistateStruct in m_uiStates)
        {
            m_UIStateMap.Add(uistateStruct.m_UiState, uistateStruct.m_uiObject);
        }
    }

    public void SetUIState(UIState newState)
    {
        m_UIStateMap[m_currentState].SetActive(false);
        m_UIStateMap[newState].SetActive(true);

        m_currentState = newState;
    }
}
