using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform[] waypoints;
    public Transform currentWaypoint = null;
    public Animator animator;
    public float stopDist = 1f;

    public AudioSource walkingSound;

    public void Update()
    {
        if (PauseMenu.isPaused || PauseMenu.inStory)
        {
            walkingSound.Stop();
            return;
        }
    }

    public void Start()
    {
        currentWaypoint = null;
    }

    public void Patrol()
    {
        if (currentWaypoint == null || Vector3.Distance(transform.position, currentWaypoint.position) <= 1f)
        {
            animator.SetBool("LookTarget", false);
            int index = Random.Range(0, waypoints.Length);
            currentWaypoint = waypoints[index];
            navAgent.destination = currentWaypoint.position;
            navAgent.stoppingDistance = 1f;
            navAgent.isStopped = false;
            animator.SetBool("Forward", true);

            if (!walkingSound.isPlaying)
                walkingSound.Play();
        }
    }

    public void MoveToTarget(GameObject target)
    {
        animator.SetBool("LookTarget", false);
        currentWaypoint = target.transform;
        navAgent.destination = currentWaypoint.position;
        navAgent.stoppingDistance = stopDist; 
        navAgent.isStopped = false;
        animator.SetBool("Forward", true);

        if (!walkingSound.isPlaying)
            walkingSound.Play();
    }

    public void StopMoving()
    {
        currentWaypoint = null;
        navAgent.isStopped = true;
        animator.SetBool("Forward", false);

        walkingSound.Stop();
    }
}
