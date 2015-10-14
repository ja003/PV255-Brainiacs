using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    public string playerName { get; set; }

    public float speed { get; set; }

    public Vector2 direction;

    protected Vector2 up = Vector2.up;
    protected Vector2 down = Vector2.down;
    protected Vector2 left = Vector2.left;
    protected Vector2 right = Vector2.right;
    protected Vector2 stop = Vector2.zero;

    public KeyCode keyUp { get; set; }
    public KeyCode keyLeft { get; set; }
    public KeyCode keyDown { get; set; }
    public KeyCode keyRight { get; set; }
    public KeyCode keyFire { get; set; }
    public KeyCode keySwitchWeapon { get; set; }


    private KeyCode lastPressed { get; set; }

    //rigid body of controlled object
    public Rigidbody2D rb2d { get; set; }

    public List<KeyCode> pressedKeys { get; set; }


    


    public void SetControlKeys(KeyCode keyUp, KeyCode keyLeft, KeyCode keyDown, KeyCode keyRight, KeyCode keyFire, KeyCode keySwitchWeapon)
    {
        this.keyUp = keyUp;
        this.keyLeft = keyLeft;
        this.keyDown = keyDown;
        this.keyRight = keyRight;
        this.keyFire = keyFire;
        this.keySwitchWeapon = keySwitchWeapon;
    }


    // <<<MOVEMENT...>>> //
    protected void Movement()
    {

        //pouze debug
        //funguje A+D+W
        //nefunguje A+S+W, S+A+W, S+D+W...
        //nevim proč, většina kombinací 3 kláves funguje
        //4 klávesy nefungujou nikdy
        if (Input.GetKeyDown(keyUp))
        {
            //Debug.Log("UP");
        }


        if (Input.GetKeyDown(keyUp) && !PressedKeysContains(keyUp))
        {
            pressedKeys.Add(keyUp);
        }
        else if (Input.GetKeyDown(keyLeft) && !PressedKeysContains(keyLeft))
        {
            pressedKeys.Add(keyLeft);
        }
        else if (Input.GetKeyDown(keyDown) && !PressedKeysContains(keyDown))
        {
            pressedKeys.Add(keyDown);
        }
        else if (Input.GetKeyDown(keyRight) && !PressedKeysContains(keyRight))
        {
            pressedKeys.Add(keyRight);
        }

        if (GetLastPressed() == keyUp)
        {
            rb2d.velocity = up * speed;
            direction = up;
        }
        else if (GetLastPressed() == keyLeft)
        {
            rb2d.velocity = left * speed;
            direction = left;
        }
        else if (GetLastPressed() == keyDown)
        {
            rb2d.velocity = down * speed;
            direction = down;
        }
        else if (GetLastPressed() == keyRight)
        {
            rb2d.velocity = right * speed;
            direction = right;
        }
        else
        {
            rb2d.velocity = stop;
        }

        if (Input.GetKeyUp(keyUp) || Input.GetKeyUp(keyLeft) ||
            Input.GetKeyUp(keyDown) || Input.GetKeyUp(keyRight))
        {
            rb2d.velocity = stop;
            if (Input.GetKeyUp(keyUp))
            {
                RemoveKeyPressed(keyUp);
            }
            if (Input.GetKeyUp(keyLeft))
            {
                RemoveKeyPressed(keyLeft);
            }
            if (Input.GetKeyUp(keyDown))
            {
                RemoveKeyPressed(keyDown);
            }
            if (Input.GetKeyUp(keyRight))
            {
                RemoveKeyPressed(keyRight);
            }
        }

        CheckPressedKeys();
        //Debug.Log(PrintPressedKeys());
    }

    /// <summary>
    /// last element of pressedKeys list
    /// </summary>
    /// <returns>defaul value (X) if no key was pressed</returns>
    public KeyCode GetLastPressed()
    {
        if (pressedKeys.Count < 1)
            return KeyCode.X;
        return pressedKeys[pressedKeys.Count - 1];
    }

    /// <summary>
    /// removes all keys in pressedKeys list
    /// </summary>
    /// <param name="key"></param>
    public void RemoveKeyPressed(KeyCode key)
    {
        for (int i = 0; i < pressedKeys.Count; i++)
        {
            if (pressedKeys[i] == key)
            {
                pressedKeys.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// debug
    /// </summary>
    /// <returns></returns>
    public string PrintPressedKeys()
    {
        string s = "";
        foreach (KeyCode k in pressedKeys)
        {
            s += k.ToString();
            s += ";";
        }
        return s;
    }

    /// <summary>
    /// prevents List from containg too many elements
    /// shouldn't be neccessary (RemoveKey should do the job)
    /// </summary>
    public void CheckPressedKeys()
    {
        if (pressedKeys.Count > 5)
        {
            pressedKeys.RemoveAt(1);
        }
    }

    public bool PressedKeysContains(KeyCode key)
    {
        foreach (KeyCode k in pressedKeys)
        {
            if (k == key)
                return true;
        }
        return false;
    }

    // <<<...MOVEMENT>>> //

}