using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject mapUI;
    private bool isActive = false;

    void Start()
    {
        mapUI.SetActive(false);
        isActive = false;
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        if (Input.GetKeyDown(KeyCode.M))
        {
            isActive = !isActive;
            mapUI.SetActive(isActive);
        }
    }
}
