using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Travel : MonoBehaviour
{
    public string code;
    public string name;
    public string address;
    public List<int> coordinates;
    public float rate;
    public string startTime;
    public string endTime;

    public Image image;
    public TMP_Text nameText;
    public TMP_Text addressText;
    public TMP_Text coordinatesText;
    public TMP_Text rateText;
    public TMP_Text timeText;

    public void Init(string code, string name, string address, List<int> coordinates, float rate)
    {
        this.code = code;
        this.name = name;
        this.address = address;
        this.coordinates = coordinates;
        this.rate = rate;

        this.image.sprite = Resources.Load<Sprite>("Sprites/Travels/" + code);
        nameText.text = name;
        addressText.text = address;
        coordinatesText.text = "";
        rateText.text = rate.ToString("F1");
    }

    public void Init(string code, string name, string startTime, string endTime)
    {
        this.code = code;
        this.name = name;
        this.startTime = startTime;
        this.endTime = endTime;
        
        nameText.text = name;
        timeText.text = $"{startTime} ~ {endTime}";
    }
    
    
}
