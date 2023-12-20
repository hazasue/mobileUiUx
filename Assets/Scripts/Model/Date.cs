using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Date : MonoBehaviour
{
    public TMP_Text dayText;

    public int year;
    public int month;
    public int day;

    private bool clicked;

    public void Init(int year, int month, int day)
    {
        this.year = year;
        this.month = month;
        this.day = day;
        clicked = false;

        if (day != 0) dayText.text = day.ToString();
    }

    public void Init()
    {
        day = 0;
        dayText.text = "";
    }

    public void Clicked()
    {
        if (!clicked)
        {
            clicked = true;
            PlanManager.GetInstance().SelectDate(this);
        }
        else
        {
            clicked = false;
            PlanManager.GetInstance().UnSelectDate(this);
        }
    }
}
