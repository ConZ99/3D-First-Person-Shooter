using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [Header("Grenade Settings")]
	public float grenadeSpawnDelay = 0.35f;
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 35f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;

    private void OnEnable() 
    {
        canShoot = true;
    }

    [System.Serializable]
	public class spawnpoints
	{  
		[Header("Spawnpoints")]
		public Transform grenadeSpawnPoint;
	}
	public spawnpoints Spawnpoints;

    [System.Serializable]
	public class prefabs
	{  
		[Header("Prefabs")]
		public Transform grenadePrefab;
	}
	public prefabs Prefabs;

    void Update()
    {
        DisplayAmmo();
        if(Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {  
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
            UnityEngine.Debug.Log(ammoSlot.GetCurrentAmmo(ammoType));
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.gameObject.GetComponent
                    <EnemyHealth>().TakeDamage(damage);
            }            
            if (hit.transform.tag == "ExplosiveBarrel")
            {
                hit.transform.gameObject.GetComponent
                    <ExplosiveBarrelScript>().explode = true;
            }
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }
    
}
