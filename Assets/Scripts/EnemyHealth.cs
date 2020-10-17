using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] int enemyType = 0;
    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnHitProvoke");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            UnityEngine.Debug.Log("DEAD");
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true;
        if (enemyType == 0)
        {
            //Collider colider;
            //colider = GameComponent<Collider>();
            gameObject.GetComponent<Collider>().enabled = false;
            GetComponent<Animator>().SetTrigger("Die");
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
