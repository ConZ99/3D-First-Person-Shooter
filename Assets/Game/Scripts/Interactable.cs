﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public bool follow = false;
    public GameObject target = null;
    public NavMeshAgent nav;
    public Transform currentWaypoint = null;
    public AudioSource walkingSound;
    public bool story = false;
    public static bool isPaused = false;
    private GameObject[] qtItems;
    GameObject storyText;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        qtItems = GameObject.FindGameObjectsWithTag("Story");
        if (qtItems.Length == 1){
            storyText = qtItems[0];
            storyText.SetActive(false);
        }
        Debug.Log(qtItems.Length);
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        if (follow)
        {
            LookAtTarget();

            float distance = Vector3.Distance(target.transform.position, transform.position);
            if (distance <= 3f)
            {
                StopMoving();
            }
            else if (distance > 3f)
                MoveToTarget(target);
        }

        StoryCheck();
    }

    void pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    void resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        story = false;
        storyText.SetActive(false);
    }

    void StoryCheck()
    {
        if (story)
        {
            StoryShow();
            if (isPaused == false){
                PauseMenu.inStory = true;
                pause();
            }
            else
                if (Input.GetKeyDown(KeyCode.Return)){
                    PauseMenu.inStory = false;
                    resume();
                }
        }
    }

    void StoryShow()
    {
        if (qtItems.Length != 0){
            
            storyText.SetActive(true);
            TextMeshProUGUI mText = storyText.GetComponent<TextMeshProUGUI>();
            int level = SceneManager.GetActiveScene().buildIndex;
            switch (level)
            {
                case 0:
                    mText.text = "Level 0";
                    break;
                case 1:
                    mText.text = "Level 1";
                    break;
                case 2:
                    mText.text = "Level 2";
                    break;
                case 3:
                    mText.text = "Level 3";
                    break;
                case 4:
                    mText.text = "Level 4";
                    break;
                default:
                    mText.text = "Level 5";
                    break;
            }
        }        
    }

    public void MoveToTarget(GameObject target)
    {
        currentWaypoint = target.transform;
        nav.destination = currentWaypoint.position;
        nav.stoppingDistance = 1.2f; 
        nav.isStopped = false;

        //if (!walkingSound.isPlaying)
            //walkingSound.Play();
    }

    public void StopMoving()
    {
        currentWaypoint = null;
        nav.isStopped = true;

        //walkingSound.Stop();
    }

    void LookAtTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        direction.y = 0;
        Quaternion rotation_body = Quaternion.LookRotation(direction);

        transform.rotation = rotation_body;
    }

    public void InteractDoor()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("merge!!!");
    }
}
