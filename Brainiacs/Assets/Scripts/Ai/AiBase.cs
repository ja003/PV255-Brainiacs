using UnityEngine;
using System.Collections;

public class AiBase : MonoBehaviour {

    public float speed{get;set;}
    public Rigidbody2D rb2d { get; set; }

    // Use this for initialization
    void Start () {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        speed = 2f;
        
    }
	
	// Update is called once per frame
	void Update () {
        

        if(Time.frameCount > 200)
        {
            MoveTo(GameObject.Find("Tesla1").transform.position.x,
                    GameObject.Find("Tesla1").transform.position.y);
        }
        else
        {
            MoveTo(-4.5, -3);
        }


    }

    /// <summary>
    /// gives copmmands to unit in order to get to the given coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void MoveTo(double x, double y)
    {
        if (ValueEquals(gameObject.transform.position.y, y) && ValueEquals(gameObject.transform.position.x, x))
        {
            //Debug.Log("You there");
            Stop();
        }
        else
        {
            if (Time.frameCount % 30 < 15)
            {
                PreferX(x,y);                
            }
            else
            {
                PreferY(x,y);
            }
        }
    }

    /// <summary>
    /// moves preferably on x axis
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void PreferX(double x, double y)
    {
        if (ValueEquals(gameObject.transform.position.y, y)) { }
        else if (ValueSmaller(gameObject.transform.position.y, y)) { MoveUp(); }
        else { MoveDown(); }
        if (ValueEquals(gameObject.transform.position.x, x)) { }
        else if (ValueSmaller(gameObject.transform.position.x, x)) { MoveRight(); }
        else { MoveLeft(); }
    }
    /// <summary>
    /// moves preferably on y axis
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void PreferY(double x, double y)
    {
        if (ValueEquals(gameObject.transform.position.x, x)) { }
        else if (ValueSmaller(gameObject.transform.position.x, x)) { MoveRight(); }
        else { MoveLeft(); }
        if (ValueEquals(gameObject.transform.position.y, y)) { }
        else if (ValueSmaller(gameObject.transform.position.y, y)) { MoveUp(); }
        else { MoveDown(); }        
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
    
    public bool ValueEquals(double value1, double value2)
    {
        if (value2 - e <= value1 && value1 <= value2 + e)
            return true;
        else
            return false;
    }
}
