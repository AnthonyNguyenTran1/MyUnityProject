using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowingTheSpeed : MonoBehaviour
{
    public float speed = 0;
    Vector3 lastPosition = Vector3.zero;
    public Text TheSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    
    void FixedUpdate()
    {
        speed = (transform.position - lastPosition).magnitude / Time.fixedDeltaTime;
        lastPosition = transform.position;
        TheSpeed.text = speed.ToString("0.00") + " m/s";
    }


}
