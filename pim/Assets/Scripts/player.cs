using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;


public class player : MonoBehaviour {
    GameObject door;
    GameObject hand; //your hand
        GameObject[] l;
    public GameObject bat1;
    public GameObject doortodestroy;

    public GameObject bat2;
    public GameObject alert;
    public GameObject codetext;

    public GameObject bat3;
    public GameObject clocktime;
    public GameObject timeA;
    public GameObject securitytext;
        private Animation anim;

    public Material mat;
    Renderer rend;

    GameObject clock;
    GameObject laptop; //your hand
    GameObject Player; //your hand
    GameObject Object1;
    GameObject doortoopen;
    GameObject DoortoopenLeft;
    GameObject DoorCollider;
      bool y = true;
    int test;
    double dist = 1;
    double dist4 = 5;
    double dist6 = 4;
    int securityon = 1;
    int codepc = 0;
     int numB;

    //room 2 
    Rigidbody a;
    public Material blue;

    GameObject starting;
    MeshRenderer startingMeshRenderer;
    
    bool isHolding = false;

    public Camera camera;

    public bool blueon = false;
    public bool greenon = false;
    public bool redon = false;
    public bool whiteon = false;
    public bool purpleon = false;
    public bool orangeon = false;

    public CharacterController charactercontroler;
    public GameObject key;
    public bool haskey = false;
    public bool keytaken = false;

    public GameObject torch;
    public bool hastorch = false;
    public bool torchon = false;
    public GameObject torchlight;
    public TextMeshPro mText;



    void Start (){     
        laptop = GameObject.FindGameObjectWithTag("laptop");   
        clock = GameObject.FindGameObjectWithTag("clock");
        door = GameObject.FindGameObjectWithTag("beb");
        hand = GameObject.FindGameObjectWithTag("handy");
        l = GameObject.FindGameObjectsWithTag("topick");
        rend = laptop.GetComponent<Renderer>();
        rend.enabled = true;
        anim = gameObject.GetComponent<Animation>();
        doortoopen=GameObject.Find("Doortoopen");
        DoortoopenLeft = GameObject.Find("DoortoopenLeft");
        DoorCollider = GameObject.Find("DoorCollider");

        //room 2 

        a = GetComponent<Rigidbody>();
        camera = Camera.main;
        key = GameObject.Find("key");
        key.active = false;

        torch = GameObject.Find("torch");
        torch.active = false;

        torchlight = GameObject.Find("torchlight");
        torchlight.GetComponent<Light>().enabled = false;
        mText = GameObject.Find("Text (TMP)").GetComponent<TextMeshPro>();










    }
    float mainSpeed = 10.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    float camSens = 1f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun= 1.0f;
     
