using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public GunPistol pistol;
    public GunAK ak;
    public GunShotgun shotgun;
    public GunSniper sniper;

    public Target playerStats;

    public WeaponSwitch switchScript;

    public bool loadOnStart;

    public void Awake()
    {
        if (loadOnStart)
            Load();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("pistol_ammo", pistol.totalAmmo);
        PlayerPrefs.SetInt("pistol_current_ammo", pistol.currentAmmo);

        PlayerPrefs.SetInt("ak_ammo", ak.totalAmmo);
        PlayerPrefs.SetInt("ak_current_ammo", ak.currentAmmo);

        PlayerPrefs.SetInt("shotgun_ammo", shotgun.totalAmmo);
        PlayerPrefs.SetInt("shotgun_current_ammo", shotgun.currentAmmo);

        PlayerPrefs.SetInt("sniper_ammo", sniper.totalAmmo);
        PlayerPrefs.SetInt("sniper_current_ammo", sniper.currentAmmo);

        PlayerPrefs.SetFloat("armor", playerStats.armor);

        PlayerPrefs.SetInt("pistol_enabled", pistol.isEnabled);
        PlayerPrefs.SetInt("ak_enabled", ak.isEnabled);
        PlayerPrefs.SetInt("shotgun_enabled", shotgun.isEnabled);
        PlayerPrefs.SetInt("sniper_enabled", sniper.isEnabled);

        PlayerPrefs.SetInt("current_weapon", switchScript.weaponNumber);

        Debug.Log(PlayerPrefs.GetInt("pistol_enabled") + " " + PlayerPrefs.GetInt("ak_enabled") + " " + PlayerPrefs.GetInt("shotgun_enabled") + " " + PlayerPrefs.GetInt("sniper_enabled"));
    }

    public void Load()
    {
        pistol.totalAmmo = PlayerPrefs.GetInt("pistol_ammo", pistol.totalAmmo);
        pistol.currentAmmo = PlayerPrefs.GetInt("pistol_current_ammo", pistol.currentAmmo);

        ak.totalAmmo = PlayerPrefs.GetInt("ak_ammo", ak.totalAmmo);
        ak.currentAmmo = PlayerPrefs.GetInt("ak_current_ammo", ak.currentAmmo);

        shotgun.totalAmmo = PlayerPrefs.GetInt("shotgun_ammo", shotgun.totalAmmo);
        shotgun.currentAmmo = PlayerPrefs.GetInt("shotgun_current_ammo", shotgun.currentAmmo);

        sniper.totalAmmo = PlayerPrefs.GetInt("sniper_ammo", sniper.totalAmmo);
        sniper.currentAmmo = PlayerPrefs.GetInt("sniper_current_ammo", sniper.currentAmmo);

        playerStats.armor = PlayerPrefs.GetFloat("armor", playerStats.armor);

        pistol.isEnabled = PlayerPrefs.GetInt("pistol_enabled", pistol.isEnabled);
        ak.isEnabled = PlayerPrefs.GetInt("ak_enabled", ak.isEnabled);
        shotgun.isEnabled = PlayerPrefs.GetInt("shotgun_enabled", shotgun.isEnabled);
        sniper.isEnabled = PlayerPrefs.GetInt("sniper_enabled", sniper.isEnabled);

        switchScript.weaponNumber = PlayerPrefs.GetInt("current_weapon", 0);
        switchScript.weaponEnabled[0] = (PlayerPrefs.GetInt("pistol_enabled", 1) == 1) ? true : false;
        switchScript.weaponEnabled[1] = (PlayerPrefs.GetInt("ak_enabled", 1) == 1) ? true : false;
        switchScript.weaponEnabled[2] = (PlayerPrefs.GetInt("shotgun_enabled", 1) == 1) ? true : false;
        switchScript.weaponEnabled[3] = (PlayerPrefs.GetInt("sniper_enabled", 1) == 1) ? true : false;
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        
        /*PlayerPrefs.DeleteKey("pistol_ammo");
        PlayerPrefs.DeleteKey("pistol_cartidge");

        PlayerPrefs.DeleteKey("ak_ammo");
        PlayerPrefs.DeleteKey("ak_cartidge");

        PlayerPrefs.DeleteKey("shotgun_ammo");
        PlayerPrefs.DeleteKey("shotgun_cartidge");

        PlayerPrefs.DeleteKey("sniper_ammo");
        PlayerPrefs.DeleteKey("sniper_cartidge");

        PlayerPrefs.DeleteKey("armor");

        PlayerPrefs.DeleteKey("pistol_enabled");
        PlayerPrefs.DeleteKey("ak_enabled");
        PlayerPrefs.DeleteKey("shotgun_enabled");
        PlayerPrefs.DeleteKey("sniper_enabled");

        PlayerPrefs.DeleteKey("current_weapon");*/
    }
}
