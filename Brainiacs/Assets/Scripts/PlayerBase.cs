using System;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    public float speed { get; set; }
    

    protected Vector2 up = Vector2.up;
    protected Vector2 down = Vector2.down;
    protected Vector2 left = Vector2.left;
    protected Vector2 right = Vector2.right;
    protected Vector2 stop = Vector2.zero;

    public KeyCode keyUp { get; set; }
    public KeyCode keyLeft { get; set; }
    public KeyCode keyDown { get; set; }
    public KeyCode keyRight { get; set; }

    private KeyCode lastPressed { get; set; }

    public Rigidbody2D rb2d { get; set; }

    public Event e;

    void OnGUI()
    {
        e = Event.current;
        if (e.isKey)
            Debug.Log("Detected key code: " + e.keyCode);


    }

    public KeyCode GetKeyUp()
    {
        return keyUp;
    }

    protected void Movement()
    {

        
        if (Input.GetKey(keyUp) && lastPressed != keyUp)
        {

            rb2d.velocity = up * speed;
            //CheckUp();
            lastPressed = keyUp;

        }
        else if (Input.GetKey(keyLeft) && lastPressed != keyLeft)
        {
            rb2d.velocity = left * speed;
            //CheckLeft();
            lastPressed = keyLeft;
        }
        else if (Input.GetKey(keyDown) && lastPressed != keyDown)
        {
            rb2d.velocity = down * speed;
            //CheckDown();
            lastPressed = keyDown;

        }
        else if (Input.GetKey(keyRight) && lastPressed != keyRight)
        {
            rb2d.velocity = right * speed;
            //CheckRight();
            lastPressed = keyRight;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            rb2d.velocity = stop;
            lastPressed = KeyCode.X;
        }


    }

    /// <summary>
    /// na to dole nekoukejte, to byl takový..pokus, možná se bude hodit:D
    /// </summary>

    //check if user is pressing other key that UP
    public void CheckUp()
    {
        if (Input.GetKey(keyLeft))
        {
            rb2d.velocity = left * speed;

            //Debug.Log("UP+LEFT");


        }
        else if (Input.GetKey(keyDown))
        {
            rb2d.velocity = down * speed;

        }
        /*
        else if (Input.GetKey(keyRight))
        {
            rb2d.velocity = right * speed;
        }*/
    }
    //check if user is pressing other key that LEFT
    public void CheckLeft()
    {
        //Debug.Log(Input.inputString);
        if (Input.GetKey(keyUp))
        {
            rb2d.velocity = up * speed;

        }
        else if (Input.GetKey(keyDown))
        {
            rb2d.velocity = down * speed;

        }
        else if (Input.GetKey(keyRight))
        {
            rb2d.velocity = right * speed;
        }
    }
    //check if user is pressing other key that DOWN
    public void CheckDown()
    {
        //Debug.Log(Input.inputString);
        if (Input.GetKey(keyLeft))
        {
            rb2d.velocity = left * speed;
            //Debug.Log("DOWN+LEFT");


        }
        else if (Input.GetKey(keyUp))
        {
            rb2d.velocity = up * speed;

        }
        else if (Input.GetKey(keyRight))
        {
            rb2d.velocity = right * speed;
        }
    }
    //check if user is pressing other key that RIGHT
    public void CheckRight()
    {
        //Debug.Log(Input.inputString);
        if (Input.GetKey(keyLeft))
        {
            rb2d.velocity = left * speed;

        }
        else if (Input.GetKey(keyUp))
        {
            rb2d.velocity = up * speed;

        }
        else if (Input.GetKey(keyDown))
        {
            rb2d.velocity = down * speed;
        }
    }





    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector2 vec)
    {
        transform.position = vec;
    }
}