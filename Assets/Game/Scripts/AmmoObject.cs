using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoObject : MonoBehaviour
{
    public GameObject pistolModel;
    public GameObject rifleModel;
    public GameObject shotgunModel;
    public GameObject sniperModel;
    public GameObject armorModel;

    public int pistolProbability = 30;
    public int rifleProbability = 20;
    public int shotgunProbability = 15;
    public int sniperProbability = 15;
    public int armorProbability = 20;

    public int pistolAmmo = 0;
    public int rifleAmmo = 0;
    public int shotgunAmmo = 0;
    public int sniperAmmo = 0;
    public int armor = 0;

    void Start()
    {
        pistolModel.SetActive(false);
        rifleModel.SetActive(false);
        shotgunModel.SetActive(false);
        sniperModel.SetActive(false);

        int p = Random.Range(0, 101);
        
        if (p < pistolProbability)
        {
            pistolAmmo = Random.Range(10, 21);
            pistolModel.SetActive(true);
            return;
        }
        p -= pistolProbability;

        if (p < rifleProbability)
        {
            rifleAmmo = Random.Range(10, 21);
            rifleModel.SetActive(true);
            return;
        }
        p -= rifleProbability;

        if (p < shotgunProbability)
        {
            shotgunAmmo = Random.Range(5, 11);
            shotgunModel.SetActive(true);
            return;
        }
        p -= shotgunProbability;

        if (p < sniperProbability)
        {
            sniperAmmo = Random.Range(5, 11);
            sniperModel.SetActive(true);
            return;
        }
        p -= sniperProbability;

        if (p < armorProbability)
        {
            armor = Random.Range(50, 101);
            armorModel.SetActive(true);
            return;
        }
    }
}
