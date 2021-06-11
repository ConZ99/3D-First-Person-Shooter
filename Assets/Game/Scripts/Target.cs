using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject ragDoll;
    public GameObject ammo;
    public float health = 100f;
    public float armor = 100f;

    private bool isPlayer = false;
    private float timer = 0;
    private float timeToRecover = 5f;

    private void Awake()
    {
        if (this.CompareTag("Player") != true)
        {
            int difficulty = PlayerPrefs.GetInt("difficulty", 2);
            if (difficulty == 1)
            {
                health = 100f;
            }
            else if (difficulty == 2)
            {
                health = 120f;
            }
            else if (difficulty == 3)
            {
                health = 150f;
            }
        }
    }

    private void Start()
    {
        if (this.CompareTag("Player") == true)
            isPlayer = true;
    }

    void Update()
    {
        if (PauseMenu.isPaused || PauseMenu.inStory)
            return;

        SetLimit();

        if (isPlayer)
            RecoverHealth();
    }

    void SetLimit()
    {
        if (armor < 0)
            armor = 0;
        else if (armor > 100)
            armor = 100;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
        else if (health > 100)
            health = 100;
    }

    public void TakeDamage(float damage)
    {
        if(armor > 0)
        {
            armor -= Mathf.RoundToInt(75f / 100f * damage);
            health -= Mathf.RoundToInt(25f / 100f * damage);
        }
        else
            health -= damage;

        if (isPlayer)
            timer = 0;
    }

    void RecoverHealth()
    {
        timer += Time.deltaTime;
        if (timer > timeToRecover && health < 100)
        {
            int amount = (int)(timer - timeToRecover);
            timer -= amount;
            health += 5 * amount;
            if (health > 100)
                health = 100;
        }
    }

    void Die()
    {
        if (isPlayer)
            return;

        GameObject ragObj = Instantiate(ragDoll, transform.position, transform.rotation);
        Instantiate(ammo, transform.position, transform.rotation);
        Destroy(ragObj, 30f);
        Destroy(gameObject);
    }
}
