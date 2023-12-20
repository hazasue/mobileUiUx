using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanInfo
{
    public string planName;
    public string startDate;
    public string endDate;
    public List<int> countADay;
    public List<string> names;
    public List<int> startTime;
    public List<int> endTime;

    public PlanInfo(string planName, string startDate, string endDate, List<int> countADay, List<string> names, List<int> startTime,
        List<int> endTime)
    {
        this.planName = planName;
        this.startDate = startDate;
        this.endDate = endDate;
        this.countADay = countADay;
        this.names = names;
        this.startTime = startTime;
        this.endTime = endTime;
    }
}
