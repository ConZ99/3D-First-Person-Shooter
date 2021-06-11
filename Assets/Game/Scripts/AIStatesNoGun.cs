using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatesNoGun : MonoBehaviour
{
    public GameObject player;
    private GameObject target = null;
    public AIMovement movement;
    public Animator animator;

    public float maxLookDist = 20f;
    public float maxMeleeDist = 6f;

    public float meleeRate = 1f;
    private float nextTimeToMelee = 0f;
    private bool isMelee = false;

    public float damage = 15f;

    public AudioSource meleeSound;

    public enum State
    {
        Patrol,
        AttackMelee
    };
    public State currentState;
    public State lastState;

    private void Awake()
    {
        int difficulty = PlayerPrefs.GetInt("difficulty", 2);
        if (difficulty == 1)
        {
            damage = 15f;
        }
        else if (difficulty == 2)
        {
            damage = 25f;
        }
        else if (difficulty == 3)
        {
            damage = 45f;
        }
    }

    private void Start()
    {
        currentState = State.Patrol;
    }

    void Update()
    {
        if (PauseMenu.isPaused || PauseMenu.inStory)
            return;

        UpdateState();
        if (currentState == State.AttackMelee)
        {
            LookAtTarget();

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= maxMeleeDist)
            {
                movement.StopMoving();

                if (Time.time > nextTimeToMelee && isMelee == false)
                {
                    isMelee = true;
                    nextTimeToMelee = Time.time + (1f / meleeRate);
                    StartCoroutine(Melee());
                }
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
    }

    IEnumerator Melee()
    {
        animator.SetBool("Melee", true);
        meleeSound.Play();

        RaycastHit hit_obj;
        Vector3 direction = (target.transform.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, direction, out hit_obj, 100))
        {
            Target target = hit_obj.transform.GetComponent<Target>();
            if (target != null)
                target.TakeDamage(damage);
        }

        yield return new WaitForSeconds(1f);
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
        }
        else
        {
            currentState = State.AttackMelee;
        }
    }
}
