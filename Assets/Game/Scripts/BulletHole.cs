using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    public float timeToLive = 10f;
    private float timer;

    void Start()
    {
        timer = timeToLive;
    }

    void Update()
    {
        if (PauseMenu.isPaused || PauseMenu.inStory)
            return;

        timer -= Time.deltaTime;
        if (timer < 0)
            Destroy(this.gameObject);
    }
}
