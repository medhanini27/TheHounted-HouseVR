using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMoveController : MonoBehaviour {

	// PUBLIC
	public SimpleTouchController leftController;
	public SimpleTouchController rightController;
	public Transform headTrans;
	public float speedMovements = 5f;
	public float speedContinuousLook = 10f;
    ///rotation speed
	public float speedProgressiveLook ;

	// PRIVA
	private Rigidbody _rigidbody;
	[SerializeField] bool continuousRightController = true;

    //roomsss
    //public GameObject rotatearound;
    public GameObject hintebtn;
    public GameObject settingbtn;
    public Slider slider;
    public GameObject settingmenu;
    public Toggle SoundToggle;
    public GameObject slidernbr;

    public GameObject resumebtn;
    public GameObject pausebtn;
    public GameObject mainmenu;
    public GameObject keypad;
    public GameObject torchonoffbutton;
    public Camera maincam;
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
    double dist = 4;
    double dist4 = 1;
    double dist6 = 5;
    int securityon = 1;
    int codepc = 0;
    int numB;

    //room 2 
    RaycastHit hit;

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
    public bool inroom2 = false;
    public GameObject torch;
    public bool hastorch = false;
    public bool torchon = false;
    public GameObject torchlight;
    public TextMeshPro mText;
    public bool Doorclosed = true;
    //room3
    public int greenclick=0;
    public int blueclick=0;
    public int redclick=0;
    public GameObject greennbr;
    public GameObject bluennbr;
    public GameObject rednbr;
    GameObject doortoopenroom3;
    GameObject DoortoopenLeftroom3;
    GameObject DoorColliderroom3;
    bool Doorrom3open=false;
    float v = 0;
    Shader oldgreen;
    void Start()
    {
        laptop = GameObject.FindGameObjectWithTag("laptop");
        clock = GameObject.FindGameObjectWithTag("clock");
        door = GameObject.FindGameObjectWithTag("beb");
        hand = GameObject.FindGameObjectWithTag("handy");
        l = GameObject.FindGameObjectsWithTag("topick");
        rend = laptop.GetComponent<Renderer>();
        rend.enabled = true;
        anim = gameObject.GetComponent<Animation>();
        doortoopen = GameObject.Find("Doortoopen");
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


        //keypad.SetActive(false);


        //room3
        doortoopenroom3 = GameObject.Find("Doortoopenroom3");
        DoortoopenLeftroom3 = GameObject.Find("DoortoopenLeftroom3");
        DoorColliderroom3 = GameObject.Find("DoorColliderroom3");



        oldgreen= GameObject.Find("greenbtn").GetComponent<Renderer>().material.shader;
        Shader oldblue = GameObject.Find("bluebtn").GetComponent<Renderer>().material.shader;
        Shader oldred = GameObject.Find("redbtn").GetComponent<Renderer>().material.shader;

        if (PlayerPrefs.GetFloat("cameraspeed") == 0)
        {
            speedProgressiveLook = 10;
            slidernbr.GetComponent<TextMeshProUGUI>().text = (slider.value * 100 / 30).ToString("0.0") + "%";

        }
        else

        {

            slider.value= PlayerPrefs.GetFloat("cameraspeed");
            slidernbr.GetComponent<TextMeshProUGUI>().text = (slider.value * 100 / 30).ToString("0.0") + "%";

            speedProgressiveLook = PlayerPrefs.GetFloat("cameraspeed");
        }


        if (PlayerPrefs.GetInt("soundon") == 1)
        {
            SoundToggle.isOn = true;
            AudioListener.pause = false;

        }
        else

        {


            SoundToggle.isOn = false;
            AudioListener.pause = true;
        }


    }
    float mainSpeed = 10.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    float maxShift = 1000.0f; //Maximum speed when holdin gshift
    float camSens = 1f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;



    void Awake()
	{

        _rigidbody = GetComponent<Rigidbody>();
		rightController.TouchEvent += RightController_TouchEvent;
	}

	public bool ContinuousRightController
	{
		set{continuousRightController = value;}
	}

	void RightController_TouchEvent (Vector2 value)
	{
		if(!continuousRightController)
		{
			UpdateAim(value);
		}
	}

	void Update()
	{
       
            //GameObject.Find("greenbtn").GetComponent<Renderer>().material.shader = Shader.Find("Diffuse");



        // move
        _rigidbody.MovePosition(transform.position + (transform.forward * leftController.GetTouchPosition.y * Time.deltaTime * speedMovements) +
			(transform.right * leftController.GetTouchPosition.x * Time.deltaTime * speedMovements) );

		if(continuousRightController)
		{
			UpdateAim(rightController.GetTouchPosition);
		}
        if (Doorrom3open == false)
        { hintebtn.SetActive(false); }
        else { hintebtn.SetActive(true); }

        ///rooooomssss
        ///
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 fwd = transform.TransformDirection(Vector3.right);


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 200))
            {
                GameObject selectedObject = hit.collider.gameObject;
                Debug.Log(selectedObject.name);
                if (selectedObject==clock) //if distance is less than dist variable
                {
                    Debug.Log(numB);
                    if (numB == 3)
                    {
                        clocktime.SetActive(true);
                        timeA.SetActive(true);
                        codepc = 1;
                    }

                }
                if (selectedObject== GameObject.Find("notebookmesh")) //if distance is less than dist variable
                {
                    Debug.Log(numB);
                    if (numB >=3)
                    {
                        securityon = 0;
                        securitytext.SetActive(true);
                        bat3.SetActive(false);
                        timeA.SetActive(false);
                        rend.material = mat;
                        StartCoroutine(Wait());

                    }
                    else
                    {
                        codetext.SetActive(false);
                        codetext.SetActive(true);
                        StartCoroutine( Wait());

                    }


                }


                if (selectedObject.name== "DoorCollider") //if distance is less than dist variable
                {
                    if (securityon == 0 && Doorclosed == true)
                    {
                        //SceneManager.LoadScene(1);
                        doortoopen.transform.Rotate(0, 90, 0);
                        DoortoopenLeft.transform.Rotate(0, -90, 0);
                        DoorCollider.SetActive(false);
                        Doorclosed = false;
                        inroom2 = true;
                        Debug.Log("opening door");

                    }
                    else
                    {
                        alert.SetActive(false);
                        alert.SetActive(true);
                    }

                }
                for (int i = 0; i < l.Length; i++)
                {

                    if (selectedObject== l[i]) //if distance is less than dist variable
                    {
                        GetComponent<AudioSource>().Play();

                        Object1 = l[i];
                        numB = numB + 1;
                        //Destroy(Object1.GetComponent<Rigidbody>());
                        l[i].SetActive(false);
                        Debug.Log(numB);
                        if (numB == 1) { bat1.SetActive(true); }
                        if (numB == 2)
                        {
                            bat1.SetActive(false);
                            bat2.SetActive(true);
                        }
                        if (numB == 3)
                        {
                            bat2.SetActive(false);
                            bat3.SetActive(true);
                            StartCoroutine(Wait());
                        }


                    }



                }
            }
        }
        //if (test == 1 && door.transform.rotation.eulerAngles.y != 270)
        //{
        //    door.transform.Rotate(0, -1, 0);
        //    Debug.Log(door.transform.rotation.eulerAngles.y);

        //}
        //if (test == 2 && door.transform.rotation.eulerAngles.y > 1)
        //{
        //    door.transform.Rotate(0, 1, 0);
        //    Debug.Log(door.transform.rotation.eulerAngles.y);

        //}

  
        //Keyboard commands
        float f = 0.0f;




        //room 2

        Ray ray2 = new Ray(torchlight.transform.position, torchlight.transform.forward);
        if (Physics.Raycast(ray2, out hit, 200))
        {
            GameObject selectedObject = hit.collider.gameObject;
            Debug.DrawLine(ray2.origin, hit.point, Color.green);

            if (selectedObject.name == "paperM" && torchon == true)
            {
                mText.text = "4523";
            }
            else if (torchon == false || selectedObject.name != "paperM")
            {
                mText.text = "light may guide your way";
            }
        }

            if (Physics.Raycast(ray, out hit, 200))
        {
            GameObject selectedObject = hit.collider.gameObject;

            //if (selectedObject.name == "paperM" && torchon == true)
            //{ mText.text = "4523"; }
            //else if (torchon == false || selectedObject.name != "paperM")
            //{ mText.text = "light may guide your way"; }

            //Debug.Log("hit" + selectedObject.name);
            //Destroy(selectedObject);
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("hit " + selectedObject.name);

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
                        typeof(PlayerMoveController).GetField(color + "on").SetValue(this, true);


                    }
                    else
                    {
                        GameObject.Find(color + "light").GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
                        GameObject.Find(color).transform.eulerAngles = of;
                        Debug.Log("of");
                        typeof(PlayerMoveController).GetField(color + "on").SetValue(this, false);


                    }
                }

                else if (key.active == true && selectedObject.name == "keyframe")
                {

                    Debug.Log("kiiiiiiiiiiiii");


                    key.SetActive(false);
                    haskey = true;
                    keytaken = true;
                    GetComponent<AudioSource>().Play();

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
                    torchonoffbutton.active = true;
                    GetComponent<AudioSource>().Play();


                }
                else if (selectedObject.name == "keypadcube")
                {
                    keypad.SetActive(true);
                }
                else if(selectedObject.tag=="btn")
                {
                    if(selectedObject.name=="greenbtn")
                    {

                        if (greenclick == 9)
                        { greenclick = 0; }
                        else { greenclick++; }
                        greennbr.GetComponent<TextMeshProUGUI>().text = greenclick.ToString();

                    }
                    if (selectedObject.name == "bluebtn")
                    {
                        if (blueclick == 9)
                        { blueclick = 0; }
                        else { blueclick++; }
                        bluennbr.GetComponent<TextMeshProUGUI>().text = blueclick.ToString();

                    }
                    if (selectedObject.name == "redbtn")
                    {
                        if (redclick == 9)
                        { redclick = 0; }
                        else { redclick++; }
                        rednbr.GetComponent<TextMeshProUGUI>().text = redclick.ToString();

                    }

                }




            }
        }

        if (Input.GetKeyDown(KeyCode.L))

        { Debug.Log("orangeon " + orangeon + " blueon" + blueon); }

        if (orangeon == false && blueon == true && whiteon == true && purpleon == false && redon == true && greenon == false && keytaken == false)
        {
            //Vector3 r = new Vector3(0, -120, 0);
            //GameObject.Find("door").transform.eulerAngles = r;


            GameObject.Find("drawer3").transform.localPosition = new Vector3(-40, 0, 0);
            key.active = true;



        }
        if (redclick == 2 && blueclick==4 && greenclick == 3&& Doorrom3open==false)
        {
            doortoopenroom3.transform.Rotate(0, 90, 0);
            DoortoopenLeftroom3.transform.Rotate(0, -90, 0);
            DoorColliderroom3.SetActive(false);
            Doorrom3open = true;

        }
























    }


    public void torchonoff()
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

    void UpdateAim(Vector2 value)
	{
		if(headTrans != null)
		{
			Quaternion rot = Quaternion.Euler(0f,
				transform.localEulerAngles.y - value.x * Time.deltaTime * -speedProgressiveLook,
				0f);

			//_rigidbody.MoveRotation(rot);

			rot = Quaternion.Euler(headTrans.localEulerAngles.x - value.y * Time.deltaTime * speedProgressiveLook,
				0f,
				0f);
			//headTrans.localRotation = rot;

		}
		else
		{

			Quaternion rot = Quaternion.Euler(transform.localEulerAngles.x - value.y * speedProgressiveLook,
				transform.localEulerAngles.y + value.x *speedProgressiveLook,
				0f);

            //maincam.transform.rotation = rot;
            //_rigidbody.MoveRotation(rot);
            //transform.localRotation = rot;

            transform.localRotation = Quaternion.Slerp(transform.rotation,rot,  /*Time.deltaTime **/ 20f);



        }
    }

	void OnDestroy()
	{
		rightController.TouchEvent -= RightController_TouchEvent;
	}


    public GameObject panel;

    // Start is called before the first frame update
    public void panelonOff()
    {
        Debug.Log("hint tttt");
        StartCoroutine(ExampleCoroutine());


    }
    IEnumerator ExampleCoroutine()
    {
        panel.SetActive(true);
        if (inroom2)
        {
            string newLine = Environment.NewLine;
            GameObject.Find("titlepanel").GetComponent<TextMeshProUGUI>().text = "The mother of the arts, arms " + newLine + "and laws";
            GameObject.Find("subtitlepanel").GetComponent<TextMeshProUGUI>().text = "Monument can define a country" + newLine + "like a flag can too";
        }
                yield return new WaitForSeconds(5);
        panel.SetActive(false);

    }

    public void hidekeypad()
    {
        keypad.SetActive(false);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
        codetext.SetActive(false);
        securitytext.SetActive(false);
        bat3.SetActive(false);


    }

    public void pausegame()
    {

        Time.timeScale = 0;
        settingbtn.SetActive(true);
        resumebtn.SetActive(true);
        mainmenu.SetActive(true);

    }
    public void resumegame()
    {

        Time.timeScale = 1;
        settingbtn.SetActive(false);
        resumebtn.SetActive(false);
        mainmenu.SetActive(false);

    }
    public void backtomainmenu()
    {
        SceneManager.LoadScene(3);
    }

    public void onslidechange()
    {

        speedProgressiveLook = slider.value;
        slidernbr.GetComponent<TextMeshProUGUI>().text = (slider.value * 100 / 30).ToString("0.0") + "%";

        PlayerPrefs.SetFloat("cameraspeed", slider.value);
        PlayerPrefs.Save();

    }
    public void onsoundchange()
    {

        if (SoundToggle.isOn)
        {
            Debug.Log("soundon");

            AudioListener.pause = false;
            PlayerPrefs.SetInt("soundon", 1);

            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("soundof");

            AudioListener.pause = true;

            PlayerPrefs.SetInt("soundon", 0);
            PlayerPrefs.Save();
        }
    }
    public void hidesettingmenue()
    {
        settingmenu.SetActive(false);
        resumebtn.SetActive(true);
        mainmenu.SetActive(true);
        settingbtn.SetActive(true);

    }

    public void showsettingmenue()
    {
        settingmenu.SetActive(true);
        resumebtn.SetActive(false);
        mainmenu.SetActive(false);
        settingbtn.SetActive(false);


    }
}
