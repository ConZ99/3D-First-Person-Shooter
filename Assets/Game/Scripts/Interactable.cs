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

    public SaveSystem saveScript;
    public GameObject weaponCamera;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Awake()
    {
        weaponCamera = GameObject.FindGameObjectWithTag("MainCamera").transform.GetChild(0).gameObject;
    }

    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        qtItems = GameObject.FindGameObjectsWithTag("Story");
        if (qtItems.Length == 1){
            storyText = qtItems[0];
        }
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
        weaponCamera.SetActive(false);
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
        } else {
            Tools = GameObject.FindGameObjectsWithTag("Tools");
            if (Tools.Length == 1){
                tools = Tools[0];
                tools.tag = "Untagged";
            }
            
        }
        weaponCamera.SetActive(true);
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
                    mText.text = "Hey!\nOh, thank God!\nSomeone alive and breathing! Everyone was taken! We have to get out.\nMy van broke down and I cannot fix it myself. We need tools and someone to help us with fixing it.\nYou need to go to the city and find the mechanic at his shop. Be careful though because I've heard gunshots coming from there. I think that those terrorists are still around there somewhere.";
                    break;
                case 2:
                    mText.text = "Hmm... there's no one here.\nThis note says that the mechanic left his shop along with his tools and took refuge in his summer house near the lake.\nLake Johnson? That should be north from here.\nThank the stars that there's a car outside and I think that it has some gas as well. It should be enough to get me to the lake.\nI must hurry!";
                    break;
                case 3:
                    mText.text = "*sob sob* Thank you, stranger...\nThey took me hostage and they stole everything I had.\nYou say that you need me and my... *grunt* tools?\nAs you can clearly see.. I've been shot and my tools no longer in my possession.\nI saw them going into the crypt in the graveyard with them.\nBe very careful, those creatures are lurking around. I think that's their doing.\nHelp me get to the entrance, I can catch my breath there... Just please clear out those creatures first.";
                    break;
                case 4:
                    mText.text = "I found the tools!!\nIf I remember correctly, there should be a way out further in.\nThere's also a hospital on the other side.\nI should be able to find some drugs or even someone to help the mechanic with his wound.\nI need to get there ASAP. I don't know how much longer I can keep this up..";
                    break;
                case 5:
                    mText.text = "Hey! You! Yes, you!\nLooks like you know how to use those guns pretty well!\n Thank you for saving me. I was hiding from these dangerous guys for hours!\nWe have to get out of here as soon as we can.\nThese guys are using some kind of bio-weapon on the people. My collegues.. they all.. *sob* (again, poti sa o scoti)oh God..\nI'll do whatever I can to help you and get out of this hellish place.\nLet's go!";
                    break;
                default:
                    mText.text = "Out of bounds";
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
    }

    public void StopMoving()
    {
        currentWaypoint = null;
        nav.isStopped = true;
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
        if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            //cand termini ultimul nivel te trimite la scena de epilog, scena 6
            SceneManager.LoadScene(6);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            saveScript.Delete();
        }
        else
        {
            //distruge obiectele cu script Interactable ca sa nu mai dea eroare "target destroyed"
            Object[] quest_objs = GameObject.FindGameObjectsWithTag("Quest");
            for (int i = 0; i < quest_objs.Length; i++)
            {
                if (quest_objs[i] != this.gameObject)
                    Destroy(quest_objs[i]);
            }

            saveScript.Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
