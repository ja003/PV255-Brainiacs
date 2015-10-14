using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2 : PlayerBase
{



    void Start()
    {
        Debug.Log("!");

        base.keyUp = KeyCode.U;
        base.keyLeft = KeyCode.H;
        base.keyDown = KeyCode.J;
        base.keyRight = KeyCode.K;

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
