using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Easy()
    {
        PlayerPrefs.SetInt("difficulty", 1);
        SceneManager.LoadScene(1);
    }

    public void Medium()
    {
        PlayerPrefs.SetInt("difficulty", 2);
        SceneManager.LoadScene(1);
    }

    public void Hard()
    {
        PlayerPrefs.SetInt("difficulty", 3);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
