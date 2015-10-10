using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private float speed = 2f;
    private Vector3 up = new Vector3(0, 1, 0);
    private Vector3 down = new Vector3(0, -1,0);
    private Vector3 left = new Vector3(-1, 0,0);
    private Vector3 right = new Vector3(1, 0,0);

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

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 vec)
    {
        transform.position = vec;
    }
}
