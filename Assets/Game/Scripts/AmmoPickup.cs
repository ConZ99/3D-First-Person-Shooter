using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public GunPistol pistol;
    public GunAK rifle;

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ammo"))
        {
            GameObject ammoObj = col.gameObject;
            AmmoObject ammo = ammoObj.GetComponent<AmmoObject>();

            if (ammo.pistolAmmo > 0)
                pistol.totalAmmo += ammo.pistolAmmo;
            else
                rifle.totalAmmo += ammo.rifleAmmo;

            Destroy(ammoObj);
        }
    }
}
