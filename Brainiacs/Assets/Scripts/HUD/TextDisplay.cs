using UnityEngine;
using System.Collections;
using System;

public class TextDisplay {

    ///HUD
    SpriteRenderer redClipSingle;
    SpriteRenderer redClipTenField;
    SpriteRenderer redClipUnitField;
    SpriteRenderer redHpSingle;
    SpriteRenderer redHpTenField;
    SpriteRenderer redHpUnitField;

    SpriteRenderer greenClipSingle;
    SpriteRenderer greenClipTenField;
    SpriteRenderer greenClipUnitField;
    SpriteRenderer greenHpSingle;
    SpriteRenderer greenHpTenField;
    SpriteRenderer greenHpUnitField;

    SpriteRenderer blueClipSingle;
    SpriteRenderer blueClipTenField;
    SpriteRenderer blueClipUnitField;
    SpriteRenderer blueHpSingle;
    SpriteRenderer blueHpTenField;
    SpriteRenderer blueHpUnitField;

    SpriteRenderer yellowClipSingle;
    SpriteRenderer yellowClipTenField;
    SpriteRenderer yellowClipUnitField;
    SpriteRenderer yellowHpSingle;
    SpriteRenderer yellowHpTenField;
    SpriteRenderer yellowHpUnitField;

    SpriteRenderer timeMinutesTen;
    SpriteRenderer timeMinutesUnit;
    SpriteRenderer timeSecondsTen;
    SpriteRenderer timeSecondsUnit;
    
    

    public TextDisplay()
    {
        try
        {
            InitializeGameVariables();
            HidePlayerInfo(Colors.Red);
            HidePlayerInfo(Colors.Green);
            HidePlayerInfo(Colors.Blue);
            HidePlayerInfo(Colors.Yellow);

        }
        catch(NullReferenceException e)
        {

        }

        

    }

    

    public void DisplayNumberOn(SpriteRenderer singleField, SpriteRenderer tenField, SpriteRenderer unitField, int value)
    {

        if(value < 0)
        {
            tenField.sprite = null;
            unitField.sprite = null;
            singleField.sprite = GetNumberSprite("number_single_field"); //blank
        }
        else if (value < 10)
        {
            tenField.sprite = null;
            unitField.sprite = null;
            singleField.sprite = GetNumberSprite("number_single_" + value);
        }
        else if (value >= 10 && value < 100)
        {
            singleField.sprite = null;

            int ten = value / 10;
            int unit = value % 10;
            tenField.sprite = GetNumberSprite("number_double_" + ten);
            unitField.sprite = GetNumberSprite("number_double_" + unit);
        }
    }

    public void InitializeTimeVariable()
    {
        timeMinutesTen = GameObject.Find("time_minutes_ten").GetComponent<SpriteRenderer>();
        timeMinutesUnit = GameObject.Find("time_minutes_unit").GetComponent<SpriteRenderer>();
        timeSecondsTen = GameObject.Find("time_seconds_ten").GetComponent<SpriteRenderer>();
        timeSecondsUnit = GameObject.Find("time_seconds_unit").GetComponent<SpriteRenderer>();
    }

    public void InitializeGameVariables()
    {
        redClipSingle = GameObject.Find("red_clip_single_field").GetComponent<SpriteRenderer>();
        redClipTenField = GameObject.Find("red_clip_ten_field").GetComponent<SpriteRenderer>();
        redClipUnitField = GameObject.Find("red_clip_unit_field").GetComponent<SpriteRenderer>();
        redHpSingle = GameObject.Find("red_hp_single_field").GetComponent<SpriteRenderer>();
        redHpTenField = GameObject.Find("red_hp_ten_field").GetComponent<SpriteRenderer>();
        redHpUnitField = GameObject.Find("red_hp_unit_field").GetComponent<SpriteRenderer>();

        greenClipSingle = GameObject.Find("green_clip_single_field").GetComponent<SpriteRenderer>();
        greenClipTenField = GameObject.Find("green_clip_ten_field").GetComponent<SpriteRenderer>();
        greenClipUnitField = GameObject.Find("green_clip_unit_field").GetComponent<SpriteRenderer>();
        greenHpSingle = GameObject.Find("green_hp_single_field").GetComponent<SpriteRenderer>();
        greenHpTenField = GameObject.Find("green_hp_ten_field").GetComponent<SpriteRenderer>();
        greenHpUnitField = GameObject.Find("green_hp_unit_field").GetComponent<SpriteRenderer>();

        blueClipSingle = GameObject.Find("blue_clip_single_field").GetComponent<SpriteRenderer>();
        blueClipTenField = GameObject.Find("blue_clip_ten_field").GetComponent<SpriteRenderer>();
        blueClipUnitField = GameObject.Find("blue_clip_unit_field").GetComponent<SpriteRenderer>();
        blueHpSingle = GameObject.Find("blue_hp_single_field").GetComponent<SpriteRenderer>();
        blueHpTenField = GameObject.Find("blue_hp_ten_field").GetComponent<SpriteRenderer>();
        blueHpUnitField = GameObject.Find("blue_hp_unit_field").GetComponent<SpriteRenderer>();

        yellowClipSingle = GameObject.Find("yellow_clip_single_field").GetComponent<SpriteRenderer>();
        yellowClipTenField = GameObject.Find("yellow_clip_ten_field").GetComponent<SpriteRenderer>();
        yellowClipUnitField = GameObject.Find("yellow_clip_unit_field").GetComponent<SpriteRenderer>();
        yellowHpSingle = GameObject.Find("yellow_hp_single_field").GetComponent<SpriteRenderer>();
        yellowHpTenField = GameObject.Find("yellow_hp_ten_field").GetComponent<SpriteRenderer>();
        yellowHpUnitField = GameObject.Find("yellow_hp_unit_field").GetComponent<SpriteRenderer>();

    }

