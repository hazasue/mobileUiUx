using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Calendar : MonoBehaviour
{
    public TMP_Text yearText;
    public TMP_Text monthText;
    public Transform viewport;
    public Date dayButton;
    
    public void Init(int year, int month)
    {
        yearText.text = year.ToString();
        monthText.text = month.ToString();

        Date tempDate;
        for (int i = 0; i < (calculateDay(year, month) + 1) % 7; i++)
        {
            tempDate = Instantiate(dayButton, viewport, true);
            tempDate.Init();
        }

        for (int i = 1; i < calculateDayOfMonth(year, month) + 1; i++)
        {
            tempDate = Instantiate(dayButton, viewport, true);
            tempDate.Init(year, month, i);
        }
    }

    private int calculateDay(int year, int month)
    {
        int day = 0;
            
        for (int i = 1; i < year; i++)
        {
            if ((i % 4 == 0 && i % 100 != 0)
                || i % 400 == 0)
                day += 366;
            else
                day += 365;
        }

        if (month > 1) day += 31;

        if (month > 2)
        {
            if ((year % 4 == 0 && year % 100 != 0)
                || year % 400 == 0)
                day += 29;
            else
            {
                day += 28;
            }
        }
        
        if (month > 3) day += 31;
        if (month > 4) day += 30;
        if (month > 5) day += 31;
        if (month > 6) day += 30;
        if (month > 7) day += 31;
        if (month > 8) day += 31;
        if (month > 9) day += 30;
        if (month > 10) day += 31;
        if (month > 11) day += 30;

        return day;
    }

    private int calculateDayOfMonth(int year, int month)
    {
        if (month == 1
            || month == 3
            || month == 5
            || month == 7
            || month == 8
            || month == 10
            || month == 12)
            return 31;
        
        else if (month == 4
                 || month == 6
                 || month == 9
                 || month == 11)
            return 30;
        
        else
        {
            if (((year % 4 == 0 && year % 100 != 0)
                 || year % 400 == 0))
                return 29;
            else
                return 28;
        }
    }
}
