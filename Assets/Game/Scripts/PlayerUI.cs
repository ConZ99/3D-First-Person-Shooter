using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    private Target target;
    public TextMeshProUGUI textboxHp;
    public TextMeshProUGUI textboxArmor;
    public TextMeshProUGUI textboxAmmo;
    public TextMeshProUGUI textboxCounter;

    void Awake()
    {
        target = transform.GetComponent<Target>();
    }

    void Update()
    {
        if (PauseMenu.isPaused)
            return;

        DisplayHealth();
        DisplayArmor();
    }

    public void DisplayHealth()
    {
        if (target.health < 30)
            textboxHp.text = "<b><color=#ff0000ff>" + target.health + "</color></b>";
        else
            textboxHp.text = "<b><color=#ffffffff>" + target.health + "</color></b>";
    }

    public void DisplayArmor()
    {
        textboxArmor.text = "<b><color=#ffffffff>" + target.armor + "</color></b>";
    }

    public void DisplayAmmo(int current_ammo, int total_ammo)
    {
        if (current_ammo == -1 && total_ammo == -1)
            textboxAmmo.text = "";
        else
            textboxAmmo.text = "<b>" + current_ammo + "/" + total_ammo + "</b>";
    }

    public void DisplayEnemiesCounter(int currentNumber, int totalNumber)
    {
        textboxCounter.text = "<b><color=#ffffffff>" + "Enemies left: " + currentNumber + "/" + totalNumber + "</color></b>";
    }
}
