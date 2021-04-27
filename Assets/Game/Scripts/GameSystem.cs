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

    private GameObject[] qtItems;
    private int totalQtItemsNumber = 0;
    private int qtItemsNumber = 0;

    public PauseMenu pauseMenu;

    void Awake()
    {
        playerStats = player.GetComponent<Target>();
        playerUI = player.GetComponent<PlayerUI>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemiesNumber = enemies.Length;
        enemiesNumber = 0;

        qtItems = GameObject.FindGameObjectsWithTag("Quest");
        totalQtItemsNumber = qtItems.Length;
        qtItemsNumber = 0;
    }

    void Update()
    {
        if (PauseMenu.isPaused || PauseMenu.inStory)
            return;

        CheckEnd();
        playerUI.DisplayEnemiesCounter(enemiesNumber, totalEnemiesNumber);
        playerUI.DisplayQuestCounter(qtItemsNumber, totalQtItemsNumber);
    }

    void CheckEnd()
    {
        if (playerStats.health <= 0)
        {
            playerUI.DisplayHealth();
            pauseMenu.GameLost();
        }
        /*else
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesNumber = enemies.Length;

            qtItems = GameObject.FindGameObjectsWithTag("Quest");
            qtItemsNumber = qtItems.Length;
            //if (qtItemsNumber == 0)
                //pauseMenu.GameWin();
        }*/
    }
}
