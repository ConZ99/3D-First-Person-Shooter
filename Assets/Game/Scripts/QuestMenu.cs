using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject questMenu;
    //public TextMeshProUGUI storyText;

    private void Start()
    {
        questMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //switch case pt nivele
        if (Input.GetKeyDown("k"))
        {
            if (isPaused == false)
            {
                questMenu.SetActive(true);
                isPaused = true;
            }
            else
            {
                questMenu.SetActive(false);
                isPaused = false;
            }
        }
    }
}
