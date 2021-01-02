using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class VisibleLineForRaycast : MonoBehaviour
{
    public float interactionDistance = 25f;
    public bool imnear;
    public float distance = 6.0f;
    bool cast = false;

    private LineRenderer lineRend;
    public Material material;
    private int currLines = 0;
    private Vector3 StartPoint;
    private Vector3 EndPoint;
    private string filePath = @"C:\theFiles\theText.txt";
    private string ThePointsForTheFile;

    private List<Vector3> points = new List<Vector3>();
    private List<List<Vector3>> TheListsofListss = new List<List<Vector3>>();
    

    [SerializeField]
    private Text distanceText;
    public Text StartPointText;
    public Text EndPointText;
    private float distanceForLine;
    public Text MeshName;

    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        
 
        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.green);
        

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            MeshName.text = hit.collider.gameObject.name;
        }


        if (Input.GetMouseButtonDown(0))
        {
            points.Clear();
            
            lineRend = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
            lineRend.material = material;
            lineRend.startWidth = 0.5f;
            lineRend.endWidth = 0.5f;

            
        }

        if (Input.GetMouseButton(0) && Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            

            if (DistanceToLastPoint(hit.point) > 1f)
            {
                points.Add(hit.point);

                
                lineRend.positionCount = points.Count;
                lineRend.SetPositions(points.ToArray());



            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            

            float TheCurrentDistance = 0;
            float TheOverallDistance = 0;
            string TheLabel = null;

             

            TheLabel = System.Environment.NewLine + "List of Points: ";
            File.AppendAllText(filePath, TheLabel);

            for (int i = 0; i < points.Count; i++)
            {
                if (i + 1 < points.Count)
                {
                    TheCurrentDistance = (points[i + 1] - points[i]).magnitude;
                    TheOverallDistance = TheOverallDistance + TheCurrentDistance;
                    distanceText.text = TheOverallDistance.ToString("0.00") + "m";

                    

                }

                ThePointsForTheFile = points[i].ToString() + " ";
                File.AppendAllText(filePath, ThePointsForTheFile);

            }

            
            StartPointText.text = points[0].ToString();
            EndPointText.text = points[points.Count - 1].ToString();

            TheListsofListss.Add(points);




        } 

            

    }

    private float DistanceToLastPoint(Vector3 point)
    {
        if (!points.Any())
            return Mathf.Infinity;
        return Vector3.Distance(points.Last(), point);
    }

    private void AddColliderToLine(LineRenderer line, Vector3 startPoint, Vector3 endPoint)
    {
        //create the collider for the line
        BoxCollider lineCollider = new GameObject("LineCollider").AddComponent<BoxCollider>();
        //set the collider as a child of your line
        lineCollider.transform.parent = line.transform;
        // get width of collider from line 
        float lineWidth = line.endWidth;
        // get the length of the line using the Distance method
        float lineLength = Vector3.Distance(startPoint, endPoint);
        // size of collider is set where X is length of line, Y is width of line
        //z will be how far the collider reaches to the sky
        lineCollider.size = new Vector3(lineLength, lineWidth, 1f);
        // get the midPoint
        Vector3 midPoint = (startPoint + endPoint) / 2;
        // move the created collider to the midPoint
        lineCollider.transform.position = midPoint;


        //heres the beef of the function, Mathf.Atan2 wants the slope, be careful however because it wants it in a weird form
        //it will divide for you so just plug in your (y2-y1),(x2,x1)
        float angle = Mathf.Atan2((endPoint.z - startPoint.z), (endPoint.x - startPoint.x));

        // angle now holds our answer but it's in radians, we want degrees
        // Mathf.Rad2Deg is just a constant equal to 57.2958 that we multiply by to change radians to degrees
        angle *= Mathf.Rad2Deg;

        //were interested in the inverse so multiply by -1
        angle *= -1;
        // now apply the rotation to the collider's transform, carful where you put the angle variable
        // in 3d space you don't wan't to rotate on your y axis
        lineCollider.transform.Rotate(0, angle, 0);
    }








}
