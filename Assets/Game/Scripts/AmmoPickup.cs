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

    public DisplayInfo displayInf;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ammo"))
        {
            GameObject ammoObj = col.gameObject;
            AmmoObject ammo = ammoObj.GetComponent<AmmoObject>();
            
            if (ammo.pistolAmmo > 0)
            {
                pistol.totalAmmo += ammo.pistolAmmo;
                displayInf.addMsg("Added " + ammo.pistolAmmo + " pistol ammo.");

                pistol.isEnabled = 1;
                switchScript.weaponEnabled[0] = true;
            }
            else if (ammo.rifleAmmo > 0)
            {
                rifle.totalAmmo += ammo.rifleAmmo;
                displayInf.addMsg("Added " + ammo.rifleAmmo + " rifle ammo.");

                rifle.isEnabled = 1;
                switchScript.weaponEnabled[1] = true;
            }
            else if (ammo.shotgunAmmo > 0)
            {
                shotgun.totalAmmo += ammo.shotgunAmmo;
                displayInf.addMsg("Added " + ammo.shotgunAmmo + " shotgun ammo.");

                shotgun.isEnabled = 1;
                switchScript.weaponEnabled[2] = true;
            }
            else if (ammo.sniperAmmo > 0)
            {
                sniper.totalAmmo += ammo.sniperAmmo;
                displayInf.addMsg("Added " + ammo.sniperAmmo + " sniper ammo.");

                sniper.isEnabled = 1;
                switchScript.weaponEnabled[3] = true;
            }
            else if (ammo.armor > 0)
            {
                statsScript.armor += ammo.armor;
                displayInf.addMsg("Added " + ammo.armor + " armor.");
            }
            
            Destroy(ammoObj);
        }
    }
}
