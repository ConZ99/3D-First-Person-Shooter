using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : MonoBehaviour
{
    public float damage = 50f;
    public float rate = 4f;
    private float nextTimeToHit = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextTimeToHit)
            {
                nextTimeToHit = Time.time + (1f / rate);
                other.gameObject.GetComponent<Target>().TakeDamage(damage);
            }
        }
            
    }
}
