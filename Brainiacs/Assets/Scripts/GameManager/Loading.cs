using UnityEngine;
using System.Collections;
using System;

public class Loading : MonoBehaviour {

    // Use this for initialization
    private SpriteRenderer sr;

    private AudioClip beep1;
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

        beep1 = Resources.Load<AudioClip>("Sounds/Loading/loading_beep_01");
        beep2 = Resources.Load<AudioClip>("Sounds/Loading/loading_beep_02");
        beep3 = Resources.Load<AudioClip>("Sounds/Loading/loading_beep_03");
        beep4 = Resources.Load<AudioClip>("Sounds/Loading/loading_beep_04");
        
        frameCountSinceLvlLoad = 0;
        SoundManager.instance.PlaySingle(beep1, false, 1f, 128, false);

    }

    int interval = 70;
    // Update is called once per frame
    void Update () {

        if(frameCountSinceLvlLoad == interval)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_" + mapName + "_02");
            //beep
            SoundManager.instance.PlaySingle(beep2, false, 1f, 128, false);
        }

        if (frameCountSinceLvlLoad == interval*2)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_" + mapName + "_03");
            //beep
            SoundManager.instance.PlaySingle(beep3, false, 1f, 128, false);

        }

        if (frameCountSinceLvlLoad == interval*3)
        {
            sr.sprite = Resources.Load<Sprite>("Sprites/Loading/loading_" + mapName + "_04");
            //beep
            SoundManager.instance.PlaySingle(beep4, false, 1f, 128, false);

        }

        //chvilku trvá...případně posunout
        if(frameCountSinceLvlLoad == interval*4)
        {
            Application.LoadLevel(mapName);
        }

        frameCountSinceLvlLoad++;

    }
}
