using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hint : MonoBehaviour
{
public GameObject panel;

    // Start is called before the first frame update
    public void panelonOff()
{
        StartCoroutine(ExampleCoroutine());


    }
    IEnumerator ExampleCoroutine()
    {
        panel.SetActive(true);
       // GameObject.Find("titlepanel").GetComponent<TextMeshProUGUI>().text = "tessssst";

        yield return new WaitForSeconds(5);
        panel.SetActive(false);
        //GetComponent<Text>().text="tessssst";

    }

}
