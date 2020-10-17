using System.Threading;
using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool paused_game = false;
    public GameObject PauseMenuUI;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused_game)
            {
                Resume();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Pause();
            }
        }
    }

    public void LoadMenu()
    {
        paused_game = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        paused_game = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        paused_game = false;
        Application.Quit();
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        paused_game = false;
        Time.timeScale = 1f;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);
        paused_game = true;
    }
}
