using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;

    public Texture headerTexture;

    public GUIStyle newGameTexture;
    public GUIStyle controlsTexture;
    public GUIStyle quitTexture;
    public GUIStyle backTexture;
    public GUIStyle startTexture;

    public float headerYCoord;
    public float headerHeight;

    public float newGamePlacementX;
    public float newGamePlacementY;
    public float newGameHeight;
    public float distanceBetweenButtons;
    public float backButtonPlacementY;

    private float cameraPositionY = 0.0f;

    private bool moveToControls = false;
    private bool moveToNewGame = false;
    private bool moveDownToMenu = false;
    private bool moveUpToMenu = false;

    void OnGUI()
    {
        //displays background texture
        GUI.DrawTexture(new Rect(0, (cameraPositionY - 1) * Screen.height, Screen.width, 3 * Screen.height), backgroundTexture);

        float buttonX = Screen.width * newGamePlacementX;
        float buttonWidth = (Screen.width) - (Screen.width * newGamePlacementX * 2);
        float buttonHeight = Screen.height * newGameHeight;

        //-------------------------------NEW GAME SCREEN-------------------------------
        if (GUI.Button(new Rect(buttonX - (Screen.width / 4), Screen.height * (-1 + cameraPositionY + backButtonPlacementY), buttonWidth, buttonHeight), "", startTexture))
        {
            //startgame
        }
        if (GUI.Button(new Rect(buttonX + (Screen.width / 4), Screen.height * (-1 + cameraPositionY + backButtonPlacementY), buttonWidth, buttonHeight), "", backTexture))
        {
            moveDownToMenu = true;
        }

        //-------------------------------MENU SCREEN-----------------------------------

        //displays header texture
        GUI.DrawTexture(new Rect(0, Screen.height * (headerYCoord + cameraPositionY), Screen.width, Screen.height * headerHeight), headerTexture);

        //newGamePlacementY += cameraPositionY;

        if (GUI.Button(new Rect(buttonX, Screen.height * (newGamePlacementY + cameraPositionY), buttonWidth, buttonHeight), "", newGameTexture))
        {
            moveToNewGame = true;
        }
        if (GUI.Button(new Rect(buttonX, Screen.height * (newGamePlacementY + distanceBetweenButtons + cameraPositionY), buttonWidth, buttonHeight), "", controlsTexture))
        {
            moveToControls = true;
        }
        if (GUI.Button(new Rect(buttonX, Screen.height * (newGamePlacementY + 2 * distanceBetweenButtons + cameraPositionY), buttonWidth, buttonHeight), "", quitTexture))
        {
            Application.Quit();
        }

        //-------------------------------CONTROLS SCREEN-------------------------------
        if (GUI.Button(new Rect(buttonX, Screen.height * (1 + cameraPositionY + backButtonPlacementY), buttonWidth, buttonHeight), "", backTexture))
        {
            moveUpToMenu = true;
        }
    }

    void FixedUpdate()
    {
        if (moveToControls)
        {
            cameraPositionY -= 0.01f;
            if (cameraPositionY < -0.99f)
            {
                cameraPositionY = -1.0f;
                moveToControls = false;
            }
        }

        if (moveToNewGame)
        {
            cameraPositionY += 0.01f;
            if (cameraPositionY > 0.99f)
            {
                cameraPositionY = 1.0f;
                moveToNewGame = false;
            }
        }

        if (moveDownToMenu)
        {
            cameraPositionY -= 0.01f;
            if (cameraPositionY < -0.0f)
            {
                cameraPositionY = -0.0f;
                moveDownToMenu = false;
            }
        }

        if (moveUpToMenu)
        {
            cameraPositionY += 0.01f;
            if (cameraPositionY > 0.0f)
            {
                cameraPositionY = 0.0f;
                moveUpToMenu = false;
            }
        }
    }
}
