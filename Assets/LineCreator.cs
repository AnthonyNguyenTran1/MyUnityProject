using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private List<Vector3> points = new List<Vector3>();

    public Action<IEnumerable<Vector3>> OnNewPathCreated = delegate { };

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            points.Clear();

        if (Input.GetButton("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                if (DistanceToLastPoint(hit.point) > 1f)
                {
                    points.Add(hit.point);

                    lineRenderer.positionCount = points.Count;
                    lineRenderer.SetPositions(points.ToArray());
                }
            }
        }

        else if (Input.GetButtonUp("Fire1"))
            OnNewPathCreated(points);

    }

    private float DistanceToLastPoint(Vector3 point)
    {
        if (!points.Any())
            return Mathf.Infinity;
        return Vector3.Distance(points.Last(), point);
    }



}
