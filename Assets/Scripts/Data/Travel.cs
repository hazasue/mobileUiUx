using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Travel
{
    public string code;
    public string name;
    public string address;
    public List<int> coordinates;
    public float rate;

    public Travel(string code, string name, string address, List<int> coordinates, float rate)
    {
        this.code = code;
        this.name = name;
        this.address = address;
        this.coordinates = coordinates;
        this.rate = rate;
    }
}
