using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class capsulesss : MonoBehaviour
{
    Rigidbody a;
    public Material blue;

    GameObject starting;
    MeshRenderer startingMeshRenderer;
    GameObject Player; //your hand
    GameObject Object1; //what your picking up
    double dist = 1; //distance at which you can pick things up

    bool isHolding = false;
    GameObject[] l;
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

    // Start is called before the first frame update
    void Start()
    {
        a = GetComponent<Rigidbody>();
        Player = GameObject.FindGameObjectWithTag("Player");
        camera = Camera.main;
        key = GameObject.Find("key");
        key.active = false;

        torch = GameObject.Find("torch");
        torch.active = false;

        torchlight = GameObject.Find("torchlight");
        torchlight.GetComponent<Light>().enabled = false;


        mText = GameObject.Find("Text (TMP)").GetComponent<TextMeshPro>();





    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);


        Vector3 fwd = transform.TransformDirection(Vector3.right);
        //Debug.DrawRay(ray, fwd * 100, Color.red);
        //GameObject.Find("bluelight").GetComponent<Renderer>().material.color = new Color32(255, 247, 120, 255);

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

            // float degrees = -50;
            // Vector3 to = new Vector3(degrees, lev1[i].transform.rotation.y, lev1[i].transform.rotation.z);
            // Quaternion a = new Quaternion(0, 0, 90, 0);
            // Vector3 r = new Vector3(90, 90, 0);

            //   Quaternion b = new Quaternion(90, 0, 90, 0);

            //// lev1[i].transform.rotation = a;
            // lev1[i].transform.eulerAngles = r;
            // //lev1[i].transform.Rotate(new Vector3(-40, lev1[i].transform.rotation.y, lev1[i].transform.rotation.z));
            // // lev1[i].transform.eulerAngles = Vector3.Lerp(lev1[i].transform.rotation.eulerAngles, to, Time.deltaTime);

            // //lev1[i].transform.rotation.Set(-40, lev1[i].transform.rotation.y , lev1[i].transform.rotation.z, 0);
            // //lev1[i].transform.rotation = Quaternion.Euler(-40, 90, 0);
            // // Debug.Log(lev1[i].name);


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
                        typeof(capsulesss).GetField(color + "on").SetValue(this, true);


                    }
                    else
                    {
                        GameObject.Find(color + "light").GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                        GameObject.Find(color).transform.eulerAngles = of;
                        Debug.Log("of");
                        typeof(capsulesss).GetField(color + "on").SetValue(this, false);


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

        if (orangeon == false && blueon == true && whiteon == true && purpleon == false && redon == true && greenon == false && keytaken == false)
        {
            //Vector3 r = new Vector3(0, -120, 0);
            //GameObject.Find("door").transform.eulerAngles = r;


            GameObject.Find("drawer3").transform.localPosition = new Vector3(-40, 0, 0);
            key.active = true;


        }



        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.Z))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        transform.Translate(p_Velocity);



        //     float h = Input.GetAxis("Horizontal");
        // float v = Input.GetAxis("Vertical");
        // //a.velocity = new Vector3(h * 20 * Time.deltaTime, 0, v * 20 * Time.deltaTime);
        //transform.Translate( v * Time.deltaTime*20, 0, -h  * Time.deltaTime*20);

        //charactercontroler.Move(move);
        // transform.position = move;
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        //float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        ///  transform.rotation = Quaternion.Euler(new Vector4(0, mouseX * 360f, 0));














    }
}
