using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public bool follow = false;
    public GameObject target = null;
    public NavMeshAgent nav;
    public Transform currentWaypoint = null;
    public AudioSource walkingSound;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        if (follow)
        {
            LookAtTarget();

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= 3f)
            {
                StopMoving();
            }
            else if (distance > 3f)
                MoveToTarget(target);
        }
    }

    public void MoveToTarget(GameObject target)
    {
        currentWaypoint = target.transform;
        nav.destination = currentWaypoint.position;
        nav.stoppingDistance = 1.2f; 
        nav.isStopped = false;

        //if (!walkingSound.isPlaying)
            //walkingSound.Play();
    }

    public void StopMoving()
    {
        currentWaypoint = null;
        nav.isStopped = true;

        //walkingSound.Stop();
    }

    void LookAtTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;
        Quaternion rotation_body = Quaternion.LookRotation(direction);

        transform.rotation = rotation_body;
    }
}
