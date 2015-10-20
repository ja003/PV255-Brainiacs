using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiBase : PlayerBase {
    
    

    public int killPlayer1Priority { get; set; }
    public int killPlayer2Priority { get; set; }
    public int killPlayer3Priority { get; set; }
    public int killPlayer4Priority { get; set; }

    public AiActionEnum currentAction { get; set; }

   
	
	// Update is called once per frame
	void Update () {
        
        //follor player 1 
        if(Time.frameCount > 10)
        {
            MoveTo(GameObject.Find("Player1").transform.position.x,
                    GameObject.Find("Player1").transform.position.y);
        }
        else
        {
            MoveTo(-4.5, -3);
        }

        //check only once per second
        if (Time.frameCount % 30 == 0)
        {
            CheckPriorities();
            /*
            Debug.Log("kill_1=" + killPlayer1Priority);
            Debug.Log("kill_2=" + killPlayer2Priority);
            Debug.Log("kill_3=" + killPlayer3Priority);
            Debug.Log("kill_4=" + killPlayer4Priority);
            */
            UpdateCurrentAction();

            if(currentAction == AiActionEnum.stand)
            {
                Stand();
            }
            else if (currentAction == AiActionEnum.stand)
            {
                //...
            }

            
            //Fire();

            
            //SwitchWeapon();


        }


    }

    // <<COMANDS...>>
    private List<GameObject> bullets;
    public GameObject bullet;
    int damage;

    public void InitializeBullets()
    {
        Debug.Log("!");

        bullets = new List<GameObject>();
        //bullet = GameObject.Find("Prefabs/Electricity");
        bullet = (GameObject)Resources.Load("Prefabs/Electricity");



        int pooledAmount = 5;
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.transform.parent = gameObject.transform.GetChild(0);
            obj.SetActive(false);           //nastavenie toho, ze sa gulka nepouziva
            bullets.Add(obj);

        }
    }
    
    /// <summary>
    /// nefachá se současným BulletShooterem
    /// </summary>
    public void Fire()
    {
        System.Random rnd = new System.Random();
        damage = rnd.Next(20, 30); //podla danej zbrane -> zbran musi mat min a max dmg
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].transform.position = (transform.position + new Vector3(direction.x, direction.y));
                bullets[i].transform.rotation = transform.rotation;
                bullets[i].SetActive(true);
                break;

            }
        }
    }
    
    public void SwitchWeapon()
    {
        if (inventory.Count == 1) return;

        activeWeapon = inventory[((inventory.IndexOf(activeWeapon) + 1) % inventory.Count)];
        transform.Find("weapon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(activeWeapon.sprite);
        //Debug.Log("active weapon = " + activeWeapon);
        
    }

    public void Stand()
    {

    }

    public void ShootAtPlayer(GameObject player)
    {

    }


    // <<...COMANDS>>

    public void CheckPriorities()
    {
        SetKillPriorities();
        //....
    }
    public void UpdateCurrentAction()
    {
        //...
    }

    /// <summary>
    /// nastaví kill priority podle zdálenosti
    /// </summary>
    public void SetKillPriorities()
    {
        foreach(GameObject obj in GetOtherPlayers())
        {
            int playerNumber = 0;
            if(obj.GetComponent<PlayerBase>() == null)
            {
                //Debug.Log("its AI");
                playerNumber = obj.GetComponent<AiBase>().playerNumber;
            }
            else
            {
                //Debug.Log("its human");
                playerNumber = obj.GetComponent<PlayerBase>().playerNumber;
            }
            //největší vzdálenost je cca 200
            switch (playerNumber)
            {
                case 1:
                    killPlayer1Priority = 200 - (int)(GetDistance(gameObject, obj));
                    break;
                case 2:
                    killPlayer2Priority = 200 - (int)(GetDistance(gameObject, obj));
                    break;
                case 3:
                    killPlayer3Priority = 200 - (int)(GetDistance(gameObject, obj));
                    break;
                case 4:
                    killPlayer4Priority = 200 - (int)(GetDistance(gameObject, obj));
                    break;
                default :
                    break;
            }
        }
    }

    public Vector3 GetMyPosition()
    {
        return gameObject.transform.position;
    }

    public float GetDistance(GameObject object1, GameObject object2)
    {
        return (object1.transform.position - object2.transform.position).sqrMagnitude;
    }

    public GameObject[] GetOtherPlayers()
    {
        return GameObject.FindGameObjectsWithTag("Player");
    }



    /// <summary>
    /// funguje jen když je barrier trigger....asi vymyslím jinak
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("col");
        if ((coll.gameObject.tag == "Barrier"))
        {
            //Debug.Log("Barrier");
            MoveDown();
            MoveLeft();
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
