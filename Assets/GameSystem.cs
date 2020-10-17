using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    public GameObject player;
    private Target playerStats;
    private PlayerUI playerUI;
    private GameObject[] enemies;
    private int totalEnemiesNumber = 0;
    private int enemiesNumber = 0;
    public PauseMenu pauseMenu;

    void Awake()
    {
        playerStats = player.GetComponent<Target>();
        playerUI = player.GetComponent<PlayerUI>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemiesNumber = enemies.Length;
        enemiesNumber = 0;
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        CheckEnd();
        playerUI.DisplayEnemiesCounter(enemiesNumber, totalEnemiesNumber);
    }

    void CheckEnd()
    {
        if (playerStats.health <= 0)
        {
            playerUI.DisplayHealth();
            pauseMenu.GameLost();
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesNumber = enemies.Length;
            if (enemiesNumber == 0)
                pauseMenu.GameWin();
        }
    }
}
