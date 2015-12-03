using UnityEngine;
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

    public float mapMinX = -4.75f;
    public float mapMinY = -4.75f;
    public float mapMaxX = 8.6f;
    public float mapMaxY = 4f;

    public bool started;

    void Start()
    {
        prefab = (GameObject)Resources.Load("Prefabs/PlayerManagment"); 

        player1 = Instantiate(prefab); player1.SetActive(false);
        player2 = Instantiate(prefab); player2.SetActive(false);
        //player3 = Instantiate(prefab); player3.SetActive(false);
        player4 = Instantiate(prefab); player4.SetActive(false);

        //!!!!!!!!!!!!!player3 is reserved for ai
        aiPrefab = (GameObject)Resources.Load("Prefabs/AiManagement"); 
        player3 = Instantiate(aiPrefab); player3.SetActive(false);
        

        //set random positions for players
        for(int i = 1; i < 4; i++)
        {
            float initX = Random.Range(mapMinX, mapMaxX);
            float initY = Random.Range(mapMinY, mapMaxY);
            Vector2 initPosition = new Vector2(initX, initY);
            switch (i)
            {
                case 1:
                    //Debug.Log(player1.GetComponentInChildren<Transform>());
                    player1.transform.position = initPosition;
                    //                    player1.GetComponentInChildren<Transform>().position = initPosition;
                    break;
                case 2:
                    player2.transform.position = initPosition;
                    break;
                case 3:
                    player3.transform.position = initPosition;
                    break;
                case 4:
                    player4.transform.position = initPosition;
                    break;

                default: break;
            }
        }
        //manualni přiřazení
        Vector2 pl1pos = new Vector2(5,1);
        player1.transform.position = pl1pos;

        Vector2 pl2pos = new Vector2(6, 0);
        player2.transform.position = pl2pos;

        Vector2 pl3pos = new Vector2(-4, 0);
        player3.transform.position = pl3pos;

        Vector2 pl4pos = new Vector2(5, 2);
        player4.transform.position = pl4pos;

        //----------------

        player1Comp = player1.transform.GetChild(0).GetComponent<Player1>();
        player2Comp = player2.transform.GetChild(0).GetComponent<Player1>();
        //player3Comp = player3.transform.GetChild(0).GetComponent<Player1>();
        player4Comp = player4.transform.GetChild(0).GetComponent<Player1>();

        player3Comp = player3.transform.GetChild(0).GetComponent<Ai_03>(); //zatim 03...

        //SetUpPlayer(1, CharacterEnum.Curie);
        SetUpPlayer(2, CharacterEnum.Einstein);
        SetUpPlayer(3, CharacterEnum.Einstein);
        SetUpPlayer(4, CharacterEnum.Tesla);


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
                player1Info.playerColor = "red";
                player1Comp.SetUpPlayer(player1Info, new ControlKeysP1());
                GameObject.Find("player_info_red").GetComponent<HUDBase>().SetUp(player1Comp);
                player1Active = true;
                break;
            case 2:
                player2Info.playerNumber = 2;
                player2Info.charEnum = charEnum;
                player2Info.playerColor = "green";
                player2Comp.SetUpPlayer(player2Info, new ControlKeysP1());
                GameObject.Find("player_info_green").GetComponent<HUDBase>().SetUp(player2Comp);
                player2Active = true;
                break;
            case 3:
                player3Info.playerNumber = 3;
                player3Info.charEnum = charEnum;
                player3Info.playerColor = "blue";
                player3Comp.SetUpPlayer(player3Info);
                GameObject.Find("player_info_blue").GetComponent<HUDBase>().SetUp(player3Comp);
                player3Active = true;
                break;
            case 4:
                player4Info.playerNumber = 4;
                player4Info.charEnum = charEnum;
                player4Info.playerColor = "yellow";
                player4Comp.SetUpPlayer(player4Info, new ControlKeysP2());
                GameObject.Find("player_info_yellow").GetComponent<HUDBase>().SetUp(player4Comp);
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
