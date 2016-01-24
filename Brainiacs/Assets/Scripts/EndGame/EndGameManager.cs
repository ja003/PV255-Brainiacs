using UnityEngine;
using System.Collections;
using System;

public class EndGameManager : MonoBehaviour
{

    private GameInfo gameInfo;
    private TextDisplay textDisplay;
    // Use this for initialization
    void Start()
    {
        try
        {
            gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
            //Debug.Log(gameInfo);
        }
        catch (Exception e)
        {
            Debug.Log("NO GAME INFO OBJECT - setting default values");
        }

        if (gameInfo == null)
        {
            GameObject gameInfoObj = new GameObject("GameInfo_tmp");
            gameInfoObj.AddComponent<GameInfo>();
            gameInfo = gameInfoObj.GetComponent<GameInfo>();
        }

        Debug.Log(gameInfo);

        textDisplay = new TextDisplay();
        textDisplay.InitializeEndGameVariables();

        DisplayResults();
    }

    void DisplayResults()
    {
        int redScore = gameInfo.player1score;
        int redDeaths = gameInfo.player1deaths;

        int greenScore = gameInfo.player2score;
        int greenDeaths = gameInfo.player2deaths;

        int blueScore = gameInfo.player3score;
        int blueDeaths = gameInfo.player3deaths;

        int yellowScore = gameInfo.player4score;
        int yellowDeaths = gameInfo.player4deaths;


        textDisplay.SetEndGameValues(
            redScore, redDeaths, 
            greenScore, greenDeaths, 
            blueScore, blueDeaths, 
            yellowScore, yellowDeaths);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
