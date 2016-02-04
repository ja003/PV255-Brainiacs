using UnityEngine;
using System.Collections;
using System;

public class EndGameBackButton : MonoBehaviour {

    public GUIStyle backTexture;

    void OnGUI() {
        float buttonWidth = backTexture.normal.background.width * Screen.width / 1920.0f;
        float buttonHeight = backTexture.normal.background.height * Screen.height / 1080.0f;
        float buttonX = (1920.0f - backTexture.normal.background.width)  * Screen.width / 3840.0f;

        if (GUI.Button(new Rect(buttonX, Screen.height * 0.9f, buttonWidth, buttonHeight), "", backTexture))
        {
            //SoundManager.instance.RemoveAllSounds();
            try
            {
                GameObject oldSoundManager = GameObject.Find("SoundManager");
                Destroy(oldSoundManager);
                GameObject newSoundManager = (GameObject)Resources.Load("Prefabs/SoundManager");
                Instantiate(newSoundManager);                
            }
            catch(Exception e)
            {
                Debug.Log("SoundManager not found");
            }
            Application.LoadLevel("MainMenu");
        }
    }
}
