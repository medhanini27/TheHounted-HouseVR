using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;
using SimpleJSON;
using UnityEngine.Networking;

public class Playsound : MonoBehaviour

{
    public static  string number="";
    public GameObject gamedone;
    private JSONNode lbData;

    public void Clicky()
    {
        GetComponent<AudioSource>().Play();

    }
    public void on1clicked()
    {
        GetComponent<AudioSource>().Play();

        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0,1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "1";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }
    public void on2clicked()
    {
        GetComponent<AudioSource>().Play();

        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0,1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "2";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }
    public void on3clicked()
    {
        GetComponent<AudioSource>().Play();

        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0,1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "3";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }

    public void on4clicked()
    {
        GetComponent<AudioSource>().Play();


        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0, 1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "4";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }
    public void on5clicked()
    {
        GetComponent<AudioSource>().Play();


        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0, 1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "5";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }

    public void on6clicked()
    {
        GetComponent<AudioSource>().Play();

        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0, 1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "6";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }
    public void on7clicked()
    {
        GetComponent<AudioSource>().Play();

        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0, 1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "7";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }
    public void on8clicked()
    {
        GetComponent<AudioSource>().Play();

        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0, 1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "8";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }
    public void on9clicked()
    {
        GetComponent<AudioSource>().Play();

        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0, 1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "9";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }
    public void on0clicked()
    {
        GetComponent<AudioSource>().Play();


        //Debug.Log(number.Length);
        if (Playsound.number.Length == 4)
        {
            //Debug.Log("trim");

            Playsound.number = Playsound.number.Remove(0, 1);
            //Debug.Log(number.Length+" hh");

        }
        Playsound.number = Playsound.number + "0";
        GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;


    }


    public void onvalidate()
    {
        GetComponent<AudioSource>().Play();

        if (Playsound.number== "4523")
        {
            StartCoroutine(ExampleCoroutine());
        }

    }
    public void oncancel()
    {
        GetComponent<AudioSource>().Play();

        Playsound.number="";
       GameObject.Find("keypadnbr").GetComponent<TextMeshProUGUI>().text = Playsound.number;

    }


    IEnumerator ExampleCoroutine()
    {

        if (Playsound.number == "4523")
        {
            gamedone.SetActive(true);
            StartCoroutine(FetchLeaderBoard());
        


        }


        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(3);

    }



    public IEnumerator FetchLeaderBoard()
    {
        string id = PlayerPrefs.GetString("id");

        WWWForm form = new WWWForm();
        form.AddField("id", id);

        WWW lbService = new WWW("http://41.226.11.252:11866/getid", form);
        yield return lbService;



        lbData = JSON.Parse(lbService.text);
        if(lbService.text == "0")
        {
            StartCoroutine(Upload());
        }


       
    }


   


    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();

        string id = PlayerPrefs.GetString("id");
        string username = PlayerPrefs.GetString("username");

        float score = PlayerPrefs.GetFloat("score");



        form.AddField("name", username);
        form.AddField("id", id);
        form.AddField("score", (int)score);

        using (UnityWebRequest www = UnityWebRequest.Post("http://41.226.11.252:11866/addscore", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
            }
        }
    }
}
