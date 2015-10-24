using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiBase : PlayerBase {

    public PlayerBase player1;
    public PlayerBase player2;
    public PlayerBase player3;
    public PlayerBase player4;

    public int killPlayer1Priority { get; set; }
    public int killPlayer2Priority { get; set; }
    public int killPlayer3Priority { get; set; }
    public int killPlayer4Priority { get; set; }

    public AiActionEnum currentAction { get; set; }

   
	// Update is called once per frame
	void Update () {
        if(Time.frameCount == 5)
        {
            SetPlayers();
            Debug.Log("Other players set");
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

            //dont kill yourself
            switch (playerNumber)
            {
                case 1:
                    killPlayer1Priority = 0;
                    break;
                case 2:
                    killPlayer2Priority = 0;
                    break;
                case 3:
                    killPlayer3Priority = 0;
                    break;
                case 4:
                    killPlayer4Priority = 0;
                    break;
                default:
                    Debug.Log(gameObject.ToString() + " has no player number");
                    break;
            }

            UpdateCurrentAction();

            //Fire();
            //SwitchWeapon();
        }

        if(Time.frameCount % 200 == 0)
        {
            PrintPriorities();
            PrintAction();
        }

        DoCurrentAction();


    }

    // <<COMANDS...>>
    public void PrintAction()
    {
        string message = "";
        message += "current action=" + currentAction;

        Debug.Log(message);
    }

    public void PrintPriorities()
    {
        string message = "priorities \n";
        message += "KillPlayer1=" + killPlayer1Priority;
        message += ",KillPlayer2=" + killPlayer2Priority;
        message += ",KillPlayer3=" + killPlayer3Priority;
        message += ",KillPlayer4=" + killPlayer4Priority;

        Debug.Log(message);
    }

    public void DoCurrentAction()
    {
        switch (currentAction)
        {
            case AiActionEnum.killPlayer1:
                KillPlayer(player1);
                break;
            case AiActionEnum.killPlayer2:
                KillPlayer(player2);
                break;
            case AiActionEnum.killPlayer3:
                KillPlayer(player3);
                break;
            case AiActionEnum.killPlayer4:
                KillPlayer(player4);
                break;
            default: Stand();
                break;
        }
        //Debug.Log("processing action: " + currentAction);
    }



        //ˇˇˇˇpak bude v jiné třídě
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
    

    public void KillPlayer(PlayerBase player)
    {
        Vector2 targetPlayerPosition = player.transform.position;
        //move to same axis 
        
        //horizontal way is better
        if(Mathf.Abs(posX - player.posX) < Mathf.Abs(posY - player.posY))
        {
            MoveTo(player.posX, posY);
        }//vertical is better
        else
        {
            MoveTo(posX, player.posY);
        }
        //turn his direction
        LookAt(player.gameObject);

        //shoot
        //Debug.Log("killing player: " + player);
    }

    public void LookAt(GameObject obj)
    {
        
        double distanceX = Mathf.Abs(posX - obj.transform.position.x);
        double distanceY = Mathf.Abs(posY - obj.transform.position.y);

        //prefer more dominant axis
        if(distanceX > distanceY)
        {
            //it is on my left
            if (obj.transform.position.x < posX)
            {
                direction = left;
            }
            else
            {
                direction = right;
            }
        }
        else
        {
            //it is below me
            if (obj.transform.position.y < posY)
            {
                direction = down;
            }
            else
            {
                direction = up;
            }
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
    


    // <<...COMANDS>>

    public void CheckPriorities()
    {
        SetKillPriorities();
       
        //....
    }
    public void UpdateCurrentAction()
    {
        if(killPlayer1Priority >= GetCurrentHighestPriority())
        {
            currentAction = AiActionEnum.killPlayer1;
        }
        else if (killPlayer2Priority >= GetCurrentHighestPriority())
        {
            currentAction = AiActionEnum.killPlayer2;
        }
        else if (killPlayer3Priority >= GetCurrentHighestPriority())
        {
            currentAction = AiActionEnum.killPlayer3;
        }
        else if (killPlayer4Priority >= GetCurrentHighestPriority())
        {
            currentAction = AiActionEnum.killPlayer4;
        }
    }

    public int GetCurrentHighestPriority()
    {
        int highestPriority = 0;
        if (killPlayer1Priority > highestPriority)
            highestPriority = killPlayer1Priority;
        if (killPlayer2Priority > highestPriority)
            highestPriority = killPlayer2Priority;
        if (killPlayer3Priority > highestPriority)
            highestPriority = killPlayer3Priority;
        if (killPlayer4Priority > highestPriority)
            highestPriority = killPlayer4Priority;

        return highestPriority;
    }

    /// <summary>
    /// nastaví kill priority podle zdálenosti
    /// 200 je cca max vzdálenost na mapě
    /// </summary>
    public void SetKillPriorities()
    {
        killPlayer1Priority = 200 - (int)(GetDistance(gameObject, player1.gameObject));
        killPlayer2Priority = 200 - (int)(GetDistance(gameObject, player2.gameObject));
        killPlayer3Priority = 200 - (int)(GetDistance(gameObject, player3.gameObject));
        //killPlayer4Priority = 200 - (int)(GetDistance(gameObject, player4.gameObject));
            
        
    }

    public Vector3 GetMyPosition()
    {
        return gameObject.transform.position;
    }

    public float GetDistance(GameObject object1, GameObject object2)
    {
        return (object1.transform.position - object2.transform.position).sqrMagnitude;
    }

    public List<PlayerBase> GetPlayers()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");
        List<PlayerBase> allPlayersBase = new List<PlayerBase>();
        foreach(GameObject obj in allPlayers)
        {
            allPlayersBase.Add(obj.GetComponent<PlayerBase>());
        }
        return allPlayersBase;
    }
    public void SetPlayers()
    {
        List<PlayerBase> allPlayers = new List<PlayerBase>();
        allPlayers = GetPlayers();
        foreach(PlayerBase player in allPlayers)
        {
            switch (player.playerNumber)
            {
                case 1:
                    player1 = player;
                    break;
                case 2:
                    player2 = player;
                    break;
                case 3:
                    player3 = player;
                    break;
                case 4:
                    player4 = player;
                    break;
                default:
                    Debug.Log(player.ToString() + " has no player number!");
                    break;
            }
            Debug.Log("setting player: " + player.gameObject.name + ", number-" + player.playerNumber);
        }
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
        //Debug.Log("moving to: " + x + "," + y);
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
