using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1 : PlayerBase {


    
    void Start()
    {
        Debug.Log("!");

        base.keyUp = KeyCode.W;
        base.keyLeft = KeyCode.A;
        base.keyDown = KeyCode.S;
        base.keyRight = KeyCode.D;

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
