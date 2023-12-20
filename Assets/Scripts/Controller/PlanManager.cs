using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlanManager : MonoBehaviour
{
    private static PlanManager instance;
    
    private const int DEFAULT_CALENDAR_COUNT = 3;
    private const int DEFAULT_MONTHS_A_YEAR = 12;
    
    private List<PlanInfo> planInfos;

    public GameObject planButton;
    public Transform viewport;

    public Transform calendarViewport;
    public Calendar calendar;
    public TMP_Text selectDay;

    public List<Date> selectedDate;

    private int currentPlanIdx;
    private int currentDate;
    
    // make plan
    public Transform planListViewport;
    public TMP_Text dayInfo;
    public GameObject addButtons;
    public GameObject infoBlock;
    public GameObject memoObject;
    
    void Awake()
    {
        init();
    }

    private void init()
    {
        instance = this;
        
        if (!File.Exists(Application.dataPath + "/Data/"))
        {
            Directory.CreateDirectory(Application.dataPath + "/Data/");
        }

        planInfos = new List<PlanInfo>();
        
        if (!File.Exists(Application.dataPath + "/Data/" + JsonManager.JSON_FILENAME_PLAN + ".json"))
            JsonManager.CreateJsonFile(JsonManager.JSON_FILENAME_PLAN, planInfos);
        else
        {
            planInfos = JsonManager.LoadJsonFile<List<PlanInfo>>(JsonManager.JSON_FILENAME_PLAN);
        }
        selectedDate = new List<Date>();

        currentPlanIdx = 0;
        currentDate = 0;
        instantiatePlanInfos();
    }

    private void instantiatePlanInfos()
    {
        GameObject tempObject;
        PlanInfo tempPlanInfo;
        
        for (int i = viewport.childCount - 1; i >= 0; i--)
        {
            Destroy(viewport.transform.GetChild(i).gameObject);
        }

        int idx = 0;
        foreach (PlanInfo data in planInfos)
        {
            tempObject = Instantiate(planButton, viewport, true);
            tempObject.transform.localScale = new Vector3(1f, 1f, 1f);
            tempObject.GetComponent<Plan>().Init(idx++, data.planName, data.startDate, data.endDate);
            tempObject.GetComponent<Button>().onClick.AddListener(LoadPlanInfo);
        }
    }

    public static PlanManager GetInstance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<PlanManager>();
        if (instance == null) Debug.Log("There's no active PlanManager object");
        return instance;
    }

    public void CreatePlan()
    {
        planInfos.Add(new PlanInfo("", "", "", new List<int>(), new List<string>(), new List<int>(), new List<int>()));
        JsonManager.CreateJsonFile(JsonManager.JSON_FILENAME_PLAN, planInfos);
        currentPlanIdx = planInfos.Count - 1;
        consistCalendar();
    }

    private void consistCalendar()
    {
        for (int i = calendarViewport.childCount - 1; i >= 0; i--)
        {
            Destroy(calendarViewport.GetChild(i).gameObject);
        }
        
        Calendar tempCalendar;
        DateTime now = System.DateTime.Now;
        int year = now.Year;
        int month = now.Month;
        for (int i = 0; i < DEFAULT_CALENDAR_COUNT; i++)
        {
            tempCalendar = Instantiate(calendar, calendarViewport, true);
            tempCalendar.Init(year, month++);
            tempCalendar.transform.localScale = new Vector3(1f, 1f, 1f);

            if (month > DEFAULT_MONTHS_A_YEAR)
            {
                year++;
                month -= DEFAULT_MONTHS_A_YEAR;
            }
        }
        
        selectDay.text += " / 등록 완료";
    }

    public void SelectDate(Date date)
    {
        if (selectedDate.Count >= 2) return;
        
        selectedDate.Add(date);
        sortSelectedDate();
        updateCalendarInfo();
    }

    public void UnSelectDate(Date date)
    {
        if (!selectedDate.Contains(date)) return;
        
        selectedDate.Remove(date);
        updateCalendarInfo();
    }

    private void updateCalendarInfo()
    {
        selectDay.text = "";

        for (int i = 0; i < selectedDate.Count; i++)
        {
            selectDay.text += $"{selectedDate[i].year}.{selectedDate[i].month}.{selectedDate[i].day}";
            if (i < selectedDate.Count - 1) selectDay.text += " ~ ";
        }
        selectDay.text += " / 등록 완료";
    }

    private void sortSelectedDate()
    {
        if (selectedDate.Count < 2) return;
        
        Date date1 = selectedDate[0];
        Date date2 = selectedDate[1];


        if (date2.year < date1.year) swapDate();
        else if (date2.month < date1.month
                 && date2.year < date1.year) swapDate();
        else if (date2.day < date1.day) swapDate();
        else return;
    }

    private void swapDate()
    {
        selectedDate.Add(selectedDate[0]);
        selectedDate.Remove(selectedDate[0]);
    }

    public bool SaveDate()
    {
        if (selectedDate.Count < 1) return false;
        
        planInfos[currentPlanIdx].startDate = $"{selectedDate[0].year}.{selectedDate[0].month}.{selectedDate[0].day}";
        planInfos[currentPlanIdx].endDate = $"{selectedDate[selectedDate.Count - 1].year}.{selectedDate[selectedDate.Count - 1].month}.{selectedDate[selectedDate.Count - 1].day}";
        for (int i = 0;
             i <= int.Parse(planInfos[currentPlanIdx].endDate.Split('.')[2]) - int.Parse(planInfos[currentPlanIdx].startDate.Split('.')[2]);
             i++)
        {
            planInfos[currentPlanIdx].countADay.Add(0);
        }
        JsonManager.CreateJsonFile(JsonManager.JSON_FILENAME_PLAN, planInfos);

        selectedDate = new List<Date>();

        initPlan(currentPlanIdx);
        instantiatePlanInfos();

        return true;
    }

    private void initPlan(int idx)
    {
        for (int i = planListViewport.childCount - 1; i >= 0; i--)
        {
            Destroy(planListViewport.GetChild(i).gameObject);
        }
        
        int index = 0;
        Dictionary<string, TravelInfo> datas = JsonManager.LoadJsonFile<Dictionary<string, TravelInfo>>(JsonManager.JSON_FILENAME_TRAVEL);
        
        for (int i = 0; i <= int.Parse(planInfos[idx].endDate.Split('.')[2]) - int.Parse(planInfos[idx].startDate.Split('.')[2]); i++)
        {
            int tempDate = i;
            TMP_Text tempText = Instantiate(dayInfo, planListViewport, true);
            tempText.transform.localScale = new Vector3(1f, 1f, 1f);
            tempText.text = $"{i + 1}일차 {planInfos[idx].startDate.Split('.')[1]}.{int.Parse(planInfos[idx].startDate.Split('.')[2]) + i}";

            for (int j = index; j < index + planInfos[idx].countADay[i]; j++)
            {
                foreach (TravelInfo data in datas.Values)
                {
                    if (planInfos[idx].names[j] != data.name) continue;
 
                    GameObject tempObject = Instantiate(infoBlock, planListViewport, true);
                    tempObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    tempObject.GetComponent<Travel>().Init(data.code, data.name, "", "");
                    break;
                }
            }

            index += planInfos[idx].countADay[i];

            GameObject tempButtons = Instantiate(addButtons, planListViewport, true);
            tempButtons.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => PanelManager.GetInstance().PushPanelToStack("findTravel"));
            tempButtons.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => SaveDate(tempDate));
            tempButtons.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => ActivateMemo(true));
            tempButtons.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void LoadPlanInfo()
    {
        currentPlanIdx = EventSystem.current.currentSelectedGameObject.GetComponent<Plan>().idx;
        initPlan(currentPlanIdx);
        PanelManager.GetInstance().PushPanelToStack("loadPlan");
    }

    public void ActivateMemo(bool state)
    {
        memoObject.SetActive(state);
    }

    public void SaveDate(int date)
    {
        currentDate = date;
    }

    public void AddToPlan()
    {
        planInfos[currentPlanIdx].countADay[currentDate]++;
        planInfos[currentPlanIdx].names.Add("세종 호수 공원");
        JsonManager.CreateJsonFile(JsonManager.JSON_FILENAME_PLAN, planInfos);
        initPlan(currentPlanIdx);

        PanelManager.GetInstance().PopPanelFromStack();
    }
}
