using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Privacy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Profile_IsLoginTrue;
    [SerializeField] private GameObject Profile_IsLoginFalse;
    [SerializeField] private GameObject Review;
    [SerializeField] private GameObject Save;
    [SerializeField] private GameObject Setting;

    public List<GameObject> SaveList = new List<GameObject>();
    public List<GameObject> ReviewList = new List<GameObject>();
    void Start()
    {
        if (UIManager.instance.IsLogin)
        {

        }

        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
