using UnityEngine;
using System.Collections;

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

        base.e = Event.current;
        //Debug.Log(base.rb2d.ToString());

    }
        void Update()
    {
        //MyMovement();
        base.Movement();
    }

    /// <summary>
    /// taky napiču....
    /// </summary>
    protected void MyMovement()
    {
        if(e.isKey)
        {
            switch (e.keyCode)
            {
                case KeyCode.W:
                    rb2d.velocity = base.up * speed;
                    break;
                case KeyCode.A:
                    rb2d.velocity = base.left * speed;
                    break;
                case KeyCode.S:
                    rb2d.velocity = base.down * speed;
                    break;
                case KeyCode.D:
                    rb2d.velocity = base.right * speed;
                    break;
                default: rb2d.velocity = stop;
                    break;
            }
        }
        
    }


    
    
    
}
