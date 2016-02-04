using UnityEngine;
using System.Collections;
using System;

public class ScoreboardManager : MonoBehaviour {

    private GameInfo gameInfo;
    private TextDisplay textDisplay;

    SpriteRenderer redKillsSingle;
    SpriteRenderer redKillsTenField;
    SpriteRenderer redKillsUnitField;
    SpriteRenderer redDeathsSingle;
    SpriteRenderer redDeathsTenField;
    SpriteRenderer redDeathsUnitField;
    SpriteRenderer redPortrait;

    SpriteRenderer greenKillsSingle;
    SpriteRenderer greenKillsTenField;
    SpriteRenderer greenKillsUnitField;
    SpriteRenderer greenDeathsSingle;
    SpriteRenderer greenDeathsTenField;
    SpriteRenderer greenDeathsUnitField;
    SpriteRenderer greenPortrait;

    SpriteRenderer blueKillsSingle;
    SpriteRenderer blueKillsTenField;
    SpriteRenderer blueKillsUnitField;
    SpriteRenderer blueDeathsSingle;
    SpriteRenderer blueDeathsTenField;
    SpriteRenderer blueDeathsUnitField;
    SpriteRenderer bluePortrait;

    SpriteRenderer yellowKillsSingle;
    SpriteRenderer yellowKillsTenField;
    SpriteRenderer yellowKillsUnitField;
    SpriteRenderer yellowDeathsSingle;
    SpriteRenderer yellowDeathsTenField;
    SpriteRenderer yellowDeathsUnitField;
    SpriteRenderer yellowPortrait;

    // Use this for initialization
    void Start () {

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
            GameObject gameInfoObj = new GameObject("GameInfo");
            gameInfoObj.AddComponent<GameInfo>();
            gameInfo = gameInfoObj.GetComponent<GameInfo>();
        }

        gameInfo.RefreshStats();
        InitializeScoreboardVariables();

        textDisplay = new TextDisplay();
        
        RefreshStats();
        DisplayPortraits();
    }
	

    public void RefreshStats()
    {
        textDisplay.DisplayNumberOn(redKillsSingle, redKillsTenField, redKillsUnitField, gameInfo.player1score);
        textDisplay.DisplayNumberOn(redDeathsSingle, redDeathsTenField, redDeathsUnitField, gameInfo.player1deaths);

        textDisplay.DisplayNumberOn(greenKillsSingle, greenKillsTenField, greenKillsUnitField, gameInfo.player2score);
        textDisplay.DisplayNumberOn(greenDeathsSingle, greenDeathsTenField, greenDeathsUnitField, gameInfo.player2deaths);

        textDisplay.DisplayNumberOn(blueKillsSingle, blueKillsTenField, blueKillsUnitField, gameInfo.player3score);
        textDisplay.DisplayNumberOn(blueDeathsSingle, blueDeathsTenField, blueDeathsUnitField, gameInfo.player3deaths);
        
        textDisplay.DisplayNumberOn(yellowKillsSingle, yellowKillsTenField, yellowKillsUnitField, gameInfo.player4score);
        textDisplay.DisplayNumberOn(yellowDeathsSingle, yellowDeathsTenField, yellowDeathsUnitField, gameInfo.player4deaths);
    }

    public void DisplayPortraits()
    {
        string path = "Sprites/HUD/";
        redPortrait.sprite = Resources.Load<Sprite>(path + gameInfo.player1char + "_portrait");
        greenPortrait.sprite = Resources.Load<Sprite>(path + gameInfo.player2char + "_portrait");
        bluePortrait.sprite = Resources.Load<Sprite>(path + gameInfo.player3char + "_portrait");
        yellowPortrait.sprite = Resources.Load<Sprite>(path + gameInfo.player4char + "_portrait");
    }



    public void InitializeScoreboardVariables()
    {
        redKillsSingle = GameObject.Find("red_kills_single_field").GetComponent<SpriteRenderer>();
        redKillsTenField = GameObject.Find("red_kills_ten_field").GetComponent<SpriteRenderer>();
        redKillsUnitField = GameObject.Find("red_kills_unit_field").GetComponent<SpriteRenderer>();
        redDeathsSingle = GameObject.Find("red_deaths_single_field").GetComponent<SpriteRenderer>();
        redDeathsTenField = GameObject.Find("red_deaths_ten_field").GetComponent<SpriteRenderer>();
        redDeathsUnitField = GameObject.Find("red_deaths_unit_field").GetComponent<SpriteRenderer>();
        redPortrait = GameObject.Find("red_portrait").GetComponent<SpriteRenderer>();

        greenKillsSingle = GameObject.Find("green_kills_single_field").GetComponent<SpriteRenderer>();
        greenKillsTenField = GameObject.Find("green_kills_ten_field").GetComponent<SpriteRenderer>();
        greenKillsUnitField = GameObject.Find("green_kills_unit_field").GetComponent<SpriteRenderer>();
        greenDeathsSingle = GameObject.Find("green_deaths_single_field").GetComponent<SpriteRenderer>();
        greenDeathsTenField = GameObject.Find("green_deaths_ten_field").GetComponent<SpriteRenderer>();
        greenDeathsUnitField = GameObject.Find("green_deaths_unit_field").GetComponent<SpriteRenderer>();
        greenPortrait = GameObject.Find("green_portrait").GetComponent<SpriteRenderer>();

        blueKillsSingle = GameObject.Find("blue_kills_single_field").GetComponent<SpriteRenderer>();
        blueKillsTenField = GameObject.Find("blue_kills_ten_field").GetComponent<SpriteRenderer>();
        blueKillsUnitField = GameObject.Find("blue_kills_unit_field").GetComponent<SpriteRenderer>();
        blueDeathsSingle = GameObject.Find("blue_deaths_single_field").GetComponent<SpriteRenderer>();
        blueDeathsTenField = GameObject.Find("blue_deaths_ten_field").GetComponent<SpriteRenderer>();
        blueDeathsUnitField = GameObject.Find("blue_deaths_unit_field").GetComponent<SpriteRenderer>();
        bluePortrait = GameObject.Find("blue_portrait").GetComponent<SpriteRenderer>();

        yellowKillsSingle = GameObject.Find("yellow_kills_single_field").GetComponent<SpriteRenderer>();
        yellowKillsTenField = GameObject.Find("yellow_kills_ten_field").GetComponent<SpriteRenderer>();
        yellowKillsUnitField = GameObject.Find("yellow_kills_unit_field").GetComponent<SpriteRenderer>();
        yellowDeathsSingle = GameObject.Find("yellow_deaths_single_field").GetComponent<SpriteRenderer>();
        yellowDeathsTenField = GameObject.Find("yellow_deaths_ten_field").GetComponent<SpriteRenderer>();
        yellowDeathsUnitField = GameObject.Find("yellow_deaths_unit_field").GetComponent<SpriteRenderer>();
        yellowPortrait = GameObject.Find("yellow_portrait").GetComponent<SpriteRenderer>();

    }
}
