using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;

    public GUIStyle newGameTexture;
    public GUIStyle controlsTexture;
    public GUIStyle quitTexture;

    public float newGamePlacementX;
    public float newGamePlacementY;
    public float newGameHeight;
    public float distanceBetweenButtons;
    

    void OnGUI()
    {
        //displays background texture
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        //display buttons
        float buttonX = Screen.width * newGamePlacementX;
        float buttonWidth = (Screen.width ) - (Screen.width * newGamePlacementX * 2);
        float buttonHeight = Screen.height * newGameHeight;
        if (GUI.Button(new Rect(buttonX, Screen.height * newGamePlacementY, buttonWidth, buttonHeight), "", newGameTexture))
        {
            //game manager
        }
        if (GUI.Button(new Rect(buttonX, Screen.height * (newGamePlacementY + distanceBetweenButtons), buttonWidth, buttonHeight), "", controlsTexture))
        {
            //controls
        }
        if (GUI.Button(new Rect(buttonX, Screen.height * (newGamePlacementY + 2 * distanceBetweenButtons), buttonWidth, buttonHeight), "", quitTexture))
        {
            Application.Quit();
        }
    }
}
