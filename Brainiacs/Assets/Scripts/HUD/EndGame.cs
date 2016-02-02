using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EndGame : MonoBehaviour {

    private GameInfo gameInfo;
    private TextDisplay textDisplay;

	// Use this for initialization
	void Start () {
        try
        {
            gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
        }
        catch (Exception e)
        {
            Debug.Log("HUDBase - NO GAME INFO OBJECT - setting default values" + e.Message);
        }
        if (gameInfo == null)
        {
            GameObject gameInfoObj = new GameObject("GameInfo");
            UnityEngine.Object.DontDestroyOnLoad(gameInfoObj);

            gameInfoObj.AddComponent<GameInfo>();
            gameInfo = gameInfoObj.GetComponent<GameInfo>();
        }
        textDisplay = new TextDisplay();
        textDisplay.InitializeGameVariables(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
