using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EndGame : MonoBehaviour {

    private GameInfo gameInfo;
    private TextDisplay textDisplay;
    private Text score;
    private Text death;

	// Use this for initialization
	void Start () {
        try
        {
            gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
        }
        catch (Exception e)
        {
            Debug.Log("EndGame - NO GAME INFO OBJECT - setting default values" + e.Message);
        }
        if (gameInfo == null)
        {
            GameObject gameInfoObj = new GameObject("GameInfo");
            UnityEngine.Object.DontDestroyOnLoad(gameInfoObj); 
            gameInfoObj.AddComponent<GameInfo>();
            gameInfo = gameInfoObj.GetComponent<GameInfo>();
        }
        textDisplay = new TextDisplay();
        textDisplay.InitializeEndGameVariables();
        /*
         *     nejak zistit kto je kolkaty a priradit to...
               score.text = player.score.ToString();
               textDisplay.SetEndGameScoreValue(rank, player.score);
               death.text = player.death.ToString();
               textDisplay.SetEndGameDeathValue(rank, player.death);*/
    }

}
