using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player4 : PlayerBase
{
    void Start()
    {
        Debug.Log("!");

        base.keyUp = KeyCode.Keypad8;
        base.keyLeft = KeyCode.Keypad4;
        base.keyDown = KeyCode.Keypad5;
        base.keyRight = KeyCode.Keypad6;

        base.rb2d = gameObject.GetComponent<Rigidbody2D>();

        base.speed = 2f;

        base.pressedKeys = new List<KeyCode>();
        //base.pressedKeys.Add(KeyCode.X);
        //Debug.Log(base.rb2d.ToString());

    }
    void Update()
    {
        //MyMovement();
        base.Movement();
    }
}
