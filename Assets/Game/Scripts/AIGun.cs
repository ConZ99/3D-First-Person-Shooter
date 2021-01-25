using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGun : MonoBehaviour
{
    public Animator animator;

    public float damage = 25f;
    public float range = 100f;
    public int cartidgeCapacity = 7;
    public int totalAmmo = 21;
    public int currentAmmo = 7;
    public float reloadTime = 3f;
    private bool isReloading = false;

    public ParticleSystem gunFlash;
    public AudioSource fireSound;
    public GameObject bulletHole;
    public GameObject impactEffect;

    public float knifeDamage = 50f;

    private void OnEnable()
    {
        isReloading = false;
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        if (isReloading)
            return;
        else if (currentAmmo <= 0 && totalAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);

        if (totalAmmo < cartidgeCapacity - currentAmmo)
        {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }
        else
        {
            totalAmmo -= (cartidgeCapacity - currentAmmo);
            currentAmmo = cartidgeCapacity;
        }

        isReloading = false;
        animator.SetBool("Reloading", false);
    }


    public void Shoot(GameObject enemy)
    {
        if (isReloading || currentAmmo < 0)
            return;

        gunFlash.Stop();
        gunFlash.Play();
        fireSound.Play();
        currentAmmo--;

        Vector3 dir = (enemy.transform.position - transform.position).normalized;
        RaycastHit hit_obj;
        if (Physics.Raycast(transform.position, dir, out hit_obj, range))
        {
            Target target = hit_obj.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(damage);

            if (hit_obj.transform.CompareTag("Environment"))
            {
                Vector3 holePosition = hit_obj.point + 0.011f * hit_obj.normal;
                Quaternion holeRortation = Quaternion.FromToRotation(Vector3.up, hit_obj.normal);
                Instantiate(bulletHole, holePosition, holeRortation);

                GameObject impactObj = Instantiate(impactEffect, hit_obj.point, Quaternion.LookRotation(hit_obj.normal));
                Destroy(impactObj, 2f);
            }
        }
    }
}
