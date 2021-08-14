using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] Button StartButton;

    // Setting the first button to be highlighted for PS4 controller selection through UI Elements
    private void Start()
    {
        if (StartButton)
        {
            StartButton.Select();
        }
    }





    // Functioins below are one for each button individually

    public void onStartClicked()
    {
        Debug.Log("Start Clicked");
        SceneManager.LoadScene("GameScene");
    }

    public void onOptionsClicked()
    {
        Debug.Log("Options CLicked");
        UiManager.instance.SetUIState(UIState.OptionsMenu);

    }

    public void onQuitClicked()
    {
        Debug.Log("Quit Clicked");
        Application.Quit();
    }


}
