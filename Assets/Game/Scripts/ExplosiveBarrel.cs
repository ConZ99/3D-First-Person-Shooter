using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : MonoBehaviour
{
    public float radius = 30f;
    public float damage = 300f;
    public bool triggered = false;

    public AudioSource explosion_sound;
    public GameObject explosion_obj;
    
    public IEnumerator Explode()
    {
        explosion_sound.Play();
        triggered = true;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;

        Object exp = Instantiate(explosion_obj, transform.position, transform.rotation);
        Destroy(exp, 10f);

        Collider[] hit_objs = Physics.OverlapSphere(transform.position, radius);
        foreach (var hit_obj in hit_objs)
        {
            Target target = hit_obj.transform.GetComponent<Target>();

            Transform root_obj = hit_obj.transform.root;
            Target root_target = (root_obj).transform.GetComponent<Target>();

            ExplosiveBarrel barr = hit_obj.transform.GetComponent<ExplosiveBarrel>();

            if (target != null)
            {
                float dist = Vector3.Distance(transform.position, target.transform.position);
                target.TakeDamage(damage * (1 - dist / radius));
            }
            else if (root_target != null)
            {
                float dist = Vector3.Distance(transform.position, root_target.transform.position);
                root_target.TakeDamage(damage * (1 - dist / radius));
            }
            else if (barr != null && barr.transform.gameObject != transform.gameObject && barr.triggered != true)
            {
                barr.triggered = true;
                StartCoroutine(barr.Explode());
            }
        }

        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
}
