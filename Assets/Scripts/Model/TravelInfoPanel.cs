using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TravelInfoPanel : MonoBehaviour
{
    private string code;
    private string name;
    private string address;
    private List<int> coordinates;
    private float rate;

    public Image image;
    public TMP_Text nameText;
    public TMP_Text addressText;
    public TMP_Text rateText;

    public void Init(string code, string name, string address, float rate)
    {
        this.code = code;
        this.name = name;
        this.address = address;
        this.rate = rate;

        this.image.sprite = Resources.Load<Sprite>("Sprites/Travels/" + code);
        nameText.text = name;
        addressText.text = address;
        rateText.text = rate.ToString("F1");
    }
}
