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

        InteractNPC();
    }

    private void InteractNPC()
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
                    SetFocus(inter);
                    inter.follow = true;
                }
            }
        }
    }

    void SetFocus(Interactable newFocus)
    {
        focus = newFocus;
    }
}
