using UnityEngine;
using System.Collections;

public class EndGameBackButton : MonoBehaviour {

    public GUIStyle backTexture;

    void OnGUI() {
        float buttonWidth = backTexture.normal.background.width * Screen.width / 1920.0f;
        float buttonHeight = backTexture.normal.background.height * Screen.height / 1080.0f;
        float buttonX = (1920.0f - backTexture.normal.background.width)  * Screen.width / 3840.0f;

        if (GUI.Button(new Rect(buttonX, Screen.height * 0.9f, buttonWidth, buttonHeight), "", backTexture))
        {
            Application.LoadLevel("MainMenu");
        }
    }
}
