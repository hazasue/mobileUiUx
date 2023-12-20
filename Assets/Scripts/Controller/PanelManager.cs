using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PanelManager : MonoBehaviour
{
    private static PanelManager instance;

    private const float DEFAULT_SCREEN_WIDTH = 360f;
    
    private const string PANEL_NAME_TRAVEL = "travel";
    private const string PANEL_NAME_TRAVEL_INFO = "travelInfo";
    private const string PANEL_NAME_PLAN_CALENDAR = "calendarPlan";
    private const string PANEL_NAME_MAKE_PLAN = "makePlan";
    private const string PANEL_NAME_LOAD_PLAN = "loadPlan";
    private const string PANEL_NAME_FIND_TRAVEL = "findTravel";
    private const string PANEL_NAME_TRAVEL_DETAIL = "detailTravel";
    private const string PANEL_NAME_RECOMMEND_PLAN = "recommendPlan";

    private Stack<GameObject> panels;

    public Transform mainViewport;
    public Transform mainToggle;

    public GameObject mainPanel;
    public GameObject travelPanel;
    public GameObject travelInfoPanel;
    public GameObject planCalendarPanel;
    public GameObject makePlanPanel;
    public GameObject findTravelPanel;
    public GameObject travelDetailPanel;
    public GameObject recommendPlanPanel;

    public Transform travelScreen;
    
    public GameObject infoBlock;

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    private void init()
    {
        instance = this;
        panels = new Stack<GameObject>();
        panels.Push(mainPanel);
    }
    
    public static PanelManager GetInstance()
    {
        if (instance != null) return instance;
        instance = FindObjectOfType<PanelManager>();
        if (instance == null) Debug.Log("There's no active PanelManager object");
        return instance;
    }

    public void PushPanelToStack(string name)
    {
        GameObject tempObject = null;
        switch (name)
        {
            case PANEL_NAME_TRAVEL:
                tempObject = travelPanel;
                updateInfos(travelScreen,
                    JsonManager.LoadJsonFile<Dictionary<string, TravelInfo>>(JsonManager.JSON_FILENAME_TRAVEL));
                break;
            
            case PANEL_NAME_TRAVEL_INFO:
                Travel tempTravel = EventSystem.current.currentSelectedGameObject.GetComponent<Travel>();
                travelInfoPanel.GetComponent<TravelInfoPanel>().Init(tempTravel.code, tempTravel.name,
                    tempTravel.address, tempTravel.rate);
                tempObject = travelInfoPanel;
                break;
            
            case PANEL_NAME_PLAN_CALENDAR:
                tempObject = planCalendarPanel;
                break;
            
            case PANEL_NAME_MAKE_PLAN:
                if (!PlanManager.GetInstance().SaveDate()) return;
                PopPanelFromStack();
                tempObject = makePlanPanel;
                break;
            
            case PANEL_NAME_LOAD_PLAN:
                tempObject = makePlanPanel;
                break;
            
            case PANEL_NAME_FIND_TRAVEL:
                tempObject = findTravelPanel;
                PlanManager.GetInstance().InitFindTravelPanel();
                break;
            
            case PANEL_NAME_RECOMMEND_PLAN:
                tempObject = recommendPlanPanel;
                break;

            default:
                Debug.Log("Invalid panel name: " + name);
                return;
        }

        panels.Peek().SetActive(false);
        
        panels.Push(tempObject);
        tempObject.SetActive(true);
    }
    
    public void PushPanelToStack(string name, TravelInfo info)
    {
        GameObject tempObject = null;
        switch (name)
        {
            case PANEL_NAME_TRAVEL_DETAIL:
                travelDetailPanel.GetComponent<TravelInfoPanel>().Init(info.code, info.name, info.address, info.rate);
                tempObject = travelDetailPanel;
                break;
                

            default:
                Debug.Log("Invalid panel name: " + name);
                return;
        }

        panels.Peek().SetActive(false);
        
        panels.Push(tempObject);
        tempObject.SetActive(true);
    }

    public void PopPanelFromStack()
    {
        panels.Pop().SetActive(false);
        panels.Peek().SetActive(true);
    }

    private void updateInfos(Transform panel, Dictionary<string, TravelInfo> datas)
    {
        clearPanelInfos(panel);
        instantiatePanelInfos(panel, datas);
    }

    private void clearPanelInfos(Transform panel)
    {
        for (int i = panel.childCount - 1; i >= 0; i--)
        {
            Destroy(panel.GetChild(i).gameObject);
        }
    }

    private void instantiatePanelInfos(Transform panel, Dictionary<string, TravelInfo> datas)
    {
        foreach (KeyValuePair<string, TravelInfo> data in datas)
        {
            GameObject tempObject = Instantiate(infoBlock, panel, true);
            tempObject.transform.localScale = new Vector3(1f, 1f, 1f);
            tempObject.GetComponent<Travel>().Init(data.Value.code, data.Value.name, data.Value.address,
                data.Value.coordinates, data.Value.rate);
            tempObject.GetComponent<Button>().onClick.AddListener(() => PushPanelToStack(PANEL_NAME_TRAVEL_INFO));
        }
    }

    public void UseMainNavigator(int index)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels.Pop().SetActive(false);
        }
        panels.Push(mainPanel);
        mainPanel.SetActive(true);

        StartCoroutine(slideMainToggle(new Vector3(-index * DEFAULT_SCREEN_WIDTH, 0f, 0f)));
        mainToggle.localScale = new Vector3(1f, 1f, 1f);
    }

    private IEnumerator slideMainToggle(Vector3 position)
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            mainViewport.localPosition = Vector3.Lerp(mainViewport.localPosition, position, 0.4f);
            mainToggle.localPosition = Vector3.Lerp(mainToggle.localPosition, -position / 3, 0.4f);
            if (Vector3.Distance(mainViewport.localPosition, position) <= DEFAULT_SCREEN_WIDTH / 36f)
            {
                mainViewport.localPosition = position;
                mainToggle.localPosition = -position / 3;
                yield break;
            }
        }
    }

    public void BackToMain()
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels.Pop().SetActive(false);
        }
        panels.Push(mainPanel);
        mainPanel.SetActive(true);
    }
}
