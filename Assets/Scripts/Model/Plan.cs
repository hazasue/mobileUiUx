using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Plan : MonoBehaviour
{
    public int idx;
    public string planName;
    public string startDate;
    public string endDate;

    public TMP_Text name;
    public TMP_Text duration;

    public void Init(int idx, string planName, string startDate, string endDate)
    {
        this.idx = idx;
        this.planName = planName;
        this.startDate = startDate;
        this.endDate = endDate;

        name.text = planName;
        duration.text = startDate + " ~ " + endDate;
    }
}
