using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;
    public GameObject border;

    void LateUpdate()
    {
        if (PauseMenu.isPaused)
            return;

        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90, player.eulerAngles.y, 0f);
        border.transform.rotation = Quaternion.Euler(0,0, player.eulerAngles.y);
    }
}
