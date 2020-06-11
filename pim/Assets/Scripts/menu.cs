using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using GoogleMobileAds.Api;

public class menu : MonoBehaviour
{
    //public Text Cscore;
    //public Text Nscore;
    public Button newGame;
    public Button loadGame;
    public TextMeshProUGUI mText;

    //ads
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private string APP_ID = "ca-app-pub-8391922098007752~4816199176";

    // Start is called before the first frame update
    void Start()
    {
    
        GetComponent<AudioSource>().Play();
        
        newGame.onClick.AddListener(ChangeScene);
        loadGame.onClick.AddListener(ChangeScene1);

        string id = PlayerPrefs.GetString("id");
        string username = PlayerPrefs.GetString("username");

        Image userimg = GameObject.Find("pic").GetComponent<Image>();
        StartCoroutine(LoadFBPicture(id, userimg));

        mText = GameObject.Find("textid").GetComponent<TextMeshProUGUI>();
        Debug.Log(mText);
        mText.text = username;


    }

    void ChangeScene() { SceneManager.LoadScene(2);
        
    }
    void ChangeScene1()
    {
        // SceneManager.LoadScene(1);
        string id = PlayerPrefs.GetString("id");

        Image userimg = GameObject.Find("pic").GetComponent<Image>();
        //TextMeshPro username = GameObject.Find("textid").GetComponent<TextMeshPro>();
        mText = GameObject.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
        mText.text = id;
        //username.text = "hhhhhhhh";
        StartCoroutine(LoadFBPicture(id, userimg));


    }
    // Update is called once per frame
    void Update()
    {
                    GetComponent<AudioSource>().Play();
        // Cscore.text = PlayerPrefs.GetInt("highScore") + "";
        // Nscore.text = PlayerPrefs.GetInt("NewScore") + "";
        
        


    }


    public IEnumerator LoadFBPicture(string fbId, Image userImg)
    {
        WWW downloadDp = new WWW("https://graph.facebook.com/" + fbId + "/picture?width=90&height=90");
        yield return downloadDp;
        Texture2D texture = new Texture2D(90, 90, TextureFormat.RGBA32, false);
        texture.filterMode = FilterMode.Bilinear;
        texture.LoadImage(downloadDp.bytes);
        if (userImg != null)
        {
            userImg.sprite = Sprite.Create(texture, new Rect(0, 0, 90, 90), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    

}