    public void ShowTime(int seconds)
    {

        int minutesTen = (seconds / 60) / 10;
        int minutesUnit = (seconds / 60) % 10;
        int secondsTen = (seconds % 60)/10;
        int secondsUnit = (seconds % 60) % 10;

        if (minutesTen != 0)
            timeMinutesTen.sprite = GetNumberSprite("number_double_" + minutesTen);
        else
            timeMinutesTen.sprite = null;

        timeMinutesUnit.sprite = GetNumberSprite("number_double_" + minutesUnit);
        timeSecondsTen.sprite = GetNumberSprite("number_double_" + secondsTen);
        timeSecondsUnit.sprite = GetNumberSprite("number_double_" + secondsUnit);

    }

    public void SetClipValue(Colors color, int value)
    {
        if (value < 10 && value > -1)
        {
            HideDoubleClip(color);
            switch (color)
            {
                case Colors.Red:
                    redClipSingle.sprite = GetNumberSprite("number_single_" + value);
                    break;
                case Colors.Green:
                    greenClipSingle.sprite = GetNumberSprite("number_single_" + value);
                    break;
                case Colors.Blue:
                    blueClipSingle.sprite = GetNumberSprite("number_single_" + value);
                    break;
                case Colors.Yellow:
                    yellowClipSingle.sprite = GetNumberSprite("number_single_" + value);
                    break;
            }
        }
        else if(value >= 10 && value < 100)
        {
            int ten = value / 10;
            int unit = value % 10;
            switch (color)
            {
                case Colors.Red:
                    HideSingleClip(color);
                    redClipTenField.sprite = GetNumberSprite("number_double_" + ten);
                    redClipUnitField.sprite = GetNumberSprite("number_double_" + unit);
                    break;
                case Colors.Green:
                    HideSingleClip(color);
                    greenClipTenField.sprite = GetNumberSprite("number_double_" + ten);
                    greenClipUnitField.sprite = GetNumberSprite("number_double_" + unit);
                    break;
                case Colors.Blue:
                    HideSingleClip(color);
                    blueClipTenField.sprite = GetNumberSprite("number_double_" + ten);
                    blueClipUnitField.sprite = GetNumberSprite("number_double_" + unit);
                    break;
                case Colors.Yellow:
                    HideSingleClip(color);
                    yellowClipTenField.sprite = GetNumberSprite("number_double_" + ten);
                    yellowClipUnitField.sprite = GetNumberSprite("number_double_" + unit);
                    break;
            }
        }
        else if (value >= 100)
        {
            HideDoubleClip(color);
            switch (color)
            {
                case Colors.Red:
                    redClipSingle.sprite = GetNumberSprite("number_single_100");
                    break;
                case Colors.Green:
                    greenClipSingle.sprite = GetNumberSprite("number_single_100");
                    break;
                case Colors.Blue:
                    blueClipSingle.sprite = GetNumberSprite("number_single_100");
                    break;
                case Colors.Yellow:
                    yellowClipSingle.sprite = GetNumberSprite("number_single_100");
                    break;
            }
        }
    }

