using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 0.5f;
    private Vector2 up = Vector2.up;
    private Vector2 down = Vector2.down;
    private Vector2 left = Vector2.left;
    private Vector2 right = Vector2.right;
    

    // Use this for initialization
    void Start () {
        	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.W))
        {
            SetPosition(GetPosition() + up * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            SetPosition(GetPosition() + left * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            SetPosition(GetPosition() + down * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            SetPosition(GetPosition() + right * speed);
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
