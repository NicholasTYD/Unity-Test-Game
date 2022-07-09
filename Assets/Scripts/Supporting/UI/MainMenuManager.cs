using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject ControlsMenu;

    public void NewGame()
    {
        GameManager.Instance.StartNewGame();
    }

    public void LoadGame()
    {
        GameManager.Instance.ResumeGame();
    }

    public void ShowControls()
    {
        MainMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }

    public void Back()
    {
        ControlsMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
