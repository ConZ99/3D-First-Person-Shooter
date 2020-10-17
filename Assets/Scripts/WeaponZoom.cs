using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera FPScamera;
    [SerializeField] float zoomedOutFOV = 60;
    [SerializeField] float zoomedInFOV = 50;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = .5f;

    [SerializeField] RigidbodyFirstPersonController FPScontroller;
    bool zoomToggle = false;

    private void OnDisable()
    {
        ZoomOut();
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomToggle == false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    public void ZoomIn()
    {
        zoomToggle = true;
        FPScamera.fieldOfView = zoomedInFOV;
        FPScontroller.mouseLook.XSensitivity = zoomInSensitivity;
        FPScontroller.mouseLook.YSensitivity = zoomInSensitivity;
    }

    public void ZoomOut()
    {
        zoomToggle = false;
        FPScamera.fieldOfView = zoomedOutFOV;
        FPScontroller.mouseLook.XSensitivity = zoomOutSensitivity;
        FPScontroller.mouseLook.YSensitivity = zoomOutSensitivity;
    }

}
