using System.Collections;
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
    private GameObject[] Tools;
    GameObject tools;

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
            //storyText.SetActive(false);
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
        if (!isPaused)
            storyText.SetActive(false);
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
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            Tools = GameObject.FindGameObjectsWithTag("Tools");
            if (Tools.Length == 1){
                tools = Tools[0];
            }
            tools.SetActive(false);
        }
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
                case 1:
                    mText.text = "Hello, hey!\nOh thank god!\nSomeone alive and breathing! everyone was taken! We have to get out.\nMy van broke down and I cant fix it, we need tools and someone to help us with fixing it.\nYou have to go to the city and find the mechanic at his shop, be careful though, I've heard gunshots coming from there. I think that those terrorists are still around here somewhere.";
                    break;
                case 2:
                    mText.text = "Hmm.. there's no one here.\nThis note says that the mechanic left his shop along with his tools and took refuge in his summer house near the lake.\nLake 'Jonson'? That should be north from here.\nThank the stars that there's a car outside, i think that it has some gas. It should be enough to get me to the lake.\nI have to hurry!";
                    break;
                case 3:
                    mText.text = "*sob sob* thank you stranger..\nThey took me hostage and they stole everything I had.\nYou say that you need me and my.. *grunt* tools?\nAs you can clearly see.. I've been shot and my tools are nowhere to be seen.\nI saw them going into the crypt in the graveyard with them.\nBe very careful, those creatures are lurking around, I think that it's their doing.\nHelp me get to the entrance, I can catch my breath there, just please clear out those creatures first.";
                    break;
                case 4:
                    mText.text = "I found the tools!!\nIf i remember correctly there should be a way out further in.\nThere's a hospital on the other side.\nI should be able to find some drugs or even someone to help the mechanic with his wound.\nI need to get there ASAP, I don't know how much longer I can keep this up.";
                    break;
                case 5:
                    mText.text = "Hey! You, yes you!\nYou look like you know how to use those guns pretty well.\n Thank you for saving me.\nWe have to get out of there as soon as we can.\nThese guys are using some kind of bio-weapon on the people. My collegues.. they all.. oh god..\nI'll do whatever I can to help you and get out of this hellish place.\nLet's go!";
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
        Debug.Log("merge!!!");
        if (SceneManager.GetActiveScene().buildIndex == 5)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
