using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    public float interactionDistance = 25f;
    public bool imnear;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Vector3 forward = GameObject.FindGameObjectWithTag("MainCamera").transform.TransformDirection(Vector3.forward) * 25;
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);
        //Debug.DrawLine(transform.position, transform.forward * interactionDistance, Color.green);
        

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            Debug.Log("You hit something");
            Debug.Log(hit.point);
        } 

        






    }
}
