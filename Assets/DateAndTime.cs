using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DateAndTime : MonoBehaviour
{
    public TextMeshPro largeText;
    public Text TheDateAndTime;

    // Start is called before the first frame update
    void Start()
    {
        /*
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy   HH:mm");
        TheDateAndTime.text = time; */
    }

    // Update is called once per frame
    void Update()
    {
        
        string time = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy   HH:mm");
        TheDateAndTime.text = time;
        
    }
}
