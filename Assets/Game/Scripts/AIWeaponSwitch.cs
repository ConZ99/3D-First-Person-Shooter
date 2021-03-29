using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWeaponSwitch : MonoBehaviour
{
    public Animator animator;
    public AiStates controller;
    public GameObject[] weapons;
    private AIGun[] weaponsScript;
    private float weaponNumber = 0;

    void Start()
    {
        weaponNumber = 0;
        DisplayWeapon();
    }

    void Update()
    {
        if (PauseMenu.isPaused || PauseMenu.inStory)
            return;

        CheckWeapons();
        DisplayWeapon();
    }

    void CheckWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            AIGun gun = weapons[i].GetComponent<AIGun>();
            if (gun.totalAmmo > 0 || gun.currentAmmo > 0)
            {
                weaponNumber = i;
                break;
            }
        }
    }

    void DisplayWeapon()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == weaponNumber)
            {
                weapons[i].SetActive(true);
                controller.gunObj = weapons[i];
            }
            else
                weapons[i].SetActive(false);
        }
    }
}
