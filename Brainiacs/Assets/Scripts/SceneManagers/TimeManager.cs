using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;


public class TimeManager : MonoBehaviour {

    public RectTransform myRectT;

    Stopwatch stopWatch;

    private TimeSpan countdown;// = TimeSpan.FromSeconds(10)

    private GameInfo gameInfo;

    private TextDisplay textDisplay;

    void Awake()
    {
        CheckGameManager();
    }

    void Start()
    {
        

        textDisplay = new TextDisplay();
        textDisplay.InitializeTimeVariable();

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
        //UnityEngine.Debug.Log(gameInfo.time);
        countdown = TimeSpan.FromSeconds(gameInfo.time);
        InvokeRepeating("Count1SecDown", 0, 1.0f);
        //UnityEngine.Debug.Log("tick");

    }
    
    void CheckGameManager()
    {
        GameObject gmObject = GameObject.Find("GameManager");
        GameManager gameManager;
        try
        {
            gameManager = gmObject.GetComponent<GameManager>();
            if (gameManager != null)
            {
                UnityEngine.Debug.Log("GameManager is OK");
            }
            else
            {
                UnityEngine.Debug.Log("GameManager script not assigned!");
                gmObject.AddComponent<GameManager>();
            }

        }
        catch (Exception e)
        {
            UnityEngine.Debug.Log("GameManager script not assigned!");
            gmObject.AddComponent<GameManager>();
        }
    }

    void Count1SecDown()
    {
        countdown = countdown.Subtract(TimeSpan.FromSeconds(1));
    }

    void Update()
    {

        switch (gameInfo.gameMode)
        {
            case GameModeEnum.Time:
                ShowCountdown();
                break;
            default:
                ShowStopWatch();
                break;
        }

        

    }

    void ShowStopWatch()
    {
        TimeSpan stopWatchSpan = stopWatch.Elapsed;
        //string str = stopWatchSpan.
        int seconds = (int)stopWatchSpan.TotalSeconds;

        textDisplay.ShowTime(seconds);

        var stopWatchStr = String.Format("{0:0}:{1:00}", Mathf.Floor(seconds / 60), seconds % 60);
        
    }

    void ShowCountdown()
    {
        //TimeSpan stopWatchSpan = countdown.Elapsed;
        //string str = stopWatchSpan.
        //UnityEngine.Debug.Log(x);
        //UnityEngine.Debug.Log(y);


        int seconds = (int)countdown.TotalSeconds;

        textDisplay.ShowTime(seconds);

        //obsolete
        var stopWatchStr = String.Format("{0:0}:{1:00}", Mathf.Floor(seconds / 60), seconds % 60);
        
        if(seconds <= 0)
        {
            GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
            gm.EndGame();
        }
    }
}