    public void SetHpValue(Colors color, int value)
    {
        if (value < 10 && value > -1)
        {
            HideDoubleHp(color);
            switch (color)
            {
                case Colors.Red:
                    redHpSingle.sprite = GetNumberSprite("number_single_" + value);
                    break;
                case Colors.Green:
                    greenHpSingle.sprite = GetNumberSprite("number_single_" + value);
                    break;
                case Colors.Blue:
                    blueHpSingle.sprite = GetNumberSprite("number_single_" + value);
                    break;
                case Colors.Yellow:
                    yellowHpSingle.sprite = GetNumberSprite("number_single_" + value);
                    break;
            }
        }
        else if (value >= 10 && value < 100)
        {
            int ten = value / 10;
            int unit = value % 10;
            HideSingleHp(color);
            switch (color)
            {
                case Colors.Red:              
                    redHpTenField.sprite = GetNumberSprite("number_double_" + ten);
                    redHpUnitField.sprite = GetNumberSprite("number_double_" + unit);
                    break;
                case Colors.Green:
                    greenHpTenField.sprite = GetNumberSprite("number_double_" + ten);
                    greenHpUnitField.sprite = GetNumberSprite("number_double_" + unit);
                    break;
                case Colors.Blue:
                    blueHpTenField.sprite = GetNumberSprite("number_double_" + ten);
                    blueHpUnitField.sprite = GetNumberSprite("number_double_" + unit);
                    break;
                case Colors.Yellow:
                    yellowHpTenField.sprite = GetNumberSprite("number_double_" + ten);
                    yellowHpUnitField.sprite = GetNumberSprite("number_double_" + unit);
                    break;

            }
        }
        else if(value >= 100)
        {
            HideDoubleHp(color);
            switch (color)
            {
                case Colors.Red:                    
                    redHpSingle.sprite = GetNumberSprite("number_single_100");
                    break;
                case Colors.Green:
                    greenHpSingle.sprite = GetNumberSprite("number_single_100");
                    break;
                case Colors.Blue:
                    blueHpSingle.sprite = GetNumberSprite("number_single_100");
                    break;
                case Colors.Yellow:
                    yellowHpSingle.sprite = GetNumberSprite("number_single_100");
                    break;
            }
        }
    }

    /// <summary>
    /// Sprites/HUD/numbers/" + path
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public Sprite GetNumberSprite(string path)
    {
        return Resources.Load<Sprite>("Sprites/HUD/numbers/" + path);
    }

    public void HidePlayerInfo(Colors color)
    {
        HideDoubleClip(color);
        HideSingleClip(color);
        HideDoubleHp(color);
        HideSingleHp(color);
    }

    public void HideDoubleClip(Colors color)
    {
        switch (color)
        {
            case Colors.Red:
                redClipTenField.sprite = null;
                redClipUnitField.sprite = null;
                break;
            case Colors.Green:
                greenClipTenField.sprite = null;
                greenClipUnitField.sprite = null;
                break;
            case Colors.Blue:
                blueClipTenField.sprite = null;
                blueClipUnitField.sprite = null;
                break;
            case Colors.Yellow:
                yellowClipTenField.sprite = null;
                yellowClipUnitField.sprite = null;
                break;
        }
    }

    public void HideSingleClip(Colors color)
    {
        switch (color)
        {
            case Colors.Red:
                redClipSingle.sprite = null;                
                break;
            case Colors.Green:
                greenClipSingle.sprite = null;
                break;
            case Colors.Blue:
                blueClipSingle.sprite = null;
                break;
            case Colors.Yellow:
                yellowClipSingle.sprite = null;
                break;
        }
    }

    public void HideDoubleHp(Colors color)
    {
        switch (color)
        {
            case Colors.Red:
                redHpTenField.sprite = null;
                redHpUnitField.sprite = null;
                break;
            case Colors.Green:
                greenHpTenField.sprite = null;
                greenHpUnitField.sprite = null;
                break;
            case Colors.Blue:
                blueHpTenField.sprite = null;
                blueHpUnitField.sprite = null;
                break;
            case Colors.Yellow:
                yellowHpTenField.sprite = null;
                yellowHpUnitField.sprite = null;
                break;
        }
    }

    public void HideSingleHp(Colors color)
    {
        switch (color)
        {
            case Colors.Red:
                redHpSingle.sprite = null;
                break;
            case Colors.Green:
                greenHpSingle.sprite = null;
                break;
            case Colors.Blue:
                blueHpSingle.sprite = null;
                break;
            case Colors.Yellow:
                yellowHpSingle.sprite = null;
                break;
        }
    }

   
}
