﻿using UnityEngine;
using System.Collections;

public class gm : MonoBehaviour {

    GameObject prefab;
    GameObject aiPrefab;

    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;

    bool player1Active = false;
    bool player2Active = false;
    bool player3Active = false;
    bool player4Active = false;

    Player1 player1Comp;
    Player1 player2Comp;

    Ai_03 player3Comp;
    Player1 player4Comp;

    PlayerInfo player1Info = new PlayerInfo();
    PlayerInfo player2Info = new PlayerInfo();
    PlayerInfo player3Info = new PlayerInfo();
    PlayerInfo player4Info = new PlayerInfo();

    public bool started;

    void Start()
    {
        prefab = (GameObject)Resources.Load("Prefabs/PlayerManagment"); 

        player1 = Instantiate(prefab); player1.SetActive(false);
        player2 = Instantiate(prefab); player2.SetActive(false);
        //player3 = Instantiate(prefab); player3.SetActive(false);
        //player4 = Instantiate(prefab); player4.SetActive(false);

        aiPrefab = (GameObject)Resources.Load("Prefabs/AiManagement"); 
        player3 = Instantiate(aiPrefab); player3.SetActive(false);

        //!!!!!!!!!!!!!player3 is reserved for ai

        player1Comp = player1.transform.GetChild(0).GetComponent<Player1>();
        player2Comp = player2.transform.GetChild(0).GetComponent<Player1>();
        //player3Comp = player3.transform.GetChild(0).GetComponent<Player1>();
        //player4Comp = player4.transform.GetChild(0).GetComponent<Player1>();

        player3Comp = player3.transform.GetChild(0).GetComponent<Ai_03>(); //zatim 03...

        SetUpPlayer(1, CharacterEnum.Einstein);
        SetUpPlayer(2, CharacterEnum.Nobel);
        SetUpPlayer(3, CharacterEnum.Tesla);
        

        Run();
    }

    public void SetUpPlayer(int playerNumber, CharacterEnum charEnum)
    {
        if (playerNumber < 1 || playerNumber > 4) return;
        switch (playerNumber)
        {
            case 1:
                player1Info.playerNumber = 1;
                player1Info.charEnum = charEnum;
                player1Info.playerColor = "Red";
                player1Comp.SetUpPlayer(player1Info, new ControlKeysP1());
                player1Active = true;
                break;
            case 2:
                player2Info.playerNumber = 2;
                player2Info.charEnum = charEnum;
                player2Info.playerColor = "Green";
                player2Comp.SetUpPlayer(player2Info, new ControlKeysP2());
                player2Active = true;
                break;
            case 3:
                player3Info.playerNumber = 3;
                player3Info.charEnum = charEnum;
                player3Info.playerColor = "Blue";
                player3Comp.SetUpPlayer(player3Info);
                player3Active = true;
                break;
            case 4:
                player4Info.playerNumber = 4;
                player4Info.charEnum = charEnum;
                player4Info.playerColor = "Yellow";
                player4Comp.SetUpPlayer(player4Info, new ControlKeys());
                player4Active = true;
                break;
        }
    }

    public void Run()
    {
        if (player1Active) player1.SetActive(true);
        started = player1Active;
        if (player2Active) player2.SetActive(true);
        if (player3Active) player3.SetActive(true);
        if (player4Active) player4.SetActive(true);
    }

    void FixedUpdate()
    {

    }
}