using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float radius = 30f;
    public float damage = 500f;
    public bool triggered = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Explode()
    {
        triggered = true;
        Collider[] hit_objs = Physics.OverlapSphere(transform.position, radius);

        foreach (var hit_obj in hit_objs)
        {
            Target target = hit_obj.transform.GetComponent<Target>();

            Transform root_obj = hit_obj.transform.root;
            Target root_target = (root_obj).transform.GetComponent<Target>();

            ExplosiveBarrel barr = hit_obj.transform.GetComponent<ExplosiveBarrel>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }
            else if (root_target != null) {
                root_target.TakeDamage(damage);
            }
            else if (barr != null && barr.transform.gameObject != transform.gameObject && barr.triggered != true)
            {
                barr.triggered = true;
                barr.Explode();
            }

            //Transform root_obj = hit_obj.transform.root;
            //Debug.Log(hit_obj.transform.gameObject);
        }

        Destroy(this.gameObject);
    }
}
