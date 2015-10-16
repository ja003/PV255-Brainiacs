using UnityEngine;
using System.Collections;

public class AiBase : MonoBehaviour {

    public float speed{get;set;}
    public Rigidbody2D rb2d { get; set; }

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        speed = 1f;
    }
	
	// Update is called once per frame
	void Update () {
        MoveTo(0.0, 3.5);
    }

    public void MoveTo(double x, double y)
    {
        //Debug.Log(gameObject.transform.position);
        //Debug.Log("X=" + x + ",Y=" + y);
        //Debug.Log("x="+gameObject.transform.position.x + ", y=" + gameObject.transform.position.y);

        if (ValueEquals(gameObject.transform.position.y, y) && ValueEquals(gameObject.transform.position.x, x))
        {
            //Debug.Log("You there");
            Stop();
        }
        else
        {

            if (ValueEquals(gameObject.transform.position.y, y))
            {
                //Debug.Log("Y ok");
            }
            else if (ValueSmaller(gameObject.transform.position.y, y))
            {
                MoveUp();
            }
            else
            {
                MoveDown();
            }
            if (ValueEquals(gameObject.transform.position.x, x))
            {
                //Debug.Log("X ok");
            }
            else if (ValueSmaller(gameObject.transform.position.x, x))
            {
                MoveRight();
            }
            else
            {
                MoveLeft();
            }
        }
    }

    public void MoveUp()
    {
        rb2d.velocity = Vector2.up * speed;
    }
    public void MoveLeft()
    {
        rb2d.velocity = Vector2.left * speed;
    }
    public void MoveDown()
    {
        rb2d.velocity = Vector2.down * speed;
    }
    public void MoveRight()
    {
        rb2d.velocity = Vector2.right * speed;
    }
    public void Stop()
    {
        rb2d.velocity = Vector2.zero;
    }

    private double e = 0.1; //odchylka
    /// <summary>
    /// porovnání z důvodu odchylky způsobené pohybem (škubáním)
    /// </summary>
    /// <param name="value1"></param>
    /// <param name="value2"></param>
    /// <returns></returns>
    public bool ValueSmaller(double value1, double value2)
    {
        if (value1 - e < value2 || value1 + e < value2)
            return true;
        else
            return false;
    }

    //1,3  3,7
    public bool ValueEquals(double value1, double value2)
    {
        if (value2 - e <= value1 && value1 <= value2 + e)
            return true;
        else
            return false;
    }
}
