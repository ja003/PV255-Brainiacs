using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public Texture backgroundTexture;

    public GUIStyle buttonTexture;

    public float newGamePlacementX;
    public float newGamePlacementY;
    public float newGameHeight;
    

    void OnGUI()
    {
        //displays background texture
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);

        //display buttons
        float newGameWidth = (Screen.width ) - (Screen.width * newGamePlacementX * 2);
        if (GUI.Button(new Rect(Screen.width * newGamePlacementX, Screen.height * newGamePlacementY, newGameWidth, Screen.height * newGameHeight), "", buttonTexture))
        {
            //game manager
        }
    }
}
