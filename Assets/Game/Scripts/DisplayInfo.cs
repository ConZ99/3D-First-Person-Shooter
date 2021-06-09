using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInfo : MonoBehaviour
{
    public int maxElements = 10;
    public float timeToDisplay = 5f;
    private List<string> msg_lst;
    private List<float> time_lst;

    void Start()
    {
        msg_lst = new List<string>();
        time_lst = new List<float>();
    }


    void Update()
    {
        UpdateTime();
        CheckTime();
        CheckLength();
        DisplayMsg();
    }

    public void addMsg(string msg)
    {
        msg_lst.Add(msg);
        time_lst.Add(timeToDisplay);
    }

    private void CheckLength()
    {
        while (msg_lst.Count > maxElements)
        {
            msg_lst.RemoveAt(0);
            time_lst.RemoveAt(0);
        }
    }

    private void CheckTime()
    {
        while (msg_lst.Count > 0 && time_lst[0] <= 0f)
        {
            msg_lst.RemoveAt(0);
            time_lst.RemoveAt(0);
        }
    }

    private void UpdateTime()
    {
        for (int i = 0; i < msg_lst.Count; i++)
        {
            time_lst[i] -= Time.deltaTime;
        }
    }

    private void DisplayMsg()
    {
        string str = "";
        for (int i = 0; i < msg_lst.Count; i++)
        {
            str += msg_lst[i];
            str += "\n";
        }
        this.gameObject.GetComponent<TextMeshProUGUI>().text = str;
    }
}
