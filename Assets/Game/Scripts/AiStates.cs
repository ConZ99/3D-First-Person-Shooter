using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStates : MonoBehaviour
{
    public GameObject gunObj;
    public GameObject player;
    private GameObject target = null;
    public AIMovement movement;
    public Animator animator;

    public float maxLookDist = 20f;
    public float maxAttackDist = 10f;
    public float maxMeleeDist = 1.5f;

    public float fireRate = 1f;
    private float nextTimeToFire = 0f;
    private bool isShooting = false;

    public float meleeRate = 1f;
    private float nextTimeToMelee = 0f;
    private bool isMelee = false;

    public AudioSource meleeSound;

    public enum State
    {
        Patrol,
        AttackGun,
        AttackMelee
    };
    public State currentState;
    public State lastState;

    private void Start()
    {
        currentState = State.Patrol;
    }

    void Update()
    {
        if (PauseMenu.isPaused || PauseMenu.inStory)
            return;

        UpdateState();
        if (currentState == State.AttackGun)
        {
            LookAtTarget();

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= maxAttackDist)
            {
                movement.StopMoving();
                if (Time.time >= nextTimeToFire && isShooting == false)
                {
                    isShooting = true;
                    nextTimeToFire = Time.time + (1f / fireRate);
                    StartCoroutine(Shoot());
                }
            }
            else if (distance > maxAttackDist)
                movement.MoveToTarget(target);
        }
        else if (currentState == State.AttackMelee)
        {
            LookAtTarget();

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= maxMeleeDist && Time.time >= nextTimeToMelee && isMelee == false)
            {
                movement.StopMoving();
                isMelee = true;
                nextTimeToMelee = Time.time + (1f / meleeRate);
                StartCoroutine(Melee());
            }
            else if (distance > maxMeleeDist)
                movement.MoveToTarget(target);
        }
        else if (currentState == State.Patrol)
        {
            if (lastState != State.Patrol)
                movement.currentWaypoint = null;
            movement.Patrol();
        }
    }

    void LookAtTarget()
    {

        animator.SetBool("LookTarget", true);
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion rotation_gun = Quaternion.LookRotation(direction);
        direction.y = 0;
        Quaternion rotation_body = Quaternion.LookRotation(direction);

        transform.rotation = rotation_body;
        gunObj.transform.rotation = rotation_gun;
    }

    IEnumerator Shoot()
    {
        AIGun gun = gunObj.GetComponent<AIGun>();
        gun.Shoot(target);
        yield return new WaitForSeconds(0.2f);
        isShooting = false;
    }

    IEnumerator Melee()
    {
        animator.SetBool("Melee", true);
        meleeSound.Play();

        RaycastHit hit_obj;
        if (Physics.Raycast(transform.position, transform.forward, out hit_obj, 100))
        {
            Target target = hit_obj.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(10f);
        }

        yield return new WaitForSeconds(2f);
        isMelee = false;

        animator.SetBool("Melee", false);
        meleeSound.Stop();
    }

    void UpdateState()
    {
        lastState = currentState;
        if (Vector3.Distance(player.transform.position, transform.position) <= maxLookDist)
            target = player;
        else
            target = null;

        if (target == null)
        {
            currentState = State.Patrol;
            gunObj.SetActive(true);
        }
        else if (gunObj.GetComponent<AIGun>().totalAmmo <= 0 && gunObj.GetComponent<AIGun>().currentAmmo <= 0)
        {
            currentState = State.AttackMelee;
            gunObj.SetActive(false);
        }
        else if (Vector3.Distance(target.transform.position, transform.position) <= maxMeleeDist)
        {
            currentState = State.AttackMelee;
            gunObj.SetActive(false);
        }
        else
        {
            currentState = State.AttackGun;
            gunObj.SetActive(true);
        }
    }
}
