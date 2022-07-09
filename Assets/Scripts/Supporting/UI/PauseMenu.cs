using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{ 
    [SerializeField] GameObject pauseMenu;
    float prevSlowdownSpeed;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.isGamePaused)
        {
            pauseGame();
        }
    }

    void pauseGame()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.Pause();
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        GameManager.Instance.Unpause();
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.Unpause();
        Time.timeScale = 1;
        GameManager.Instance.BackToMainMenu();
    }
}
