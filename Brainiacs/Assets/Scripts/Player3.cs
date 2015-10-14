using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player3 : PlayerBase
{
    void Start()
    {
        Debug.Log("!");

        base.keyUp = KeyCode.UpArrow;
        base.keyLeft = KeyCode.LeftArrow;
        base.keyDown = KeyCode.DownArrow;
        base.keyRight = KeyCode.RightArrow;

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
