using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private const string PANEL_NAME_TRAVEL = "travel";
    private const string PANEL_NAME_TRAVEL_INFO = "travelInfo";

    private Stack<GameObject> panels;

    public GameObject mainPanel;
    public GameObject travelPanel;
    public GameObject travelInfoPanel;

    public Transform travelScreen;
    
    public GameObject infoBlock;

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    private void init()
    {
        panels = new Stack<GameObject>();
        panels.Push(mainPanel);
    }

    public void PushPanelToStack(string name)
    {
        GameObject tempObject = null;
        switch (name)
        {
            case PANEL_NAME_TRAVEL:
                tempObject = travelPanel;
                updateInfos<Travel>(travelScreen,
                    JsonManager.LoadJsonFile<Dictionary<string, Travel>>(JsonManager.JSON_FILENAME_TRAVEL));
                break;
            
            case PANEL_NAME_TRAVEL_INFO:
                tempObject = travelInfoPanel;
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

    private void updateInfos<T>(Transform panel, Dictionary<string, T> datas)
    {
        clearPanelInfos(panel);
        instantiatePanelInfos<T>(panel, datas);
    }

    private void clearPanelInfos(Transform panel)
    {
        for (int i = panel.childCount - 1; i >= 0; i--)
        {
            Destroy(panel.GetChild(i).gameObject);
        }
    }

    private void instantiatePanelInfos<T>(Transform panel, Dictionary<string, T> datas)
    {
        foreach (KeyValuePair<string, T> data in datas)
        {
            Instantiate(infoBlock, panel, true);
        }

    }
}
