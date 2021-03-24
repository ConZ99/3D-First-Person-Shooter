using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Camera cam;
    public float mouseSensitivity = 2f;
    public Transform playerBody;
    private float xRotation = 0f;
    private GameObject[] qtItems;
    private int totalQtItemsNumber = 0;

    public Interactable focus;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (PauseMenu.isPaused == true)
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        Interact();
    }

    private void Interact()
    {
        if (Input.GetKeyDown("f"))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 2))
            {
                Interactable inter = hit.collider.GetComponent<Interactable>();
                if(inter != null)
                {
                    if (inter.transform.gameObject.tag == "StoryQuest")
                    {
                        inter.story = true;
                    }
                    else if(inter.transform.gameObject.tag == "Quest")
                    {
                        SetFocus(inter);
                        inter.follow = true;
                        inter.story = true;
                        GameObject door = GameObject.Find("Door");
                        inter.transform.gameObject.tag = "StoryQuest";
                        //fa cumva ca sa dai enable la iconita o data ce ai completat questul local.                        
                    }
                    else if(inter.transform.gameObject.tag == "Door")
                    {
                        
                        qtItems = GameObject.FindGameObjectsWithTag("Quest");
                        totalQtItemsNumber = qtItems.Length;
                        if (totalQtItemsNumber == 0)
                        {
                            SetFocus(inter);
                            inter.InteractDoor();
                        }
                        inter.transform.gameObject.tag = "Untagged";
                    }
                    
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }
}
