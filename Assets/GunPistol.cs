using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPistol : MonoBehaviour
{
    public Camera fpsCamera;
    public GameObject player;
    private PlayerUI UI;
    public Animator animator;

    public float damage = 25f;
    public float knifeDamage = 50f;
    public float range = 100f;
    public float knifeRange = 20f;
    public float reloadTime = 2f;
    private bool isReloading = false;
    public int cartidgeCapacity = 7;
    public int totalAmmo = 21;
    private int currentAmmo = 7;

    public ParticleSystem gunFlash;
    public AudioSource fireSound;
    public GameObject impactEffect;
    public GameObject bulletHole;

    public GameObject tacticalKnife;
    public float recoilAmount = 0.01f;

    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    void Awake()
    {
        UI = player.transform.GetComponent<PlayerUI>();
        player.GetComponent<PlayerMovement>().animator = animator;
        isReloading = false;
        tacticalKnife.SetActive(false);
    }

    void OnEnable()
    {
        player.GetComponent<PlayerMovement>().animator = animator;
        isReloading = false;
        tacticalKnife.SetActive(false);
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        UI.DisplayAmmo(currentAmmo, totalAmmo);
        CheckInput();
    }

    private void CheckInput()
    {
        if (isReloading)
            return;
        else if (currentAmmo <= 0 && totalAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }
        else if (Input.GetKeyDown(KeyCode.R) && currentAmmo < cartidgeCapacity && totalAmmo > 0)
        {
            StartCoroutine(Reload());
            return;
        }
        else if (Input.GetButtonDown("Fire1") && currentAmmo > 0 && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + (1f / fireRate);
            Shoot();
            return;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(KnifeAttack());
            return;
        }
    }

    public void Shoot()
    {
        RaycastHit hit_obj;

        gunFlash.Stop();
        gunFlash.Play();
        fireSound.Play();
        currentAmmo--;

        float recoilX = Random.Range(-recoilAmount, recoilAmount);
        float recoilY = Random.Range(-recoilAmount, recoilAmount);
        Vector3 dir = (fpsCamera.transform.forward + new Vector3(recoilX, recoilY, 0)).normalized;

        if (Physics.Raycast(fpsCamera.transform.position, dir, out hit_obj, range))
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

    IEnumerator KnifeAttack()
    {
        animator.SetBool("Melee", true);
        tacticalKnife.SetActive(true);

        RaycastHit hit_obj;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit_obj, knifeRange))
        {
            Target target = hit_obj.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(knifeDamage);
        }

        yield return new WaitForSeconds(1f);
        animator.SetBool("Melee", false);
        tacticalKnife.SetActive(false);
    }
}
