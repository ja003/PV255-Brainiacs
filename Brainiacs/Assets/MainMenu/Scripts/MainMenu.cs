using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;

    public Texture newGameHeader;
    public Texture headerTexture;
    public Texture controlsHeader;
    public Texture controls;
    public Texture portraitBackground;

    public Texture p1Text;
    public Texture p2Text;
    public Texture p3Text;
    public Texture p4Text;

    public Texture selectBackground;

    //0 - currie, 1 - davinci, 2 - einstein, 3 - nobel, 4 - tesla, 5 - random
    public List<Texture> heads;
    public List<Texture> backgrounds;
    public List<Texture> classes;
    //0 - player, 1 - AI, 2 - none
    public List<Texture> playerType;
    //0 - time, 1 - score, 2 - deathmatch
    public List<GUIStyle> gameMode;
    public List<GUIStyle> activeGameMode;
    //0 - steampunk, 1 - wonderland
    public List<Texture> maps;
    public List<Texture> mapSelect;

    private int p1Class = 1;
    private int p2Class = 2;
    private int p3Class = 3;
    private int p4Class = 4;

    private int p1Type = 0;
    private int p2Type = 0;
    private int p3Type = 0;
    private int p4Type = 0;

    private int selectedMode = 0;
    private int selectedMap = 0;

    private GUIStyle activeTimeText;
    private GUIStyle activeScoreText;
    private GUIStyle activeDeathText;

    public GUIStyle newGameTexture;
    public GUIStyle controlsTexture;
    public GUIStyle quitTexture;
    public GUIStyle backTexture;
    public GUIStyle startTexture;
    public GUIStyle nextTexture;
    public GUIStyle previousTexture;
    public GUIStyle inputField;

    public float playerTextX;
    public float playerTextY;

    public float headYPosition;
    public float classYPosition;
    public float typeYPosition;
    public float selectYPosition;
    public float selectOptionYDistance;

    public float headerYCoord;
    public float headerHeight;

    public float newGamePlacementX;
    public float newGamePlacementY;
    public float newGameHeight;
    public float distanceBetweenButtons;
    public float backButtonPlacementY;

    public float controlsHeaderX;
    public float controlsHeaderY;
    public float controlsHeaderHeight;

    private float cameraPositionY = 0.0f;

    private bool moveToControls = false;
    private bool moveToNewGame = false;
    private bool moveDownToMenu = false;
    private bool moveUpToMenu = false;
    private float movingSpeed = 0.01f;

    private string inputValue = "10";

    private AudioClip bgMusic;

    //Game info
    private GameObject gameInfoObj;

    void Start()
    {
        activeTimeText = new GUIStyle();
        activeScoreText = new GUIStyle();
        activeDeathText = new GUIStyle();
        activeTimeText = activeGameMode[0];
        activeScoreText = gameMode[1];
        activeDeathText = gameMode[2];
        inputField.fontSize = inputField.normal.background.height / 3;

        //for now
        bgMusic = Resources.Load("Sounds/BackgroundMusic/map_" + "steampunk") as AudioClip;

        SoundManager.instance.StartBackgroundMusic(bgMusic);

        gameInfoObj = GameObject.Find("GameInfo");
    }

    void OnGUI()
    {
        //displays background texture
        GUI.DrawTexture(new Rect(0, (cameraPositionY - 1) * Screen.height, Screen.width, 3 * Screen.height), backgroundTexture);

        float buttonX = Screen.width * newGamePlacementX;
        float buttonWidth = (Screen.width) - (Screen.width * newGamePlacementX * 2);
        float buttonHeight = Screen.height * newGameHeight;

        float portBackX = backgrounds[0].width * 1.2f * Screen.width / 1920.0f;
        float portBackY = backgrounds[0].height * 1.2f * Screen.height / 1080.0f;
        float portBackSpace = (classYPosition - (playerTextY + (p1Text.height / 1080.0f)) - (backgrounds[0].height * 1.2f / 1080.0f)) / 2.0f;
        float headBackSpace = (classYPosition - (playerTextY + (p1Text.height / 1080.0f)) - (heads[0].height / 1080.0f)) / 2.0f;
        float playerTX = playerTextX * Screen.width;
        float playerTextWidth = p1Text.width * Screen.width / 1920.0f;
        float playerTextHeight = p1Text.height * Screen.height / 1080.0f;
        float headWidth = heads[0].width * Screen.width / 1920.0f;
        float headHeight = heads[0].height * Screen.height / 1080.0f;
        float classWidth = classes[0].width * Screen.width / 1920.0f;
        float classHeight = classes[0].height * Screen.height / 1080.0f;
        float nextWidth = nextTexture.normal.background.width * (Screen.width / 1920.0f);
        float nextHeight = nextTexture.normal.background.height * (Screen.height / 1080.0f);
        float nextClassPosX = (classes[0].width + 10.0f) * Screen.width / (1920.0f * 2.0f);
        float nextClassPosY = (classHeight - nextHeight) / (1080.0f * 2.0f);
        float selectWidth = selectBackground.width * Screen.width / 1920.0f;
        float selectHeight = selectBackground.height * Screen.height / 1080.0f;
        float selectOptionWidth = activeScoreText.normal.background.width * Screen.width / 1920.0f;
        float selectOptionHeight = activeScoreText.normal.background.height * Screen.height / 1080.0f;
        float activeMapWidth = maps[0].width * Screen.width / 1920.0f;
        float activeMapHeigh = maps[0].height * Screen.height / 1080.0f;
        float activeMapYPos = (selectHeight - activeMapHeigh) / (2.0f * Screen.height);
        float mapSelectWidth = mapSelect[0].width * Screen.width / 1920.0f;
        float mapSelectHeight = mapSelect[0].height * Screen.height / 1080.0f;
        float mapSelectYPos = ((Screen.height * selectYPosition) + selectHeight) + ((Screen.height * backButtonPlacementY - ((Screen.height * selectYPosition) + selectHeight + mapSelectHeight)) / 2.0f);
        float nextMapXPos = (mapSelect[0].width + 10.0f) * Screen.width / (1920.0f * 2.0f);
        float nextMapYDif = (mapSelect[0].height - nextTexture.normal.background.height) * Screen.height / (1080.0f * 2.0f);
        float inputWidth = inputField.normal.background.width * Screen.width / 1920.0f;
        float nextInputXPos = (inputField.normal.background.width + 10.0f) * Screen.width / (1920.0f * 2.0f);
        //-------------------------------NEW GAME SCREEN-------------------------------
        GUI.DrawTexture(new Rect(Screen.width * controlsHeaderX, Screen.height * (-1 + cameraPositionY), Screen.width * (1.0f - controlsHeaderX * 2.0f), Screen.height * controlsHeaderHeight), newGameHeader);

        //player <number> plates 
        GUI.DrawTexture(new Rect((Screen.width / 8) - playerTX, Screen.height * (-1 + cameraPositionY + playerTextY), playerTextWidth, playerTextHeight), p1Text);
        GUI.DrawTexture(new Rect((3 * Screen.width / 8) - playerTX, Screen.height * (-1 + cameraPositionY + playerTextY), playerTextWidth, playerTextHeight), p2Text);
        GUI.DrawTexture(new Rect((5 * Screen.width / 8) - playerTX, Screen.height * (-1 + cameraPositionY + playerTextY), playerTextWidth, playerTextHeight), p3Text);
        GUI.DrawTexture(new Rect((7 * Screen.width / 8) - playerTX, Screen.height * (-1 + cameraPositionY + playerTextY), playerTextWidth, playerTextHeight), p4Text);

        //portrait background
        GUI.DrawTexture(new Rect((Screen.width / 8) - (portBackX / 2), Screen.height * (-1 + cameraPositionY + portBackSpace + playerTextY) + playerTextHeight, portBackX, portBackY), backgrounds[0]);
        GUI.DrawTexture(new Rect((3 * Screen.width / 8) - (portBackX / 2), Screen.height * (-1 + cameraPositionY + portBackSpace + playerTextY) + playerTextHeight, portBackX, portBackY), backgrounds[1]);
        GUI.DrawTexture(new Rect((5 * Screen.width / 8) - (portBackX / 2), Screen.height * (-1 + cameraPositionY + portBackSpace + playerTextY) + playerTextHeight, portBackX, portBackY), backgrounds[3]);
        GUI.DrawTexture(new Rect((7 * Screen.width / 8) - (portBackX / 2), Screen.height * (-1 + cameraPositionY + portBackSpace + playerTextY) + playerTextHeight, portBackX, portBackY), backgrounds[2]);

        //heads
        GUI.DrawTexture(new Rect((Screen.width / 8) - (headWidth / 2.0f), Screen.height * (-1 + cameraPositionY + headBackSpace + playerTextY) + playerTextHeight, headWidth, headHeight), heads[p1Class]);
        GUI.DrawTexture(new Rect((3 * Screen.width / 8) - (headWidth / 2.0f), Screen.height * (-1 + cameraPositionY + headBackSpace + playerTextY) + playerTextHeight, headWidth, headHeight), heads[p2Class]);
        GUI.DrawTexture(new Rect((5 * Screen.width / 8) - (headWidth / 2.0f), Screen.height * (-1 + cameraPositionY + headBackSpace + playerTextY) + playerTextHeight, headWidth, headHeight), heads[p3Class]);
        GUI.DrawTexture(new Rect((7 * Screen.width / 8) - (headWidth / 2.0f), Screen.height * (-1 + cameraPositionY + headBackSpace + playerTextY) + playerTextHeight, headWidth, headHeight), heads[p4Class]);

        //classes
        GUI.DrawTexture(new Rect((Screen.width / 8) - (classWidth / 2.0f), Screen.height * (-1 + cameraPositionY + classYPosition), classWidth, classHeight), classes[p1Class]);
        GUI.DrawTexture(new Rect((3 * Screen.width / 8) - (classWidth / 2.0f), Screen.height * (-1 + cameraPositionY + classYPosition), classWidth, classHeight), classes[p2Class]);
        GUI.DrawTexture(new Rect((5 * Screen.width / 8) - (classWidth / 2.0f), Screen.height * (-1 + cameraPositionY + classYPosition), classWidth, classHeight), classes[p3Class]);
        GUI.DrawTexture(new Rect((7 * Screen.width / 8) - (classWidth / 2.0f), Screen.height * (-1 + cameraPositionY + classYPosition), classWidth, classHeight), classes[p4Class]);

        //next classes
        if (GUI.Button(new Rect((Screen.width / 8) + nextClassPosX, Screen.height * (-1 + cameraPositionY + classYPosition + nextClassPosY), nextWidth, nextHeight), "", nextTexture))
        {
            p1Class = NextClass(p1Class);
        }
        if (GUI.Button(new Rect((3 * Screen.width / 8) + nextClassPosX, Screen.height * (-1 + cameraPositionY + classYPosition + nextClassPosY), nextWidth, nextHeight), "", nextTexture))
        {
            p2Class = NextClass(p2Class);
        }
        if (GUI.Button(new Rect((5 * Screen.width / 8) + nextClassPosX, Screen.height * (-1 + cameraPositionY + classYPosition + nextClassPosY), nextWidth, nextHeight), "", nextTexture))
        {
            p3Class = NextClass(p3Class);
        }
        if (GUI.Button(new Rect((7 * Screen.width / 8) + nextClassPosX, Screen.height * (-1 + cameraPositionY + classYPosition + nextClassPosY), nextWidth, nextHeight), "", nextTexture))
        {
            p4Class = NextClass(p4Class);
        }

        //previous classes
        if (GUI.Button(new Rect((Screen.width / 8) - nextClassPosX - nextWidth, Screen.height * (-1 + cameraPositionY + classYPosition + nextClassPosY), nextWidth, nextHeight), "", previousTexture))
        {
            p1Class = PreviousClass(p1Class);
        }
        if (GUI.Button(new Rect((3 * Screen.width / 8) - nextClassPosX - nextWidth, Screen.height * (-1 + cameraPositionY + classYPosition + nextClassPosY), nextWidth, nextHeight), "", previousTexture))
        {
            p2Class = PreviousClass(p2Class);
        }
        if (GUI.Button(new Rect((5 * Screen.width / 8) - nextClassPosX - nextWidth, Screen.height * (-1 + cameraPositionY + classYPosition + nextClassPosY), nextWidth, nextHeight), "", previousTexture))
        {
            p3Class = PreviousClass(p3Class);
        }
        if (GUI.Button(new Rect((7 * Screen.width / 8) - nextClassPosX - nextWidth, Screen.height * (-1 + cameraPositionY + classYPosition + nextClassPosY), nextWidth, nextHeight), "", previousTexture))
        {
            p4Class = PreviousClass(p4Class);
        }

        //player types
        GUI.DrawTexture(new Rect((Screen.width / 8) - (classWidth / 2.0f), Screen.height * (-1 + cameraPositionY + typeYPosition), classWidth, classHeight), playerType[p1Type]);
        GUI.DrawTexture(new Rect((3 * Screen.width / 8) - (classWidth / 2.0f), Screen.height * (-1 + cameraPositionY + typeYPosition), classWidth, classHeight), playerType[p2Type]);
        GUI.DrawTexture(new Rect((5 * Screen.width / 8) - (classWidth / 2.0f), Screen.height * (-1 + cameraPositionY + typeYPosition), classWidth, classHeight), playerType[p3Type]);
        GUI.DrawTexture(new Rect((7 * Screen.width / 8) - (classWidth / 2.0f), Screen.height * (-1 + cameraPositionY + typeYPosition), classWidth, classHeight), playerType[p4Type]);

        //next player types
        if (GUI.Button(new Rect((Screen.width / 8) + nextClassPosX, Screen.height * (-1 + cameraPositionY + typeYPosition + nextClassPosY), nextWidth, nextHeight), "", nextTexture))
        {
            p1Type = NextPlayerType(p1Type);
        }
        if (GUI.Button(new Rect((3 * Screen.width / 8) + nextClassPosX, Screen.height * (-1 + cameraPositionY + typeYPosition + nextClassPosY), nextWidth, nextHeight), "", nextTexture))
        {
            p2Type = NextPlayerType(p2Type);
        }
        if (GUI.Button(new Rect((5 * Screen.width / 8) + nextClassPosX, Screen.height * (-1 + cameraPositionY + typeYPosition + nextClassPosY), nextWidth, nextHeight), "", nextTexture))
        {
            p3Type = NextPlayerType(p3Type);
        }
        if (GUI.Button(new Rect((7 * Screen.width / 8) + nextClassPosX, Screen.height * (-1 + cameraPositionY + typeYPosition + nextClassPosY), nextWidth, nextHeight), "", nextTexture))
        {
            p4Type = NextPlayerType(p4Type);
        }

        //previous player types
        if (GUI.Button(new Rect((Screen.width / 8) - nextClassPosX - nextWidth, Screen.height * (-1 + cameraPositionY + typeYPosition + nextClassPosY), nextWidth, nextHeight), "", previousTexture))
        {
            p1Type = PreviousPlayerType(p1Type);
        }
        if (GUI.Button(new Rect((3 * Screen.width / 8) - nextClassPosX - nextWidth, Screen.height * (-1 + cameraPositionY + typeYPosition + nextClassPosY), nextWidth, nextHeight), "", previousTexture))
        {
            p2Type = PreviousPlayerType(p2Type);
        }
        if (GUI.Button(new Rect((5 * Screen.width / 8) - nextClassPosX - nextWidth, Screen.height * (-1 + cameraPositionY + typeYPosition + nextClassPosY), nextWidth, nextHeight), "", previousTexture))
        {
            p3Type = PreviousPlayerType(p3Type);
        }
        if (GUI.Button(new Rect((7 * Screen.width / 8) - nextClassPosX - nextWidth, Screen.height * (-1 + cameraPositionY + typeYPosition + nextClassPosY), nextWidth, nextHeight), "", previousTexture))
        {
            p4Type = PreviousPlayerType(p4Type);
        }

        //selects
        GUI.DrawTexture(new Rect((Screen.width / 4) - (selectWidth / 2.0f), Screen.height * (-1 + cameraPositionY + selectYPosition), selectWidth, selectHeight), selectBackground);
        GUI.DrawTexture(new Rect((3 * Screen.width / 4) - (selectWidth / 2.0f), Screen.height * (-1 + cameraPositionY + selectYPosition), selectWidth, selectHeight), selectBackground);

        if (GUI.Button(new Rect((Screen.width / 4) - (selectOptionWidth / 2.0f), Screen.height * (-1 + cameraPositionY + selectYPosition + (1 * selectOptionYDistance)), selectOptionWidth, selectOptionHeight), "", activeTimeText))
        {
            activeTimeText = activeGameMode[0];
            activeScoreText = gameMode[1];
            activeDeathText = gameMode[2];
            selectedMode = 0;
        }
        if (GUI.Button(new Rect((Screen.width / 4) - (selectOptionWidth / 2.0f), Screen.height * (-1 + cameraPositionY + selectYPosition + (2 * selectOptionYDistance) + (selectOptionHeight / Screen.height)), selectOptionWidth, selectOptionHeight), "", activeScoreText))
        {
            activeTimeText = gameMode[0];
            activeScoreText = activeGameMode[1];
            activeDeathText = gameMode[2];
            selectedMode = 1;
        }
        if (GUI.Button(new Rect((Screen.width / 4) - (selectOptionWidth / 2.0f), Screen.height * (-1 + cameraPositionY + selectYPosition + (3 * selectOptionYDistance) + (2 * selectOptionHeight / Screen.height)), selectOptionWidth, selectOptionHeight), "", activeDeathText))
        {
            activeTimeText = gameMode[0];
            activeScoreText = gameMode[1];
            activeDeathText = activeGameMode[2];
            selectedMode = 2;
        }

        //input field
        string tmp = inputValue;
        inputField.fontSize = Screen.height / 25;
        inputValue = GUI.TextField(new Rect((Screen.width / 4) - (inputWidth / 2.0f), Screen.height * (-1 + cameraPositionY) + mapSelectYPos, inputWidth, mapSelectHeight), inputValue, 3, inputField);
        if (!containsOnlyNumeric(inputValue))
        {
            inputValue = tmp;
        }
        if (GUI.Button(new Rect((Screen.width / 4) + nextInputXPos, Screen.height * (-1 + cameraPositionY) + mapSelectYPos + nextMapYDif, nextWidth, nextHeight), "", nextTexture))
        {
            inputValue = NextInputValue(inputValue);
        }
        if (GUI.Button(new Rect((Screen.width / 4) - nextInputXPos - nextWidth, Screen.height * (-1 + cameraPositionY) + mapSelectYPos + nextMapYDif, nextWidth, nextHeight), "", previousTexture))
        {
            inputValue = PreviousInputValue(inputValue);
        }

        //map select
        GUI.DrawTexture(new Rect((3 * Screen.width / 4) - (activeMapWidth / 2.0f), Screen.height * (-1 + cameraPositionY + selectYPosition + activeMapYPos), activeMapWidth, activeMapHeigh), maps[selectedMap]);
        GUI.DrawTexture(new Rect((3 * Screen.width / 4) - (mapSelectWidth / 2.0f), Screen.height * (-1 + cameraPositionY) + mapSelectYPos, mapSelectWidth, mapSelectHeight), mapSelect[selectedMap]);
        if (GUI.Button(new Rect((3 * Screen.width / 4) + nextMapXPos, Screen.height * (-1 + cameraPositionY) + mapSelectYPos + nextMapYDif, nextWidth, nextHeight), "", nextTexture))
        {
            selectedMap = NextMap(selectedMap);
        }
        if (GUI.Button(new Rect((3 * Screen.width / 4) - nextMapXPos - nextWidth, Screen.height * (-1 + cameraPositionY) + mapSelectYPos + nextMapYDif, nextWidth, nextHeight), "", previousTexture))
        {
            selectedMap = PreviousMap(selectedMap);
        }

        //startgame
        if (GUI.Button(new Rect(buttonX - (Screen.width / 4), Screen.height * (-1 + cameraPositionY + backButtonPlacementY), buttonWidth, buttonHeight), "", startTexture))
        {
            if (countInactivePlayers() > 2)
            {
                //play sound
            }
            else
            {
                string selectedMapName = "" + (MapEnum)selectedMap;
                StartGame(selectedMapName);
            }
        }
        if (GUI.Button(new Rect(buttonX + (Screen.width / 4), Screen.height * (-1 + cameraPositionY + backButtonPlacementY), buttonWidth, buttonHeight), "", backTexture))
        {
            moveToControls = false;
            moveToNewGame = false;
            moveDownToMenu = true;
            moveUpToMenu = false;
        }

        //-------------------------------MENU SCREEN-----------------------------------

        //displays header texture
        GUI.DrawTexture(new Rect(0, Screen.height * (headerYCoord + cameraPositionY), Screen.width, Screen.height * headerHeight), headerTexture);

        float newGameButtonY = cameraPositionY + newGamePlacementY;

        if (GUI.Button(new Rect(buttonX, Screen.height * newGameButtonY, buttonWidth, buttonHeight), "", newGameTexture))
        {
            moveToControls = false;
            moveToNewGame = true;
            moveDownToMenu = false;
            moveUpToMenu = false;
        }
        if (GUI.Button(new Rect(buttonX, Screen.height * (newGameButtonY + distanceBetweenButtons), buttonWidth, buttonHeight), "", controlsTexture))
        {
            moveToControls = true;
            moveToNewGame = false;
            moveDownToMenu = false;
            moveUpToMenu = false;
        }
        if (GUI.Button(new Rect(buttonX, Screen.height * (newGameButtonY + 2 * distanceBetweenButtons), buttonWidth, buttonHeight), "", quitTexture))
        {
            Application.Quit();
        }

        //-------------------------------CONTROLS SCREEN-------------------------------
        GUI.DrawTexture(new Rect(Screen.width * controlsHeaderX, Screen.height * (1 + controlsHeaderY + cameraPositionY), Screen.width * (1.0f - controlsHeaderX * 2.0f), Screen.height * controlsHeaderHeight), controlsHeader);
        
        GUI.DrawTexture(new Rect(0, Screen.height * (1 + cameraPositionY), Screen.width, Screen.height), controls);

        if (GUI.Button(new Rect(buttonX, Screen.height * (1 + cameraPositionY + backButtonPlacementY), buttonWidth, buttonHeight), "", backTexture))
        {
            moveToControls = false;
            moveToNewGame = false;
            moveDownToMenu = false;
            moveUpToMenu = true;
        }
    }

    void StartGame(string mapName)
    {
        SetUpGameInfo();

        Application.LoadLevel("Loading");
    }

    void SetUpGameInfo()
    {
        Object.DontDestroyOnLoad(gameInfoObj);
        GameInfo gameInfo = gameInfoObj.GetComponent<GameInfo>();

        gameInfo.mapName = "" + (MapEnum)selectedMap;

        gameInfo.player1char = GetPlayersCharacter(PlayerEnum.Player1);
        gameInfo.player1type = GetTypeOfPlayer(PlayerEnum.Player1);
        gameInfo.player2char = GetPlayersCharacter(PlayerEnum.Player2);
        gameInfo.player2type = GetTypeOfPlayer(PlayerEnum.Player2);
        gameInfo.player3char = GetPlayersCharacter(PlayerEnum.Player3);
        gameInfo.player3type = GetTypeOfPlayer(PlayerEnum.Player3);
        gameInfo.player4char = GetPlayersCharacter(PlayerEnum.Player4);
        gameInfo.player4type = GetTypeOfPlayer(PlayerEnum.Player4);

        DisableInactivePlayers(gameInfo);

        gameInfo.gameMode = GetGameMode();

        gameInfo.time = GetInputValue() * 60; //minutes
        gameInfo.winScore = GetInputValue();
        gameInfo.lifes = GetInputValue();
        
    }
    
    void DisableInactivePlayers(GameInfo gameInfo)
    {
        if (gameInfo.player1type == PlayerTypeEnum.None)
        {
            gameInfo.player1char = CharacterEnum.None;
            gameInfo.player1score = -1;
            gameInfo.player1deaths = -1;
        }
        if (gameInfo.player2type == PlayerTypeEnum.None)
        {
            gameInfo.player2char = CharacterEnum.None;
            gameInfo.player2score = -1;
            gameInfo.player2deaths = -1;
        }
        if (gameInfo.player3type == PlayerTypeEnum.None)
        {
            gameInfo.player3char = CharacterEnum.None;
            gameInfo.player3score = -1;
            gameInfo.player3deaths = -1;
        }
        if (gameInfo.player4type == PlayerTypeEnum.None)
        {
            gameInfo.player4char = CharacterEnum.None;
            gameInfo.player4score = -1;
            gameInfo.player4deaths = -1;
        }
    }

    void FixedUpdate()
    {
        if (moveToControls)
        {
            cameraPositionY -= movingSpeed;
            if (cameraPositionY < -0.99f)
            {
                cameraPositionY = -1.0f;
                moveToControls = false;
            }
        }

        if (moveToNewGame)
        {
            //Debug.Log("tmp");
            //StartGame("");

            cameraPositionY += movingSpeed;
            if (cameraPositionY > 0.99f)
            {
                cameraPositionY = 1.0f;
                moveToNewGame = false;
            }
        }

        if (moveDownToMenu)
        {
            cameraPositionY -= movingSpeed;
            if (cameraPositionY < 0.0f)
            {
                cameraPositionY = 0.0f;
                moveDownToMenu = false;
            }
        }

        if (moveUpToMenu)
        {
            cameraPositionY += movingSpeed;
            if (cameraPositionY > 0.0f)
            {
                cameraPositionY = 0.0f;
                moveUpToMenu = false;
            }
        }
    }

    private int NextClass(int p)
    {
        if (p + 1 == classes.Count)
        {
            return 0;
        }
        return p + 1;
    }

    private int PreviousClass(int p)
    {
        if (p == 0)
        {
            return classes.Count - 1;
        }
        return p - 1;
    }

    private int NextPlayerType(int p)
    {
        if (p + 1 == playerType.Count)
        {
            return 0;
        }
        return p + 1;
    }

    private int PreviousPlayerType(int p)
    {
        if (p == 0)
        {
            return playerType.Count - 1;
        }
        return p - 1;
    }

    private int NextMap(int m)
    {
        if (m + 1 == maps.Count)
        {
            return 0;
        }
        return m + 1;
    }

    private int PreviousMap(int m)
    {
        if (m == 0)
        {
            return maps.Count - 1;
        }
        return m - 1;
    }

    private string NextInputValue(string s)
    {
        if (s.Equals("99"))
        {
            return "1";
        }
        return (int.Parse(s) + 1).ToString();
    }

    private string PreviousInputValue(string s)
    {
        if (s.Equals("1"))
        {
            return "99";
        }
        return (int.Parse(s) - 1).ToString();
    }

    public CharacterEnum GetPlayersCharacter(PlayerEnum p)
    {
        switch (p)
        {
            case PlayerEnum.Player1:
                return GetCharacter(p1Class);
            case PlayerEnum.Player2:
                return GetCharacter(p2Class);
            case PlayerEnum.Player3:
                return GetCharacter(p3Class);
            case PlayerEnum.Player4:
                return GetCharacter(p4Class);
            default:
                Debug.Log("error in assigning class for " + p);
                return GetCharacter(5);
        }
    }

    private CharacterEnum GetCharacter(int p)
    {
        //Debug.Log(p + " = " + (CharacterEnum)p);
        if (p >= 0 && p < 5){
            return (CharacterEnum) p;
        }
        if (p == 5){
            return GetCharacter(Random.Range(0, 5));
        }
        Debug.Log("invalid character enum number " + p);
        return GetCharacter(Random.Range(0, 5));
    }

    public PlayerTypeEnum GetTypeOfPlayer(PlayerEnum p)
    {
        switch (p)
        {
            case PlayerEnum.Player1:
                return (PlayerTypeEnum)p1Type;
            case PlayerEnum.Player2:
                return (PlayerTypeEnum)p2Type;
            case PlayerEnum.Player3:
                return (PlayerTypeEnum)p3Type;
            case PlayerEnum.Player4:
                return (PlayerTypeEnum)p4Type;
            default:
                Debug.Log("Invalid PlayerEnum" + p);
                return PlayerTypeEnum.Player;
        }
    }

    public GameModeEnum GetGameMode(){
        return (GameModeEnum)selectedMode;
    }

    public int GetInputValue()
    {
        if (inputValue.Length == 0)
        {
            return 10;
        }
        return int.Parse(inputValue);
    }

    private bool containsOnlyNumeric(string s)
    {
        if (s.Length > 0)
        {
            if (s[0] == '0')
            {
                return false;
            }
        }
        
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] > '9' || s[i] < '0')
            {
                return false;
            }
        }
        return true;
    }

    private int countInactivePlayers()
    {
        int i = 0;
        if (GetTypeOfPlayer(PlayerEnum.Player1) == PlayerTypeEnum.None)
        {
            i++;
        }
        if (GetTypeOfPlayer(PlayerEnum.Player2) == PlayerTypeEnum.None)
        {
            i++;
        }
        if (GetTypeOfPlayer(PlayerEnum.Player3) == PlayerTypeEnum.None)
        {
            i++;
        }
        if (GetTypeOfPlayer(PlayerEnum.Player4) == PlayerTypeEnum.None)
        {
            i++;
        }
        return i;
    }
}