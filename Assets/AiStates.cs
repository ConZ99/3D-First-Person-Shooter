using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiStates : MonoBehaviour
{
    public GameObject gunObj;
    private GameObject target = null;
    public AIMovement movement;
    public Animator animator;

    public float maxLookDist = 60f;
    public float maxAttackDist = 35f;
    public float maxMeleeDist = 3f;

    public float shotInterval = 0.4f;
    private float shotTime = 0f;
    private bool isShooting = false;

    public float meleeInterval = 1f;
    private float meleeTime = 0f;
    private bool isMelee = false;
    public GameObject knife;

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
        knife.SetActive(false);
        currentState = State.Patrol;
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        UpdateState();
        if (currentState == State.AttackGun)
        {
            LookAtTarget();

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= maxAttackDist && (Time.time - shotTime) > shotInterval && isShooting == false)
            {
                movement.StopMoving();
                isShooting = true;
                StartCoroutine(Shoot());
            }
            else if (distance > maxAttackDist)
                movement.MoveToTarget(target);
        }
        else if (currentState == State.AttackMelee)
        {
            LookAtTarget();

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= maxMeleeDist && (Time.time - meleeTime) > meleeInterval && isMelee == false)
            {
                movement.StopMoving();
                isMelee = true;
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

    public GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        GameObject closest_enemy = null;
        float distance_closest = Mathf.Infinity;
        Vector3 my_position = transform.position;

        if (enemies.Length == 0)
            return null;

        foreach (GameObject enemy in enemies)
        {
            RaycastHit firstObjHit;
            if (Physics.Linecast(gunObj.transform.position, enemy.transform.position, out firstObjHit))
            {
                if (firstObjHit.transform.tag.Equals("Player") == false)
                    continue;
            }

            float current_distance = Vector3.Distance(enemy.transform.position, my_position);
            if (current_distance < distance_closest && current_distance <= maxLookDist)
            {
                closest_enemy = enemy;
                distance_closest = current_distance;
            }
        }

        return closest_enemy;
    }

    void LookAtTarget()
    {
        Vector3 direction = (target.transform.position - gunObj.transform.position + new Vector3(0f, 1f, 0f)).normalized;
        Quaternion rotation_gun = Quaternion.LookRotation(direction);
        direction.y = 0;
        Quaternion rotation_body = Quaternion.LookRotation(direction);

        transform.rotation = rotation_body;
        gunObj.transform.rotation = rotation_gun;
    }

    IEnumerator Shoot()
    {
        AIGun gun = gunObj.GetComponent<AIGun>();
        gun.Shoot();
        yield return new WaitForSeconds(0.2f);
        isShooting = false;
    }

    IEnumerator Melee()
    {
        animator.SetBool("Melee", true);
        knife.SetActive(true);
        gunObj.SetActive(false);

        RaycastHit hit_obj;
        if (Physics.Raycast(gunObj.transform.position, transform.forward, out hit_obj, 100))
        {
            Target target = hit_obj.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(50f);
        }

        yield return new WaitForSeconds(1f);
        animator.SetBool("Melee", false);
        yield return new WaitForSeconds(2f);
        isMelee = false;
        knife.SetActive(false);
        gunObj.SetActive(true);

    }

    void UpdateState()
    {
        lastState = currentState;
        target = FindClosestEnemy();
        if (target == null)
            currentState = State.Patrol;
        else if (gunObj.GetComponent<AIGun>().totalAmmo <= 0 && gunObj.GetComponent<AIGun>().currentAmmo <= 0)
            currentState = State.AttackMelee;
        else
            currentState = State.AttackGun;
    }
}
