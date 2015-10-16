using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    public CharacterEnum character;

    // JP - farba spritu
    public string color { get; set; }
    // JP added
    public int ID { get; set; }
    public float posX;
    public float posY;

    public string playerName { get; set; }

    public float speed { get; set; }

    public List<WeaponBase> inventory { get; set; }

    public WeaponBase activeWeapon { get; set; }
    private static int maxHP = 100;
    public int hitPoints = maxHP;

    public Vector2 direction;
    private Vector2 newRandomPosition;

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
    // JP - pre alternatibny movement
    public Stack<KeyCode> pressed_keys  = new Stack<KeyCode>();
    int pops = 0;

    public void SetControlKeys(KeyCode keyUp, KeyCode keyLeft, KeyCode keyDown, KeyCode keyRight, KeyCode keyFire, KeyCode keySwitchWeapon)
    {
        this.keyUp = keyUp;
        this.keyLeft = keyLeft;
        this.keyDown = keyDown;
        this.keyRight = keyRight;
        this.keyFire = keyFire; 
        this.keySwitchWeapon = keySwitchWeapon;
    }



    protected void SwitchWeapon()
    {
        if (Input.GetKeyDown(keySwitchWeapon))
        {
            if (inventory.IndexOf(activeWeapon) == inventory.Count - 1)
            {
                activeWeapon = inventory[0];
                transform.Find("weaponTry").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/weaponTry_01");

            }
            else
            {
                activeWeapon = inventory[inventory.IndexOf(activeWeapon) + 1];
                transform.Find("weaponTry").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/weaponTry_02");

            }
            
            // TEN Sí šarp je dobre prijebany !           

            //Debug.Log(activeWeapon);
        }
    }

    // <<<MOVEMENT...>>> //
    protected void Movement()
    {

        /* JP alternativny movement

        if (Input.GetKeyDown(keyUp) && pressed_keys.Peek() != keyUp)
        {
            pressed_keys.Push(keyUp);
        }
        else if (Input.GetKeyDown(keyLeft) && pressed_keys.Peek() != keyLeft)
        {
            pressed_keys.Push(keyLeft);
        }
        else if (Input.GetKeyDown(keyDown) && pressed_keys.Peek() != keyDown)
        {
            pressed_keys.Push(keyDown);
        }
        else if (Input.GetKeyDown(keyRight) && pressed_keys.Peek() != keyRight)
        {
            pressed_keys.Push(keyRight);
        }

        if (Input.GetKeyUp(keyUp))
        {
            if (keyUp == pressed_keys.Peek())
            {
                pressed_keys.Pop();
                pop();
            }
            else { pops++; }
        }
        else if (Input.GetKeyUp(keyLeft))
        {
            if (keyLeft == pressed_keys.Peek())
            {
                pressed_keys.Pop();
                pop();
            }
           else { pops++; }
        }
        else if (Input.GetKeyUp(keyDown))
        {
            if (keyDown == pressed_keys.Peek())
            {
                pressed_keys.Pop();
                pop();
            }
           else { pops++; }
        }
        else if (Input.GetKeyUp(keyRight))
        {
            if (keyRight == pressed_keys.Peek())
            {
                pressed_keys.Pop();
                pop();
            }
            else { pops++; }
        }

        if (pressed_keys.Peek() == keyUp)
        {
            rb2d.velocity = up * speed;
            direction = up;
            //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Tesla_left");

        }
        else if (pressed_keys.Peek() == keyLeft)
        {
            rb2d.velocity = left * speed;
            direction = left;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tesla_left");

        }
        else if (pressed_keys.Peek() == keyDown)
        {
            rb2d.velocity = down * speed;
            direction = down;
        }
        else if (pressed_keys.Peek() == keyRight)
        {
            rb2d.velocity = right * speed;
            direction = right;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tesla_right");

        }
        else
        {
            rb2d.velocity = stop;
        }

        */

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
            //gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Tesla_left");
            
        }
        else if (GetLastPressed() == keyLeft)
        {
            rb2d.velocity = left * speed;
            direction = left;
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tesla_left");

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
            gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tesla_right");

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

    private void pop() {

        for (int i = 0; i < pops; i++)
        {
            pressed_keys.Pop();
        }

        pops = 0;

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

    //HP management - MG
    public void ApplyDamage(int dmg)
    {
        if ((hitPoints - dmg) <= 0)
        {
            hitPoints = 0;
            //TODO sputenie animacie smrti
            rb2d.velocity = stop;
            //TODO delay nejake 2s

            //presun na novu poziciu
            GenerateRandomPosition(newRandomPosition);
            transform.Translate(newRandomPosition.x, newRandomPosition.y, 0);
            posX = newRandomPosition.x;
            posY = newRandomPosition.y;
        }
        else
        {
            hitPoints -= dmg;
        }
        Debug.Log(hitPoints);
    }

    public void ApplyHeal(int heal)
    {
        if ((hitPoints + heal) > maxHP)
        {
            hitPoints = maxHP;
        }
        else
        {
            hitPoints += heal;
        }

    }

    private void GenerateRandomPosition(Vector2 vec)
    {
        System.Random rnd = new System.Random();
        int randX = rnd.Next(gameObject.transform.parent.gameObject.GetComponent<GameManager>().mapWidth);
        vec.x = (randX / 100) + gameObject.transform.parent.gameObject.GetComponent<GameManager>().mapStartX;
        int randY = rnd.Next(gameObject.transform.parent.gameObject.GetComponent<GameManager>().mapHeight);
        vec.y = (randY / 100) + gameObject.transform.parent.gameObject.GetComponent<GameManager>().mapStartY;
        //TODO check ci tam uz nieco neni
        /*if(){
            GenerateRandomPosition(vec);
        }*/
    }
}