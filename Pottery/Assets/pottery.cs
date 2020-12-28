using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;


public class pottery : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);
    public GameObject workpiece;
    public Mesh main;
    public Vector3[] vertices;
    public GameObject hand;
    public float brushsize = 0.1f;
    float intencity = 1f;
    public RaycastHit hit;
    Vector3 test;
    public Transform mouse;
    public Transform rotator;
    public Transform workedvert;
    Vector2 repeats;
    // Start is called before the first frame update
    void Start()
    {
        repeats = new Vector2(1,1);
           main = workpiece.GetComponent<MeshFilter>().mesh;
        Cursor.visible =false;

    }

    // Update is called once per frame
    void Update()
    { workpiece.transform.Rotate(new Vector3(0, 10, 0)*Time.deltaTime *10); //wrap
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotator.rotation = Quaternion.Euler(rotator.eulerAngles.x, rotator.eulerAngles.y + 0.1f , 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotator.rotation = Quaternion.Euler(rotator.eulerAngles.x, rotator.eulerAngles.y - 0.1f, 0);

        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
           // rotator.rotation = Quaternion.Euler(rotator.eulerAngles.x + 0.1f, rotator.eulerAngles.y, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
          //  rotator.rotation = Quaternion.Euler(rotator.eulerAngles.x - 0.1f, rotator.eulerAngles.y, 0);

        }







        Vector3 mousepos = Input.mousePosition;
        mousepos.z = 1;
       // print(mousepos);

        Vector3 mouseposwrld = Camera.main.ScreenToWorldPoint(mousepos);

        //input
        mouse.position = new Vector3(mouseposwrld.x, mouseposwrld.y, 1);
        //   mouse.transform.LookAt(Vector3.zero);
        //  Ray ray = new Ray(mouse.position+(Camera.main.transform.position/5), mouse.transform.TransformDirection(Vector3.forward) * 0.1f);
        //    Ray ray = new Ray(mouse.position ,Camera.main.transform.forward*100);
        hand.transform.position = Camera.main.transform.position;
        hand.transform.position =new Vector3(hand.transform.position.x, mouseposwrld.y,hand.transform.position.z);
     Ray ray = new Ray(hand.transform.position, Camera.main.transform.forward * 100);
        Debug.DrawRay(hand.transform.position, Camera.main.transform.forward * 100, Color.red, 100);
        Physics.Raycast(ray, out hit);
    
        test = hit.point;
        hand.transform.LookAt(Vector3.zero);
        //    hand.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, hit.point.z);

      
       hand.transform.position =hit.point;
     if(hit.point != Vector3.zero)
        {
            hand.transform.position = hit.point;
        }
       
     //end
     vertices = main.vertices;

        //modification
        if (Input.GetKey(KeyCode.Mouse0)){

            for (int i = 0; i < vertices.Length; i++)
            {
                if (Vector3.Distance(workpiece.transform.TransformPoint(vertices[i]), hand.transform.position ) < brushsize)
                {

                    //   vertices[i] = vertices[i] - main.normals[i];
                    workedvert.position = workpiece.transform.TransformPoint(vertices[i]);
               workedvert.LookAt(workpiece.transform.position);
                    //      vertices[i] -= Vector3.Scale(Vector3.MoveTowards(workpiece.transform.TransformPoint(vertices[i]), workpiece.transform.position,intencity), vertices[i]) * Time.deltaTime ;
                    vertices[i] -= (vertices[i] / 2 )*Time.deltaTime;
                    //vertices[i] += Vector3.MoveTowards(workpiece.transform.TransformPoint(vertices[i]), workpiece.transform.position,intencity) *Time.deltaTime ;
                }
            }
        }
       

        //end
        main.vertices = vertices;

        workpiece.GetComponent<MeshCollider>().sharedMesh = main;
     
        //    main.RecalculateNormals();
    }
}
