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

    public WeaponSwitch switchScript;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ammo"))
        {
            GameObject ammoObj = col.gameObject;
            AmmoObject ammo = ammoObj.GetComponent<AmmoObject>();

            Debug.Log(ammo.pistolAmmo + " " + ammo.rifleAmmo + " " + ammo.shotgunAmmo + " " + ammo.sniperAmmo + " " + ammo.armor);

            pistol.totalAmmo += ammo.pistolAmmo;
            rifle.totalAmmo += ammo.rifleAmmo;
            shotgun.totalAmmo += ammo.shotgunAmmo;
            sniper.totalAmmo += ammo.sniperAmmo;
            statsScript.armor += ammo.armor;

            Destroy(ammoObj);
        }

        if (pistol.totalAmmo > 0)
        {
            Debug.Log("enable pistol");
            pistol.isEnabled = 1;
            switchScript.weaponEnabled[0] = true;
        }
        if (rifle.totalAmmo > 0)
        {
            Debug.Log("rifle pistol");
            rifle.isEnabled = 1;
            switchScript.weaponEnabled[1] = true;
        }
        if (shotgun.totalAmmo > 0)
        {
            Debug.Log("shotgun pistol");
            shotgun.isEnabled = 1;
            switchScript.weaponEnabled[2] = true;
        }
        if (sniper.totalAmmo > 0)
        {
            Debug.Log("sniper pistol");
            sniper.isEnabled = 1;
            switchScript.weaponEnabled[3] = true;
        }
    }
}
