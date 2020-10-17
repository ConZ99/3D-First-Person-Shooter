using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoObject : MonoBehaviour
{
    public GameObject rifleModel;
    public GameObject pistolModel;
    public int rifleAmmo = 0;
    public int pistolAmmo = 0;

    void Start()
    {
        int type = Random.Range(1, 3);
        if (type == 1)
        {
            rifleAmmo = Random.Range(10, 20);
            rifleModel.SetActive(true);
            pistolModel.SetActive(false);
        }
        else
        {
            pistolAmmo = Random.Range(10, 20);
            rifleModel.SetActive(false);
            pistolModel.SetActive(true);
        }
    }
}
