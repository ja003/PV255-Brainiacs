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
    public List<Texture> classes;
    //0 - player, 1 - AI, 2 - none
    public List<Texture> playerType;
    //0 - time, 1 - score, 2 - deathmatch
    public List<Texture> gameMode;
    public List<Texture> activeGameMode;
    //0 - steampunk
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

    private Texture activeTimeText;
    private Texture activeScoreText;
    private Texture activeDeathText;

    public GUIStyle newGameTexture;
    public GUIStyle controlsTexture;
    public GUIStyle quitTexture;
    public GUIStyle backTexture;
    public GUIStyle startTexture;
    public GUIStyle nextTexture;
    public GUIStyle previousTexture;

    public float portraitBackgroundX;
    public float portraitBackgroundY;

    public float playerTextX;
    public float playerTextY;

    public float headYPosition;
    public float classYPosition;
    public float typeYPosition;
    public float selectYPosition;

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

    void OnGUI()
    {
        //displays background texture
        GUI.DrawTexture(new Rect(0, (cameraPositionY - 1) * Screen.height, Screen.width, 3 * Screen.height), backgroundTexture);

        float buttonX = Screen.width * newGamePlacementX;
        float buttonWidth = (Screen.width) - (Screen.width * newGamePlacementX * 2);
        float buttonHeight = Screen.height * newGameHeight;

        float portBackX = portraitBackgroundX * Screen.width;
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
        float nextClassPosY = (classHeight - nextHeight) / (2.0f * Screen.width);
        float selectWidth = selectBackground.width * Screen.width / 1920.0f;
        float selectHeight = selectBackground.height * Screen.height / 1080.0f;
        //-------------------------------NEW GAME SCREEN-------------------------------
        GUI.DrawTexture(new Rect(Screen.width * controlsHeaderX, Screen.height * (-1 + cameraPositionY), Screen.width * (1.0f - controlsHeaderX * 2.0f), Screen.height * controlsHeaderHeight), newGameHeader);

        //player <number> plates 
        GUI.DrawTexture(new Rect((Screen.width / 8) - playerTX, Screen.height * (-1 + cameraPositionY + playerTextY), playerTextWidth, playerTextHeight), p1Text);
        GUI.DrawTexture(new Rect((3 * Screen.width / 8) - playerTX, Screen.height * (-1 + cameraPositionY + playerTextY), playerTextWidth, playerTextHeight), p2Text);
        GUI.DrawTexture(new Rect((5 * Screen.width / 8) - playerTX, Screen.height * (-1 + cameraPositionY + playerTextY), playerTextWidth, playerTextHeight), p3Text);
        GUI.DrawTexture(new Rect((7 * Screen.width / 8) - playerTX, Screen.height * (-1 + cameraPositionY + playerTextY), playerTextWidth, playerTextHeight), p4Text);

        //portrait background
        GUI.DrawTexture(new Rect((Screen.width / 8) - portBackX, Screen.height * (-1 + cameraPositionY + portraitBackgroundY), portBackX * 2, portBackX * 2), portraitBackground);
        GUI.DrawTexture(new Rect((3 * Screen.width / 8) - portBackX, Screen.height * (-1 + cameraPositionY + portraitBackgroundY), portBackX * 2, portBackX * 2), portraitBackground);
        GUI.DrawTexture(new Rect((5 * Screen.width / 8) - portBackX, Screen.height * (-1 + cameraPositionY + portraitBackgroundY), portBackX * 2, portBackX * 2), portraitBackground);
        GUI.DrawTexture(new Rect((7 * Screen.width / 8) - portBackX, Screen.height * (-1 + cameraPositionY + portraitBackgroundY), portBackX * 2, portBackX * 2), portraitBackground);

        //heads
        GUI.DrawTexture(new Rect((Screen.width / 8) - (headWidth / 2.0f), Screen.height * (-1 + cameraPositionY + headYPosition), headWidth, headHeight), heads[p1Class]);
        GUI.DrawTexture(new Rect((3 * Screen.width / 8) - (headWidth / 2.0f), Screen.height * (-1 + cameraPositionY + headYPosition), headWidth, headHeight), heads[p2Class]);
        GUI.DrawTexture(new Rect((5 * Screen.width / 8) - (headWidth / 2.0f), Screen.height * (-1 + cameraPositionY + headYPosition), headWidth, headHeight), heads[p3Class]);
        GUI.DrawTexture(new Rect((7 * Screen.width / 8) - (headWidth / 2.0f), Screen.height * (-1 + cameraPositionY + headYPosition), headWidth, headHeight), heads[p4Class]);

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

        if (GUI.Button(new Rect(buttonX - (Screen.width / 4), Screen.height * (-1 + cameraPositionY + backButtonPlacementY), buttonWidth, buttonHeight), "", startTexture))
        {
            //startgame
            StartGame("BackgroundSteampuk");
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

    void StartGame(string levelName)
    {
        Application.LoadLevel(levelName);
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
}
