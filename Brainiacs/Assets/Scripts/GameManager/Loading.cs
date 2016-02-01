using UnityEngine;
using System.Collections;
using System;

public class Loading : MonoBehaviour {

    // Use this for initialization
    private SpriteRenderer sr;

    private AudioClip beep2;
    private AudioClip beep3;
    private AudioClip beep4;

    private GameInfo gameInfo;
    private string mapName;
    
    private int frameCountSinceLvlLoad;

    void Start () {
        try
        {
            gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
            Debug.Log(gameInfo);
        }
        catch (Exception e)
        {
            Debug.Log("NO GAME INFO OBJECT - setting default values");
        }
        if (gameInfo == null)
        {
            GameObject gameInfoObj = new GameObject("GameInfo");
            UnityEngine.Object.DontDestroyOnLoad(gameInfoObj);

            gameInfoObj.AddComponent<GameInfo>();
            gameInfo = gameInfoObj.GetComponent<GameInfo>();
        }

        mapName = gameInfo.mapName;

        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_" + mapName + "_01");
        Debug.Log("Sprites/Loading/loading_" + mapName + "_01");

        beep2 = Resources.Load<AudioClip>("Sounds/Loading/beep2");
        beep3 = Resources.Load<AudioClip>("Sounds/Loading/beep2");
        beep4 = Resources.Load<AudioClip>("Sounds/Loading/beep2");
        
        frameCountSinceLvlLoad = 0;
    }
	
	// Update is called once per frame
	void Update () {

        if(frameCountSinceLvlLoad == 50)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_" + mapName + "_02");
            //beep
            SoundManager.instance.PlaySingle(beep2);
        }

        if (frameCountSinceLvlLoad == 100)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_" + mapName + "_03");
            //beep
            SoundManager.instance.PlaySingle(beep3);

        }

        if (frameCountSinceLvlLoad == 150)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_" + mapName + "_04");
            //beep
            SoundManager.instance.PlaySingle(beep4);

        }

        //chvilku trvá...případně posunout
        if(frameCountSinceLvlLoad == 200)//fastload
        {
            Application.LoadLevel(mapName);
        }

        frameCountSinceLvlLoad++;

    }
}
