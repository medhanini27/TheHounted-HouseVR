
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class FacebookScript : MonoBehaviour
{

    public Text FriendsText;
   
    private void Awake()
    {


        if (!FB.IsInitialized)
        {
            FB.Init(() =>
            {
                if (FB.IsInitialized)
                    FB.ActivateApp();
                ///else
                   // Debug.LogError("Couldn't initialize");
            },
            isGameShown =>
            {
                if (!isGameShown)
                    Time.timeScale = 0;
                else
                    Time.timeScale = 1;
            });
        }
        else
            FB.ActivateApp();
    }


    public void go()
    {
        SceneManager.LoadScene(3);
    }

    #region Login / Logout
    public void FacebookLogin()
    {
        var permissions = new List<string>() { "public_profile", "email" };



        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
            
            // Print current access token's User ID

            FB.API("me?fields=name", Facebook.Unity.HttpMethod.GET, GetFacebookData);


            PlayerPrefs.SetString("id", aToken.UserId);
            PlayerPrefs.Save();

            SceneManager.LoadScene(3);



            // Text username = GameObject.Find("Texteee").GetComponent<Text>();
            //username.text = aToken.UserId;

            //EditorUtility.DisplayDialog("token",aToken.UserId , "mriguel", "moch mriguel ");
            // Print current access token's granted permissions
            //foreach (string perm in aToken.Permissions)
            //{
            //    Debug.Log(perm);
            //}
        }
        else
        {
            FB.LogInWithReadPermissions(permissions, AuthCallback);
            // AccessToken class will have session details
            
           // Debug.Log("User cancelled login");
        }





    }
    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;

            // Print current access token's User ID

            FB.API("me?fields=name", Facebook.Unity.HttpMethod.GET, GetFacebookData);


            PlayerPrefs.SetString("id", aToken.UserId);
            PlayerPrefs.Save();

            SceneManager.LoadScene(3);
        }
        else
        {
            Debug.Log("User cancelled login");
        }
    }

    public void FacebookLogout()
    {
        FB.LogOut();
        SceneManager.LoadScene(0);
    }
    void GetFacebookData(Facebook.Unity.IGraphResult result)
    {
        string fbName = result.ResultDictionary["name"].ToString();

        PlayerPrefs.SetString("username", fbName);
        PlayerPrefs.Save();
    }
    #endregion

    public void FacebookShare()
    {
        FB.ShareLink(new System.Uri("https://resocoder.com"), "Check it out!",
            "Good programming tutorials lol!",
            new System.Uri("https://resocoder.com/wp-content/uploads/2017/01/logoRound512.png"));
    }

    #region Inviting
    public void FacebookGameRequest()
    {
        FB.AppRequest("Hey! Come and play this awesome game!", title: "Escape room VR",callback: GamerequestCallback);
    }

    //public void FacebookInvite()
    //{
    //    FB.Mobile.AppInvite(new System.Uri("https://play.google.com/store/apps/details?id=com.tappybyte.byteaway"));
    //}
    private void GamerequestCallback(IAppRequestResult result)
    {

        //Debug.Log("invi tba3thet " + (result.Cancelled&&result.RequestID!=null));

    }

    #endregion

    public void GetFriendsPlayingThisGame()
    {
        string query = "/me/friends";
        FB.API(query, HttpMethod.GET, result =>
        {
            var dictionary = (Dictionary<string, object>)Facebook.MiniJSON.Json.Deserialize(result.RawResult);
            var friendsList = (List<object>)dictionary["data"];
           /// Debug.Log(friendsList);
            FriendsText.text = string.Empty;
            foreach (var dict in friendsList)
                FriendsText.text += ((Dictionary<string, object>)dict)["name"];
        });
    }










}