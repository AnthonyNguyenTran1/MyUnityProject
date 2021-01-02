using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayForSpeed : MonoBehaviour
{
    public GameObject TheCylinder;
    public Text TheSpeedText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //TheSpeedText.text = TheCylinder.rigidbody.velocity.magnitude;
    }
}
