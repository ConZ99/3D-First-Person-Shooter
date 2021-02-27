using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject[] weapons;
    public Image[] weaponsSign;
    public Image[] weaponsBkg;
    private int weaponNumber = 0;

    private Color tempColor;

    public GunSniper sniperScript;

    void Start()
    {
        weaponNumber = 0;
        UpdateWeapon();
    }

    void Update()
    {
        if (PauseMenu.isPaused == true)
            return;

        if (sniperScript.isAiming == true)
            return;


        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                weaponNumber = weaponNumber - 1;
                if (weaponNumber == -1)
                    weaponNumber = weapons.Length - 1;
                
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                weaponNumber = weaponNumber + 1;
                if (weaponNumber == weapons.Length)
                    weaponNumber = 0;
            }
            
            UpdateWeapon();
        }
    }

    void UpdateWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == weaponNumber)
            {
                weapons[i].SetActive(true);

                tempColor = weaponsSign[i].color;
                tempColor.a = 255f / 255f;
                weaponsSign[i].color = tempColor;
                tempColor = weaponsBkg[i].color;
                tempColor.a = 161f / 255f;
                weaponsBkg[i].color = tempColor;
            }
            else
            {
                weapons[i].SetActive(false);

                tempColor = weaponsSign[i].color;
                tempColor.a = 20f / 255f;
                weaponsSign[i].color = tempColor;
                tempColor = weaponsBkg[i].color;
                tempColor.a = 20f / 255f;
                weaponsBkg[i].color = tempColor;
            }
        }
    }
}
