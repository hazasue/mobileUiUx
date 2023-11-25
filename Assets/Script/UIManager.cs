using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
    private bool isLogin = true;
    private List<GameObject> InstanceObject = new List<GameObject>();
    [SerializeField] private GameObject NewReview;
    [SerializeField] private GameObject WriteWindow;
    [SerializeField] private InputField Review;
    public bool IsLogin
    {
        get { return isLogin; }
        set { isLogin = value; }
    }
    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonEvent(string name)
    {
        if (isLogin)
        {
            switch (name)
            {
                case "review":
                    LoadReviewPanel();
                    break;

                case "save":
                    LoadSavePanel();
                    break;

                case "setting":
                    break;

                case "back":
                    DestroyObject();
                    break;

                case "newreview":
                    LoadWriteReview();
                    break;

                case "saving":

                    break;
            }
        }
    }

    private void LoadReviewPanel()
    {
        Privacy privacy = GameObject.Find("PrivacyPanel").GetComponent<Privacy>();
        for (int i = 0; i <= privacy.ReviewList.Count; i++)
        {
            if (i == privacy.ReviewList.Count || privacy.ReviewList.Count == 0)
            {
                GameObject NewReviewButton = Instantiate(NewReview);
                NewReviewButton.transform.SetParent(GameObject.Find("ReviewPanel").transform, false);
                InstanceObject.Add(NewReviewButton);
                RectTransform newRect = NewReviewButton.GetComponent<RectTransform>();
                newRect.anchorMin = new Vector2(0.5f, 0.8f);
                newRect.anchorMax = new Vector2(0.5f, 0.8f);
                newRect.anchoredPosition = new Vector2(0f, 0f - (i * 50f));

                Button button = NewReviewButton.GetComponent<Button>();
                if(button != null)
                {
                    button.onClick.AddListener(() => ButtonEvent("newreview"));
                }
            }
            else
            {
                GameObject temp = Instantiate(privacy.ReviewList[i]);
                InstanceObject.Add(temp);
                temp.transform.SetParent(GameObject.Find("ReviewPanel").transform, false);
                RectTransform tempRect = temp.GetComponent<RectTransform>();
                tempRect.anchorMin = new Vector2(0.5f, 0.8f);
                tempRect.anchorMax = new Vector2(0.5f, 0.8f);
                tempRect.anchoredPosition = new Vector2(0f, 0f - (i * 50f));
            }
        }
    }

    private void LoadSavePanel()
    {
        
        Privacy privacy = GameObject.Find("PrivacyPanel").GetComponent<Privacy>();
        for (int i = 0; i < privacy.SaveList.Count; i++)
        {
            Debug.Log(privacy.SaveList);
            GameObject temp = Instantiate(privacy.SaveList[i]);
            InstanceObject.Add(temp);
            temp.transform.SetParent(GameObject.Find("SavePanel").transform, false);
            RectTransform tempRect = temp.GetComponent<RectTransform>();
            tempRect.anchorMin = new Vector2(0.5f, 0.8f);
            tempRect.anchorMax = new Vector2(0.5f, 0.8f);
            tempRect.anchoredPosition = new Vector2(0f, 0f - (i * 50f));
        }
    }

    private void LoadWriteReview()
    {
        GameObject temp = Instantiate(WriteWindow);
        InstanceObject.Add(temp);
        temp.transform.SetParent(GameObject.Find("WriteReviewPanel").transform, false);
        RectTransform tempRect = temp.GetComponent<RectTransform>();
        tempRect.anchorMin = new Vector2(0.5f, 0.7f);
        tempRect.anchorMax = new Vector2(0.5f, 0.7f);
        tempRect.anchoredPosition = new Vector2(0f, 0f);
    }

    private void DestroyObject()
    {
        foreach(GameObject go in InstanceObject)
        {
            Destroy(go);
        }
    }
}
