using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool inStory = false;
    public GameObject pauseMenu;

    public GameObject resumeButton;
    public GameObject winText;
    public GameObject looseText;

    private void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(!inStory)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused == false)
                    Pause();
                else
                    Resume();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        resumeButton.SetActive(true);
        winText.SetActive(false);
        looseText.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void GameWin()
    {
        pauseMenu.SetActive(true);
        resumeButton.SetActive(false);
        winText.SetActive(true);
        looseText.SetActive(false);

        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void GameLost()
    {
        pauseMenu.SetActive(true);
        resumeButton.SetActive(false);
        winText.SetActive(false);
        looseText.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ReturnToMenu()
    {
        PlayerPrefs.DeleteAll();

        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteAll();

        Application.Quit();
    }
}
