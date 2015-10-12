using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;
    private Vector2 up = Vector2.up;
    private Vector2 down = Vector2.down;
    private Vector2 left = Vector2.left;
    private Vector2 right = Vector2.right;
    private Vector2 stop = Vector2.zero;

    private Rigidbody2D rb2d;
    

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        speed = 200f;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W))
        {
            //SetPosition(GetPosition() + up * speed);
            //rb2d.AddForce(up * speed);
            rb2d.velocity = up * speed;

        }
        if (Input.GetKey(KeyCode.A))
        {
            //SetPosition(GetPosition() + left * speed);
            //rb2d.AddForce(left * speed);
            rb2d.velocity = left * speed;

        }
        if (Input.GetKey(KeyCode.S))
        {
            //SetPosition(GetPosition() + down * speed);
            //rb2d.AddForce(down * speed);
            rb2d.velocity = down * speed;

        }
        if (Input.GetKey(KeyCode.D))
        {
            //Debug.Log(rb2d.mass);
            //rb2d.AddForce(right* speed);
            rb2d.velocity = right * speed;
            //SetPosition(GetPosition() + right * speed);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            rb2d.velocity = stop;
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
