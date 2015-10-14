using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1 : PlayerBase {

   
    void Start()
    {
        Debug.Log("!");

        base.playerName = "Player1";
        base.speed = 2f;
        base.direction = base.right;

        SetControlKeys(KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftControl, KeyCode.LeftShift);

        base.rb2d = gameObject.GetComponent<Rigidbody2D>();

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