    void Update () {

if (Input.GetMouseButtonDown(0))
        {
            if (Vector3.Distance(hand.transform.position, clock.transform.position) < dist4) //if distance is less than dist variable
                {
                    Debug.Log(numB);
                   if(numB == 3){                            
                       clocktime.SetActive(true);
                       timeA.SetActive(true);
                       codepc = 1;}

                }
            if (Vector3.Distance(hand.transform.position, laptop.transform.position) < dist4) //if distance is less than dist variable
                {
                    Debug.Log(numB);
                   if(numB == 3)
                        {
                       securityon = 0;                       
                       securitytext.SetActive(true);
                       bat3.SetActive(false);
                        timeA.SetActive(false);
                       rend.material = mat;
                       }
                    else{
                         codetext.SetActive(false);
                    codetext.SetActive(true);
                    }


                }


            if (Vector3.Distance(hand.transform.position, door.transform.position) < dist6) //if distance is less than dist variable
            { 
                if (securityon == 0)
                {
                    //SceneManager.LoadScene(1);
                    doortoopen.transform.Rotate(0, 90, 0);
                    DoortoopenLeft.transform.Rotate(0,- 90, 0);
                    DoorCollider.SetActive(false);
                    Debug.Log("opening door");

                }
                else
                    {
                        alert.SetActive(false);
                        alert.SetActive(true);
                    }

            }
        }
        if (test == 1 && door.transform.rotation.eulerAngles.y !=270 )
        {
            door.transform.Rotate(0, -1, 0);
            Debug.Log(door.transform.rotation.eulerAngles.y);

        }
        if (test == 2 && door.transform.rotation.eulerAngles.y > 1 )
        {
            door.transform.Rotate(0, 1, 0);
            Debug.Log(door.transform.rotation.eulerAngles.y);

        }

if (Input.GetMouseButtonDown(0))
        {


            for (int i = 0; i < l.Length; i++)
            {
                if (Vector3.Distance(hand.transform.position, l[i].transform.position) < dist) //if distance is less than dist variable
                {
                                GetComponent<AudioSource>().Play();

                    Object1 = l[i];
                    numB=numB+1;
                Destroy(Object1.GetComponent<Rigidbody>());
                    Debug.Log(numB);
                }
                if (numB == 1){bat1.SetActive(true);}
                if (numB == 2){bat1.SetActive(false);
                            bat2.SetActive(true);}
                if (numB == 3){bat2.SetActive(false);
                            bat3.SetActive(true);}


            }
            
        }









        //float mousex = ((input.mouseposition.x / screen.width) - 0.5f) + 0.5f;
        //float mousey = ((input.mouseposition.y / screen.height) - 0.5f) + 0.5f;
        //transform.localrotation = quaternion.euler(new vector4(-1f * (mousey * 180f), mousex * 360f, transform.localrotation.z));
        //zdz
        lastMouse = Input.mousePosition - lastMouse;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse = Input.mousePosition;
        //Mouse  camera angle done.  

        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (Input.GetKey (KeyCode.LeftShift)){
            totalRun += Time.deltaTime;
            p  = p * totalRun * shiftAdd;
            p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
            p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
            p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
        }
        else{
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;
        }
       
        p = p * Time.deltaTime;
       Vector3 newPosition = transform.position;
        if (Input.GetKey(KeyCode.Space)){ //If player wants to move on X and Z axis only
            transform.Translate(p);
            newPosition.x = transform.position.x;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
        else{
            transform.Translate(p);
        }


        //room 2
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 fwd = transform.TransformDirection(Vector3.right);
        if (Input.GetKeyDown(KeyCode.B))
        {



            if (torchon == true)
            {
                torchlight.GetComponent<Light>().enabled = false;
                torchon = false;
            }
            else if (torchon == false && hastorch == true)
            {
                torchlight.GetComponent<Light>().enabled = true;
                torchon = true;
            }

          


        }
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200))
        {
            GameObject selectedObject = hit.collider.gameObject;

            if (selectedObject.name == "paperM" && torchon == true)
            { mText.text = "4523"; }
            else if (torchon == false || selectedObject.name != "paperM")
            { mText.text = "light may guide your way"; }

            //Debug.Log("hit" + selectedObject.name);
            //Destroy(selectedObject);
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("hit " + selectedObject.name);

                if (selectedObject.tag == "levermesh")
                {
                    Vector3 on = new Vector3(-40, 90, 0);
                    Vector3 of = new Vector3(90, 90, 0);
                    string color = selectedObject.name.Remove(selectedObject.name.Length - 1);
                    //Debug.Log(color);
                    if (GameObject.Find(color).transform.eulerAngles == of)

                    {
                        GameObject.Find(color + "light").GetComponent<Renderer>().material.color = new Color32(255, 247, 120, 255);
                        GameObject.Find(color).transform.eulerAngles = on;
                        Debug.Log("on");
                        typeof(player).GetField(color + "on").SetValue(this, true);


                    }
                    else
                    {
                        GameObject.Find(color + "light").GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                        GameObject.Find(color).transform.eulerAngles = of;
                        Debug.Log("of");
                        typeof(player).GetField(color + "on").SetValue(this, false);


                    }
                }

                else if (key.active == true && selectedObject.name == "keyframe")
                {

                    Debug.Log("kiiiiiiiiiiiii");


                    key.SetActive(false);
                    haskey = true;
                    keytaken = true;
                }
                else if (selectedObject.name == "door" && haskey == true)
                {
                    Vector3 r = new Vector3(0, -120, 0);
                    GameObject.Find("door").transform.eulerAngles = r;
                }

                else if (selectedObject.name == "drawer5")
                {
                    GameObject.Find("drawer5").transform.localPosition = new Vector3(-40, 0, 0);
                    torch.active = true;
                }
                else if (selectedObject.name == "torch")
                {
                    torch.active = false;
                    hastorch = true;

                }



            }
        }

        if(Input.GetKeyDown(KeyCode.L))

        { Debug.Log("orangeon " + orangeon + " blueon" + blueon); }

        if (orangeon == false && blueon == true && whiteon == true && purpleon == false && redon == true && greenon == false && keytaken == false)
        {
            //Vector3 r = new Vector3(0, -120, 0);
            //GameObject.Find("door").transform.eulerAngles = r;


            GameObject.Find("drawer3").transform.localPosition = new Vector3(-40, 0, 0);
            key.active = true;
            


        }













    }
     
    private Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey (KeyCode.Z)){
            p_Velocity += new Vector3(0, 0 , 1);
        }
        if (Input.GetKey (KeyCode.S)){
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey (KeyCode.Q)){
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey (KeyCode.D)){
            p_Velocity += new Vector3(1, 0, 0);
        }
        return p_Velocity;
    }
}
