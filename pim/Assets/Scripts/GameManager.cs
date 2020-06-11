using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public static int score=0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";

    }

    // Update is called once per frame
    void Update()
    {

        scoreText.text = GameManager.score + "";
    }
} //transform.Translate(0,0,0 5 * Time*deltaTime)
