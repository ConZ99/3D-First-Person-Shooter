using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public float damage = 50f;
    public float rate = 10f;
    private float nextTimeToHit = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (Time.time >= nextTimeToHit)
        {
            nextTimeToHit = Time.time + (1f / rate);
            Debug.Log(other.gameObject);
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Target>().TakeDamage(50);
            }
        }
    }
}
