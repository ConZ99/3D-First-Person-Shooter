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
    }

    public void Load()
    {
        pistol.totalAmmo = PlayerPrefs.GetInt("pistol_ammo");
        pistol.currentAmmo = PlayerPrefs.GetInt("pistol_current_ammo");

        ak.totalAmmo = PlayerPrefs.GetInt("ak_ammo");
        ak.currentAmmo = PlayerPrefs.GetInt("ak_current_ammo");

        shotgun.totalAmmo = PlayerPrefs.GetInt("shotgun_ammo");
        shotgun.currentAmmo = PlayerPrefs.GetInt("shotgun_current_ammo");

        sniper.totalAmmo = PlayerPrefs.GetInt("sniper_ammo");
        sniper.currentAmmo = PlayerPrefs.GetInt("sniper_current_ammo");

        playerStats.armor = PlayerPrefs.GetFloat("armor");
    }

    public void Delete()
    {
        PlayerPrefs.DeleteKey("pistol_ammo");
        PlayerPrefs.DeleteKey("pistol_cartidge");

        PlayerPrefs.DeleteKey("ak_ammo");
        PlayerPrefs.DeleteKey("ak_cartidge");

        PlayerPrefs.DeleteKey("shotgun_ammo");
        PlayerPrefs.DeleteKey("shotgun_cartidge");

        PlayerPrefs.DeleteKey("sniper_ammo");
        PlayerPrefs.DeleteKey("sniper_cartidge");

        PlayerPrefs.DeleteKey("armor");
    }
}
