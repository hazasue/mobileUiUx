using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    private const string PANEL_NAME_TRAVEL = "travel";
    private const string PANEL_NAME_TRAVEL_INFO = "travelInfo";

    private Stack<GameObject> panels;
    
    public GameObject travelPanel;
    public GameObject travelInfoPanel;

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    private void init()
    {
        panels = new Stack<GameObject>();
    }

    public void PushPanelToStack(string name)
    {
        GameObject tempObject = null;
        switch (name)
        {
            case PANEL_NAME_TRAVEL:
                tempObject = travelPanel;
                break;
            
            case PANEL_NAME_TRAVEL_INFO:
                tempObject = travelInfoPanel;
                break;
            
            default:
                Debug.Log("Invalid panel name: " + name);
                return;
        }

        panels.Push(tempObject);
        tempObject.SetActive(true);
    }

    public void PopPanelFromStack()
    {
        GameObject tempObject = panels.Pop();
        tempObject.SetActive(false);
    }
}
