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

    public void Patrol()
    {
        Debug.Log(currentWaypoint);
        if (currentWaypoint == null || Vector3.Distance(transform.position, currentWaypoint.position) < 1f)
        {
            int index = Random.Range(0, waypoints.Length);
            currentWaypoint = waypoints[index];
            navAgent.destination = currentWaypoint.position;
            navAgent.stoppingDistance = 1f;
            navAgent.isStopped = false;
            animator.SetBool("Forward", true);
        }
    }

    public void MoveToTarget(GameObject target)
    {
        currentWaypoint = target.transform;
        navAgent.destination = currentWaypoint.position;
        navAgent.stoppingDistance = 1f; 
        navAgent.isStopped = false;
        animator.SetBool("Forward", true);

    }

    public void StopMoving()
    {
        currentWaypoint = null;
        navAgent.isStopped = true;
        animator.SetBool("Forward", false);
    }
}
