using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public GunPistol pistol;
    public GunAK rifle;
    public GunShotgun shotgun;
    public GunSniper sniper;
    public Target statsScript;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ammo"))
        {
            GameObject ammoObj = col.gameObject;
            AmmoObject ammo = ammoObj.GetComponent<AmmoObject>();

            pistol.totalAmmo += ammo.pistolAmmo;
            rifle.totalAmmo += ammo.rifleAmmo;
            shotgun.totalAmmo += ammo.shotgunAmmo;
            sniper.totalAmmo += ammo.sniperAmmo;
            statsScript.armor += ammo.armor;

            Destroy(ammoObj);
        }
    }
}
