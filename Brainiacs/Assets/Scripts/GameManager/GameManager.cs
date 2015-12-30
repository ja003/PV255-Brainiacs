using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    GameObject prefab;
    GameObject aiPrefab;

    public PlayerBase player1base;
    public PlayerBase player2base;
    public PlayerBase player3base;
    public PlayerBase player4base;

    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;

    bool player1Active = false;
    bool player2Active = false;
    bool player3Active = false;
    bool player4Active = false;

    Player player1Comp;
    Player player2Comp;
    Player player3Comp;
    Player player4Comp;

    Ai ai1Comp;
    Ai ai2Comp;
    Ai ai3Comp;
    Ai ai4Comp;


    PlayerInfo player1Info = new PlayerInfo();
    PlayerInfo player2Info = new PlayerInfo();
    PlayerInfo player3Info = new PlayerInfo();
    PlayerInfo player4Info = new PlayerInfo();

    public float mapMinX = -4.75f;
    public float mapMinY = -4.75f;
    public float mapMaxX = 8.6f;
    public float mapMaxY = 4f;

    public bool started;

    private GameInfo gameInfo;

    private int frameCountSinceLvlLoad;

    void Start()
    {
        frameCountSinceLvlLoad = 0;

        try {
            gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
        }catch(Exception e)
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

        SetUpBackgroundMusic("steampunk");
        
        LoadPlayersTypes();

        SetUpStartingPosition();        
        
        AssignCharacters();
        
        Run();
    }

    public void SetUpStartingPosition()
    {
        //set random positions for players
        for (int i = 1; i <= 4; i++)
        {
            float initX = UnityEngine.Random.Range(mapMinX, mapMaxX);
            float initY = UnityEngine.Random.Range(mapMinY, mapMaxY);
            Vector2 initPosition = new Vector2(initX, initY);
            switch (i)
            {
                case 1:
                    if(gameInfo.player1type != PlayerTypeEnum.None)
                        player1.transform.position = initPosition;
                    break;
                case 2:
                    if (gameInfo.player2type != PlayerTypeEnum.None)
                        player2.transform.position = initPosition;
                    break;
                case 3:
                    if (gameInfo.player3type != PlayerTypeEnum.None)
                        player3.transform.position = initPosition;
                    break;
                case 4:
                    if (gameInfo.player4type != PlayerTypeEnum.None)
                        player4.transform.position = initPosition;
                    break;

                default: break;
            }
        }
        //manualni přiřazení
        Vector2 pl1pos = new Vector2(0, 0);
        if (gameInfo.player1type != PlayerTypeEnum.None)
            player1.transform.position = pl1pos;

        Vector2 pl2pos = new Vector2(2, 0);
        if (gameInfo.player2type != PlayerTypeEnum.None)
            player2.transform.position = pl2pos;

        Vector2 pl3pos = new Vector2(-4, 0);
        if (gameInfo.player3type != PlayerTypeEnum.None)
            player3.transform.position = pl3pos;

        Vector2 pl4pos = new Vector2(2, -1);
        if (gameInfo.player4type != PlayerTypeEnum.None)
            player4.transform.position = pl4pos;
    }

    public void SetUpBackgroundMusic(string mapName)
    {
        //GameObject sm = GameObject.Find("SoundManager");
        //AudioSource[] audioSources = sm.GetComponents<AudioSource>();
        //0 = sound FX, 1 = bg music
        //AudioSource bgMusicAudioSource = audioSources[1];
        AudioClip bgMusic = Resources.Load("Sounds/BackgroundMusic/map_" + mapName) as AudioClip;        
        //bgMusicAudioSource.clip = bgMusic;
        //play it
        SoundManager.instance.StartBackgroundMusic(bgMusic);
    }

    public void AssignCharacters()
    {
        if(gameInfo.player1type != PlayerTypeEnum.None)
            SetUpPlayer(1, gameInfo.player1char);
        if (gameInfo.player2type != PlayerTypeEnum.None)
            SetUpPlayer(2, gameInfo.player2char);
        if (gameInfo.player3type != PlayerTypeEnum.None)
            SetUpPlayer(3, gameInfo.player3char);
        if (gameInfo.player4type != PlayerTypeEnum.None)
            SetUpPlayer(4, gameInfo.player4char);
    }

        

    public void LoadPlayersTypes()
    {
        prefab = (GameObject)Resources.Load("Prefabs/PlayerManagment");
        aiPrefab = (GameObject)Resources.Load("Prefabs/AiManagement");
        
        switch (gameInfo.player1type)
        {
            case PlayerTypeEnum.Player:
                player1 = Instantiate(prefab);
                player1Comp = player1.transform.GetChild(0).GetComponent<Player>();
                player1.SetActive(false);
                break;
            case PlayerTypeEnum.AI:
                player1 = Instantiate(aiPrefab);
                ai1Comp = player1.transform.GetChild(0).GetComponent<Ai>();
                player1.SetActive(false);
                break;
        }
        switch (gameInfo.player2type)
        {
            case PlayerTypeEnum.Player:
                player2 = Instantiate(prefab);
                player2Comp = player2.transform.GetChild(0).GetComponent<Player>();
                player2.SetActive(false);
                break;
            case PlayerTypeEnum.AI:
                player2 = Instantiate(aiPrefab);
                ai2Comp = player2.transform.GetChild(0).GetComponent<Ai>();
                player2.SetActive(false);
                break;
        }
        switch (gameInfo.player3type)
        {
            case PlayerTypeEnum.Player:
                player3 = Instantiate(prefab);
                player3Comp = player3.transform.GetChild(0).GetComponent<Player>();
                player3.SetActive(false);
                break;
            case PlayerTypeEnum.AI:
                player3 = Instantiate(aiPrefab);
                ai3Comp = player3.transform.GetChild(0).GetComponent<Ai>();
                player3.SetActive(false);
                break;
        }
        switch (gameInfo.player4type)
        {
            case PlayerTypeEnum.Player:
                player4 = Instantiate(prefab);
                player4Comp = player4.transform.GetChild(0).GetComponent<Player>();
                player4.SetActive(false);

                break;
            case PlayerTypeEnum.AI:
                player4 = Instantiate(aiPrefab);
                ai4Comp = player4.transform.GetChild(0).GetComponent<Ai>();
                player4.SetActive(false);

                break;
        }
        

        
        
        
    }

    public void SetUpPlayer(int playerNumber, CharacterEnum charEnum)
    {
        if (playerNumber < 1 || playerNumber > 4) return;
        switch (playerNumber)
        {
            case 1:
                player1Info.playerNumber = 1;
                player1Info.charEnum = charEnum;
                player1Info.playerColor = "red";
                player1Info.lifes = gameInfo.lifes;
                switch (gameInfo.player1type)
                {
                    case PlayerTypeEnum.Player:
                        //Debug.Log(player1Comp);               
                        //Debug.Log(player1Info);
                        player1Comp.SetUpPlayer(player1Info, new ControlKeysP1());
                        GameObject.Find("player_info_red").GetComponent<HUDBase>().SetUp(player1Comp);
                        player1Active = true;
                        break;
                    case PlayerTypeEnum.AI:
                        ai1Comp.SetUpPlayer(player1Info);
                        GameObject.Find("player_info_red").GetComponent<HUDBase>().SetUp(ai1Comp);
                        player1Active = true;
                        break;
                    case PlayerTypeEnum.None:
                        player1Active = false;
                        break;
                }
                break;
                
            case 2:
                player2Info.playerNumber = 2;
                player2Info.charEnum = charEnum;
                player2Info.playerColor = "green";
                player2Info.lifes = gameInfo.lifes;
                switch (gameInfo.player2type)
                {
                    case PlayerTypeEnum.Player:
                        player2Comp.SetUpPlayer(player2Info, new ControlKeysP2());
                        GameObject.Find("player_info_green").GetComponent<HUDBase>().SetUp(player2Comp); ;
                        player2Active = true;
                        break;
                    case PlayerTypeEnum.AI:
                        ai2Comp.SetUpPlayer(player2Info);
                        GameObject.Find("player_info_green").GetComponent<HUDBase>().SetUp(ai2Comp);
                        player2Active = true;
                        break;
                    case PlayerTypeEnum.None:
                        player2Active = false;
                        break;
                }
                break;
            case 3:
                player3Info.playerNumber = 3;
                player3Info.charEnum = charEnum;
                player3Info.playerColor = "blue";
                player3Info.lifes = gameInfo.lifes;
                switch (gameInfo.player3type)
                {
                    case PlayerTypeEnum.Player:
                        player3Comp.SetUpPlayer(player3Info, new ControlKeysP3());
                        GameObject.Find("player_info_blue").GetComponent<HUDBase>().SetUp(player3Comp);
                        player3Active = true;
                        break;
                    case PlayerTypeEnum.AI:
                        ai3Comp.SetUpPlayer(player3Info);
                        GameObject.Find("player_info_blue").GetComponent<HUDBase>().SetUp(ai3Comp);
                        player3Active = true;
                        break;
                    case PlayerTypeEnum.None:
                        player3Active = false;
                        break;
                }
                break;
            case 4:
                player4Info.playerNumber = 4;
                player4Info.charEnum = charEnum;
                player4Info.playerColor = "yellow";
                player4Info.lifes = gameInfo.lifes;
                switch (gameInfo.player4type)
                {
                    case PlayerTypeEnum.Player:
                        player4Comp.SetUpPlayer(player4Info, new ControlKeysP4());
                        GameObject.Find("player_info_yellow").GetComponent<HUDBase>().SetUp(player4Comp);
                        player4Active = true;
                        break;
                    case PlayerTypeEnum.AI:
                        ai4Comp.SetUpPlayer(player4Info);
                        GameObject.Find("player_info_yellow").GetComponent<HUDBase>().SetUp(ai4Comp);
                        player4Active = true;
                        break;
                    case PlayerTypeEnum.None:
                        player4Active = false;
                        break;
                }
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

    public List<PlayerBase> GetPlayers()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        List<PlayerBase> allPlayersBase = new List<PlayerBase>();
        foreach (GameObject obj in allPlayers)
        {
            allPlayersBase.Add(obj.GetComponent<PlayerBase>());
        }
        return allPlayersBase;
    }

    public void SetPlayers()
    {
        List<PlayerBase> allPlayers = new List<PlayerBase>();
        allPlayers = GetPlayers();
        foreach (PlayerBase player in allPlayers)
        {
            //Debug.Log(player);
            switch (player.playerNumber)
            {
                case 1:
                    player1base = player;
                    player1base.gameInfo = gameInfo;
                    break;
                case 2:
                    player2base = player;
                    player2base.gameInfo = gameInfo;
                    break;
                case 3:
                    player3base = player;
                    player3base.gameInfo = gameInfo;
                    break;
                case 4:
                    player4base = player;
                    player4base.gameInfo = gameInfo;
                    break;
                default:
                    Debug.Log(player.ToString() + " has no player number!");
                    break;
            }
        }
    }

    public void CheckLifes()
    {
        int alive = 0;

        if (player1base != null && player1base.lifes > 0)
            alive++;
        if (player2base != null && player2base.lifes > 0)
            alive++;
        if (player3base != null && player3base.lifes > 0)
            alive++;
        if (player4base != null && player4base.lifes > 0)
            alive++;

        if(alive <= 1)
        {
            Debug.Log("everybody dead");
            EndGame();
        }
    }

    

    public void EndGame()
    {
        Debug.Log("END");
        Application.LoadLevel("MainMenu");
    }


    void Update()
    {
        if(frameCountSinceLvlLoad == 5)
        {
            SetPlayers();
        }
        frameCountSinceLvlLoad++;
    }


}
