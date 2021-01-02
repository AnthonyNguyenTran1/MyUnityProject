using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public class LoadingLines : MonoBehaviour
{

    private string filePath = @"C:\theFiles\theText.txt";

    private LineRenderer lineRend;
    public Material material;
    private List<Vector3> TheListOfOverallPoints = new List<Vector3>();
    private int currLines = 0;

    // Start is called before the first frame update
    void Start()
    {
        //lineRend = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {

            List<string> TheListsOfLines = new List<string>();

            TheListsOfLines = File.ReadAllLines(filePath).ToList();

            List<string> ThePointsOfEachLine = new List<string>();

            
            List<List<Vector3>> TheListsofLists = new List<List<Vector3>>();

            List<Vector3> TheFirstPoints = new List<Vector3>();
            List<Vector3> TheEndPoints = new List<Vector3>();

            Char[] myCharacters = { '(', ')' };

            foreach (string line in TheListsOfLines)
            {
                //Debug.Log("New Line/List of Points: ");
                ThePointsOfEachLine = line.Split(myCharacters).ToList();
                //List<Vector3> ThePointsConverted = new List<Vector3>();
                //lineRend = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
                List<Vector3> NewSublist = new List<Vector3>();




                foreach (string poinnt in ThePointsOfEachLine.ToList())
                {
                    //Debug.Log(poinnt);
                    if (poinnt.Any(char.IsDigit))
                    {

                        //Debug.Log(poinnt);
                        string[] TheVecComponents = poinnt.Split(',');

                        Vector3 result = new Vector3(
                        float.Parse(TheVecComponents[0]),
                        float.Parse(TheVecComponents[1]),
                        float.Parse(TheVecComponents[2]));


                        //TheListOfOverallPoints.Add(result);

                        NewSublist.Add(result);

                        if (poinnt.Equals(ThePointsOfEachLine[ThePointsOfEachLine.Count - 2]))
                        {
                            TheListsofLists.Add(NewSublist);
                        }
                        
                        
                        

                        //lineRend.positionCount = TheListOfOverallPoints.Count;
                        //lineRend.SetPositions(TheListOfOverallPoints.ToArray());
                        

                        //Debug.Log(result.ToString());


                    }

                    
                }


            }



            
            foreach (List<Vector3> list in TheListsofLists)
            {
                //Debug.Log("New Line/List: ");
                lineRend = new GameObject("Line" + currLines).AddComponent<LineRenderer>();
                lineRend.material = material;
                lineRend.startWidth = 0.5f;
                lineRend.endWidth = 0.5f;
                

                foreach (Vector3 eachpoint in list)
                {
                    //Debug.Log(eachpoint.ToString());
                    if (list.IndexOf(eachpoint) < list.Count)
                    {
                        lineRend.positionCount = list.Count;
                        lineRend.SetPositions(list.ToArray());

                        

                        
                    }
                }
            } 



        }



    }

    

    private float DistanceToLastPoint(Vector3 point)
    {
        if (!TheListOfOverallPoints.Any())
            return Mathf.Infinity;
        return Vector3.Distance(TheListOfOverallPoints.Last(), point);
    }

    public void AddColliderToLine(LineRenderer line, Vector3 startPoint, Vector3 endPoint)
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
