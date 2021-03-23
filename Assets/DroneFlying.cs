using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFlying : MonoBehaviour
{
    public Transform[] waypoints;
    private Transform currentWaypoint = null;
    private int currentIndex = 0;

    public float speed = 2f;
    public float stopDist = 1f;


    void Start()
    {
        currentIndex = 0;
        currentWaypoint = waypoints[currentIndex];
    }
    
    void Update()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }

        if (Vector3.Distance(transform.position, currentWaypoint.position) < stopDist)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            currentWaypoint = waypoints[currentIndex];
        }
        else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, step);
            Vector3 direction = (currentWaypoint.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.localRotation, rotation, Time.deltaTime * 10);
        }
    }
}
