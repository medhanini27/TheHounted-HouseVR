using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Menuf : MonoBehaviour {

    public GameObject MainMenu;
    public GameObject Leaderboard;
    public Slider slider;
    public GameObject settingmenu;
    public Toggle SoundToggle;
    public GameObject slidernbr;
   

    public void Start()
    {
        if (PlayerPrefs.GetFloat("cameraspeed") == 0)
        {
            slidernbr.GetComponent<TextMeshProUGUI>().text = (slider.value * 100 / 30).ToString("0.0")+"%";

        }
        else

        {

            slider.value = PlayerPrefs.GetFloat("cameraspeed");
            slidernbr.GetComponent<TextMeshProUGUI>().text = (slider.value * 100 / 30).ToString("0.0") + "%";
        }
    }

    public void leaderboard() {
        //MainMenu.SetActive(false);
        Leaderboard.SetActive(true);
    }

    public void mainMenu()
    {
        MainMenu.SetActive(true);
        LBManager.Instance.DestroyLB();
        Leaderboard.SetActive(false);
    }

    public void UpdateMyScore() {
        StartCoroutine(ServerManager.Instance.UpdateToSever(FacebookManager.Instance.myFbId, Random.Range(0, 20)));
    }

    public void leaderboardoff()
    {
        Leaderboard.SetActive(false);
    }

    public void onslidechange()
    {

        Debug.Log(slider.value);
        slidernbr.GetComponent<TextMeshProUGUI>().text = (slider.value*100/30).ToString("0.0") + "%";
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
    }

    public void showsettingmenue()
    {
        settingmenu.SetActive(true);

    }
}