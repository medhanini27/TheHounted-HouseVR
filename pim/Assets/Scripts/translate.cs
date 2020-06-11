using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class translate : MonoBehaviour
{

    GameObject Player; //your hand
    GameObject Object1; //what your picking up
    bool isHolding = false;
    GameObject[] l;
    GameObject lever;
    GameObject hiddenwall1;
    GameObject hiddenwall2;
    GameObject stairs;


    bool leverup = true;
    bool leverdown = false;
    int a = 0;
    public float xPosition, yPosition, zPosition;
    int u = 0;
    bool y = true;
    int test;
    double dist = 5;
    GameObject door;
    //distance at which you can pick things up

    public Rigidbody rb;    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //l = GameObject.FindGameObjectsWithTag("topick");
        //Debug.Log(l.Length);
        Player = GameObject.FindGameObjectWithTag("Player");
        // lever = GameObject.FindGameObjectWithTag("lever");
        //hiddenwall1 = GameObject.FindGameObjectWithTag("hiddenwall1");
        //hiddenwall2 = GameObject.FindGameObjectWithTag("hiddenwall2");
        //stairs = GameObject.FindGameObjectWithTag("stairs");

        //door = GameObject.Find("dr R");
        //Debug.Log(door.transform.rotation.eulerAngles.y);



    }

    void Update()
    {
        bool isLeftButtonDown = Input.GetMouseButtonDown(0);
        bool isRightButtonDown = Input.GetMouseButtonDown(1);
        bool isMiddleButtonDown = Input.GetMouseButtonDown(2);

        float mouseXValue = Input.GetAxis("Mouse X");
float mouseYValue = Input.GetAxis("Mouse Y");
 
if(mouseXValue != 0)
{
 print("Mouse X movement: " + mouseXValue);
}
 
if(mouseYValue != 0)
{
 print("Mouse Y movement: " + mouseYValue);
}
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal") ;
        transform.Translate(0, 0, v * 7 * Time.deltaTime);
        transform.Rotate(0,h * 40 * Time.deltaTime, 0);



        /*if (Input.GetKeyDown(KeyCode.E))
        { //if you press 'E'
            for (int i = 0; i < l.Length; i++)
            {
                if (Vector3.Distance(Player.transform.position, l[i].transform.position) < dist) //if distance is less than dist variable
                {
                    isHolding = !isHolding; //changes isHolding var from false to true
                    Object1 = l[i];
                    Debug.Log("chosing  " + i);
                }



            }
            if (Vector3.Distance(Player.transform.position, lever.transform.position) < dist) //if distance is less than dist variable
            {
                while (leverup)
                { 
                lever.transform.Rotate(Time.deltaTime-10, 0,0);
                    a = a + 1;
                    if (a == 10) { leverup = false;
                }
                Debug.Log("rotate lever");
                }


            }


            if (Vector3.Distance(Player.transform.position, door.transform.position) < dist) //if distance is less than dist variable
            {
                Debug.Log("nearby");

                //if (-0.2066299 > door.transform.rotation.y && door.transform.rotation.y > -0.8379524)
                if (door.transform.rotation.eulerAngles.y < 1 && door.transform.rotation.eulerAngles.y > -1)

                {
                    Debug.Log("open");
                    test = 1;


                }
                else 
                    test = 2;

            }



        }

        if (isHolding == true)
        {

            //Object1.GetComponent<Rigidbody>().useGravity = false; //sets gravity to not on so it doesn't just fall to the ground
            Object1.transform.parent = Player.transform; //parents the object
                                                         //  Object1.transform.position = SpawnTo.transform.position; //sets position (makes it go crazy)
                                                         //  Object1.transform.rotation = SpawnTo.transform.rotation; //sets rotation  (makes it go crazy)
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isHolding = false;
            Player.transform.DetachChildren(); //detach child (object) from hand
            //Object1.GetComponent<Rigidbody>().useGravity = true; //add the gravity back on
        }



        if (u == 90) { y = false; }
        if (leverup == false)
        {
            if (y == true) { 
            hiddenwall1.transform.position += new Vector3(0.03f, 0, 0);
            hiddenwall2.transform.position -= new Vector3(0.03f, 0, 0);
                u  +=  1;

            }
        }



        if (test == 1 && door.transform.rotation.eulerAngles.y !=270 )
        {
            door.transform.Rotate(0, -1, 0);
            Debug.Log(door.transform.rotation.eulerAngles.y);
            stairs.transform.position += new Vector3(0, -0.03f, 0);


        }
        if (test == 2 && door.transform.rotation.eulerAngles.y > 1 )
        {
            door.transform.Rotate(0, 1, 0);
            Debug.Log(door.transform.rotation.eulerAngles.y);

        }*/


    }
    void FixedUpdate()
    {

}
}