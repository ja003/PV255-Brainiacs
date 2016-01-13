using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;


public class TimeManager : MonoBehaviour {

    public RectTransform myRectT;

    Stopwatch stopWatch;

    private TimeSpan countdown;// = TimeSpan.FromSeconds(10)

    private GameInfo gameInfo;


    void Start()
    {
        
        try
        {
            gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("NO GAME INFO OBJECT - setting default values");
        }

        if (gameInfo == null)
        {
            GameObject gameInfoObj = new GameObject("GameInfo_tmp 2");
            gameInfoObj.AddComponent<GameInfo>();
            gameInfo = gameInfoObj.GetComponent<GameInfo>();
        }

        stopWatch = Stopwatch.StartNew();
        countdown = TimeSpan.FromSeconds(gameInfo.time);
        InvokeRepeating("Count1SecDown", 0, 1.0f);
        //UnityEngine.Debug.Log("tick");

    }
    

    void Count1SecDown()
    {
        countdown = countdown.Subtract(TimeSpan.FromSeconds(1));
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.border = new RectOffset(0,0,0,0);
        //FontStyle fontStyle = new FontStyle();
        //fontStyle
        //style.fontStyle = new FontStyle
        style.alignment = TextAnchor.MiddleCenter;
        

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float ratio = screenWidth / screenHeight;

        //UnityEngine.Debug.Log(screenWidth);
        //UnityEngine.Debug.Log(screenHeight);
        //UnityEngine.Debug.Log(ratio);


        style.fontSize = (int)(screenWidth/100);//ratio * 10f;

        Vector3 pos = Camera.main.WorldToScreenPoint(new Vector3(2,4.7f, 4.7f));
        //UnityEngine.Debug.Log(pos);


        //UnityEngine.Debug.Log(posX);
        //UnityEngine.Debug.Log(posY);

        switch (gameInfo.gameMode)
        {
            case GameModeEnum.Time:
                ShowCountdown(screenWidth / (1.85f), screenHeight / (24f), screenWidth / 8, screenHeight / 20, style);
                break;
            default:
                ShowStopWatch(screenWidth / (1.85f), screenHeight / (24f), screenWidth / 8, screenHeight / 20, style);
                break;
        }


        
        //ShowCountdown(screenWidth / 1.85f, screenHeight / (24f), screenWidth / 8, screenHeight / 20, style);
        //ShowCountdown(pos.x, pos.z, screenWidth / 8, screenHeight / 20, style);
        //ShowCountdown(419, 19, screenWidth / 8, screenHeight / 20, style);

        /*
        Vector3 result;
        Camera MainCam = Camera.current;
        
        Vector2 myV2 = new Vector2(2, 4);
        UnityEngine.Debug.Log(RectTransformUtility.ScreenPointToWorldPointInRectangle
            (myRectT,myV2,MainCam,out result));

        UnityEngine.Debug.Log(result);
        */



        /*
        stringToEdit = GUI.TextArea(
            new Rect(screenWidth/ (1.85f), screenHeight/(24f), screenWidth/8, screenHeight/20), 
            stringToEdit,
            style);*/

    }

    void ShowStopWatch(float x, float y, float width, float height, GUIStyle style)
    {
        TimeSpan stopWatchSpan = stopWatch.Elapsed;
        //string str = stopWatchSpan.
        int seconds = stopWatchSpan.Seconds;

        var stopWatchStr = String.Format("{0:0}:{1:00}", Mathf.Floor(seconds / 60), seconds % 60);
        

        stopWatchStr = GUI.TextArea(
            new Rect(x, y, width, height),
            stopWatchStr,
            style);
    }

    void ShowCountdown(float x, float y, float width, float height, GUIStyle style)
    {
        //TimeSpan stopWatchSpan = countdown.Elapsed;
        //string str = stopWatchSpan.
        //UnityEngine.Debug.Log(x);
        //UnityEngine.Debug.Log(y);


        int seconds = countdown.Seconds;

        var stopWatchStr = String.Format("{0:0}:{1:00}", Mathf.Floor(seconds / 60), seconds % 60);


        stopWatchStr = GUI.TextArea(
            new Rect(x, y, width, height),
            stopWatchStr,
            style);

        if(seconds <= 0)
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.EndGame();
        }
    }
}
