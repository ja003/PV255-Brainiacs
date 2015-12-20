using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;

public class TextManager : MonoBehaviour {

    

    Stopwatch stopWatch;

    private TimeSpan countdown;// = TimeSpan.FromSeconds(10)


    void Start()
    {
        stopWatch = Stopwatch.StartNew();
        countdown = TimeSpan.FromSeconds(10);
        InvokeRepeating("Count1SecDown", 0, 1.0f);
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

        style.fontSize = (int)(screenWidth/100);//ratio * 10f;

        Vector2 pos = Camera.main.WorldToScreenPoint(new Vector3(0,0));

        //ShowStopWatch(screenWidth / (1.85f), screenHeight / (24f), screenWidth / 8, screenHeight / 20, style);
        ShowCountdown(screenWidth / (1.85f), screenHeight / (24f), screenWidth / 8, screenHeight / 20, style);

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
        int seconds = countdown.Seconds;

        var stopWatchStr = String.Format("{0:0}:{1:00}", Mathf.Floor(seconds / 60), seconds % 60);


        stopWatchStr = GUI.TextArea(
            new Rect(x, y, width, height),
            stopWatchStr,
            style);
    }
}
