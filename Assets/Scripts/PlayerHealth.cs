using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] TextMeshProUGUI healthText;

    void Update()
    {
        DisplayHealth();
    }

    private void DisplayHealth()
    {
        healthText.text = hitPoints.ToString();
    }

    public void TakeDamage(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            UnityEngine.Debug.Log("DEAD");
            GetComponent<DeathHandler>().HandleDeath();
            //respawn menu
        }
    }
}
