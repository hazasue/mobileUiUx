using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Date : MonoBehaviour
{
    public TMP_Text dayText;
    public TMP_Text dayTextToggle;

    public int year;
    public int month;
    public int day;

    public GameObject toggle;

    private bool clicked;

    public void Init(int year, int month, int day)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        clicked = false;

        toggle.SetActive(false);

        if (day != 0)
        {
            dayText.text = day.ToString();
            dayTextToggle.text = day.ToString();
        }
    }

    public void Init()
    {
        day = 0;
        dayText.text = "";
        dayTextToggle.text = "";
        toggle.SetActive(false);
    }

    public void Clicked()
    {
        if (!clicked)
        {
            clicked = PlanManager.GetInstance().SelectDate(this);
            if (clicked) toggle.SetActive(true);
        }
        else
        {
            clicked = PlanManager.GetInstance().UnSelectDate(this);
            if (!clicked) toggle.SetActive(false);
        }

        Debug.Log(clicked);
    }
}
