using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiBase : PlayerBase {

    public PlayerBase player1;
    public PlayerBase player2;
    public PlayerBase player3;
    public PlayerBase player4;

    Components comp;
    PlayerInfo playInfo;

    public int killPlayer1Priority { get; set; }
    public int killPlayer2Priority { get; set; }
    public int killPlayer3Priority { get; set; }
    public int killPlayer4Priority { get; set; }

    public Vector2 lastPosition;
    public Vector2 preLastPosition;

    /// //////////////////////////////////// CHARACTER COORDINATES /////////////////////////////
    Renderer renderer;
    Collider2D collider;

    public Vector2 botLeft;
    public Vector2 botRight;
    public Vector2 topLeft;
    public Vector2 topRight;

    public Vector2 currentTargetDestination;

    public AiActionEnum currentAction { get; set; }

    public void setUpAiB(Components c, PlayerInfo p)
    {
        comp = c;
        playInfo = p;
        //weaponHandling = GetComponent<WeaponHandling>();
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        //Debug.Log(rb2d);
        renderer = GetComponent<Renderer>();
        currentTargetDestination = new Vector2(0, 0);
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update () {
        if(Time.frameCount == 5)
        {
            SetPlayers();
            //Debug.Log("Other players set");
            lastPosition = new Vector2(0, 0);
            lastPosition = new Vector2(0, 1);
        }

        

        //check only once per second
        if (Time.frameCount % 30 == 6)
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
            UpdateLastPosition();
            


            //Fire();
            //SwitchWeapon();
        }

        if (Time.frameCount % 200 == 0)
        {
            PrintPriorities();
            PrintAction();
        }

        DoCurrentAction();
        UpdateDirection();


    }
    

    public void UpdateLastPosition()
    {
        preLastPosition = lastPosition;
        lastPosition = gameObject.transform.position;
    }

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

        //Debug.Log(message);
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

    public bool CanShoot(Vector2 center, Vector2 direction)
    {
        
        Ray rayGun = new Ray(center, direction);

        float mapLenght = 15;
        RaycastHit2D[] hitGun = Physics2D.RaycastAll(rayGun.origin, direction, mapLenght);

        Debug.DrawRay(rayGun.origin, direction, Color.cyan);
        //Debug.DrawRay(rayGun.origin, direction*-1, Color.red);

        if (hitGun.Length != 0)
        {
            //for (int i = 0; i < hitGun.Length; i++)
            //{
            //    Debug.Log("hit from " + center + " to " + hitGun[i].transform.name + " in dir:" + direction);
            //}
            //Debug.Log("---");
            if (hitGun[0].transform.tag == "Barrier")
            {
                //Debug.Log("cant shoot");
                return false;
            }
        }
        return true;
    }

    public bool IsInPlayground(Vector2 point)
    {
        return mapMinX < point.x && point.x < mapMaxX && mapMinY < point.y && point.y < mapMaxY;
    }

    /// <summary>
    /// TODO: nastavení destinace - aby se nekrylo s barrierou
    /// </summary>
    /// <param name="player"></param>
    public void KillPlayer(PlayerBase player)
    {
        Vector2 targetPlayerPosition = player.transform.position;

        //move to same axis 
        float targetX;
        float targetY;

        Vector2 bestHorizontal = new Vector2(player.posX, player.posY);
        Vector2 bestVertical = new Vector2(player.posX, player.posY);

        Vector2 bestHorizontalUp = bestHorizontal;
        Vector2 bestHorizontalDown = bestHorizontal;

        Vector2 bestVerticalLeft = bestVertical;
        Vector2 bestVerticalRight = bestVertical;
        

        //CanShoot volá špatně, nebo blbě funguje

        while (bestHorizontalDown.y > mapMinY && !ObjectCollides(bestHorizontalDown) && bestHorizontalDown.y > posY)
        {
            bestHorizontalDown.y -= 0.1f;
        }
        while (bestHorizontalUp.y < mapMaxY && !ObjectCollides(bestHorizontalUp) && bestHorizontalUp.y < posY)
        {
            bestHorizontalUp.y += 0.1f;
        }
        while (bestVerticalLeft.x > mapMinX && !ObjectCollides(bestVerticalLeft) && bestVerticalLeft.x > posX)
        {
            bestVerticalLeft.x -= 0.1f;
        }
        while (bestVerticalRight.x < mapMaxX && !ObjectCollides(bestVerticalRight) && bestVerticalRight.x < posX)
        {
            bestVerticalRight.x += 0.1f;
        }

        List<Vector2> possibleShootSpots = new List<Vector2>();
        possibleShootSpots.Add(bestHorizontalDown);
        possibleShootSpots.Add(bestHorizontalUp);
        possibleShootSpots.Add(bestVerticalLeft);
        possibleShootSpots.Add(bestVerticalRight);
        
        int spotIndex = 0;
        float distanceFromMe = 100;
        for(int i = 0; i < possibleShootSpots.Count;i++)
        {
            if(distanceFromMe > GetDistance(gameObject.transform.position, possibleShootSpots[i]))
            {
                spotIndex = i;
                distanceFromMe = GetDistance(gameObject.transform.position, possibleShootSpots[i]);
            }
        }
        
        MoveTo(possibleShootSpots[spotIndex]);
        
        //if you are on same axis -> turn his direction and shoot
        if (AlmostEqual(posX,player.posX,0.1)||AlmostEqual(posY,player.posY,0.1))
        {
            LookAt(player.gameObject);
            if (CanShoot(transform.position, direction))
            {
                //Debug.Log("I can shoot form:" + transform.position + " to: " + direction);
                weaponHandling.fire(direction);
            }
        }
        
    }

    public bool AlmostEqual(float pos1, float pos2, double e)
    {
        return pos2 - e < pos1 && pos1 < pos2 + e;
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


    /*
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
    */


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

    public float GetDistance(Vector2 pos1, Vector2 pos2)
    {
        return (pos1 - pos2).sqrMagnitude;
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
            //Debug.Log("setting player: " + player.gameObject.name + ", number: " + player.playerNumber);
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
    /// řeší stav po skončení kolize
    /// bez toho by se zaseknul na jednom místě
    /// </summary>
    public void AfterCollision()
    {

        if (lastDirection == down || lastDirection == up)
        {
            if (!Collides(right))
            {
                MoveRight();
            }
            else if (!Collides(left))
            {
                MoveLeft();
            }
        }
        else if (lastDirection == left || lastDirection == right)
        {
            if (!Collides(up))
            {
                MoveUp();
            }
            else if (!Collides(down))
            {
                MoveDown();
            }
        }
    }


    public void MoveTo(Vector2 position)
    {
        MoveTo(position.x, position.y);
    }

    /// <summary>
    /// gives commands to unit in order to get to the given coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void MoveTo(float x, float y)
    {
        if (ValueEquals(gameObject.transform.position.y, y) && ValueEquals(gameObject.transform.position.x, x))
        {
            //Debug.Log("You there");
            Stop();
        }
        else if(AvoidBariers())
        {
            if (Time.frameCount % 30 < 15)
            {
                AfterCollision();
                PreferX(x, y);
            }
            else
            {
                AfterCollision();
                PreferY(x, y);
            }
        }
        currentTargetDestination = new Vector2(x, y);
        //Debug.Log("moving to: " + x + "," + y);
    }

    public bool ObjectCollides(Vector2 center)
    {
        return ObjectCollides(center, 0.5f, right, 0.1f);
    }

    public bool ObjectCollides(Vector2 center, float width, Vector2 direction, float distance)
    {
        LayerMask layerMask = barrierMask;

        botLeft = new Vector2(center.x - width/2, center.y - width / 2);
        botRight = new Vector2(center.x + width / 2, center.y - width / 2);
        topLeft = new Vector2(center.x - width / 2, center.y + width / 2);
        topRight = new Vector2(center.x + width / 2, center.y + width / 2);

        RaycastHit2D hitBotLeft;
        Ray rayBotLeft = new Ray(botLeft, direction);
        RaycastHit2D hitBotRight;
        Ray rayBotRight = new Ray(botRight, direction);
        RaycastHit2D hitTopLeft;
        Ray rayTopLeft = new Ray(topLeft, direction);
        RaycastHit2D hitTopRight;
        Ray rayTopRight = new Ray(topRight, direction);


        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, direction, distance, layerMask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, direction, distance, layerMask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, direction, distance, layerMask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, direction, distance, layerMask);

        Debug.DrawRay(currentTargetDestination, left, Color.red);
        Debug.DrawRay(currentTargetDestination, up, Color.blue);
        

        if (hitBotLeft || hitBotRight || hitTopLeft || hitTopRight)
            return true;

        return false;
    }

    public bool Collides(Vector2 direction, float distance)
    {
        LayerMask layerMask = barrierMask;

        botLeft = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
        botRight = new Vector2(collider.bounds.max.x, collider.bounds.min.y);
        topLeft = new Vector2(collider.bounds.min.x, collider.bounds.max.y);
        topRight = new Vector2(collider.bounds.max.x, collider.bounds.max.y);

        RaycastHit2D hitBotLeft;
        Ray rayBotLeft = new Ray(botLeft, direction);
        RaycastHit2D hitBotRight;
        Ray rayBotRight = new Ray(botRight, direction);
        RaycastHit2D hitTopLeft;
        Ray rayTopLeft = new Ray(topLeft, direction);
        RaycastHit2D hitTopRight;
        Ray rayTopRight = new Ray(topRight, direction);


        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, direction, distance, layerMask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, direction, distance, layerMask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, direction, distance, layerMask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, direction, distance, layerMask);

        Debug.DrawRay(currentTargetDestination, left, Color.red);
        Debug.DrawRay(currentTargetDestination, up, Color.blue);

        //Debug.DrawRay(botLeft, direction, Color.cyan);
        //Debug.DrawRay(botRight, direction, Color.green);
        //Debug.DrawRay(topLeft, direction, Color.yellow);
        //Debug.DrawRay(topRight, direction, Color.red);

        //if(hitTopRight.rigidbody != null)
            //Debug.Log(hitTopRight.rigidbody.transform.name);

        if (hitBotLeft || hitBotRight || hitTopLeft || hitTopRight)
            return true;

        return false;
    }

    public bool Collides(Vector2 direction)
    {
        return Collides(direction, 0.1f);
    }

    public bool Collides(float distance)
    {
        return Collides(left, distance) || Collides(up, distance)|| Collides(right, distance)|| Collides(down, distance);
    }

    //old
    public bool Collides(Vector2 center, float extens, float width, LayerMask layermask, Vector2 direction)
    {
        botLeft = new Vector2(center.x - extens+width, center.y - extens+ width);
        botRight = new Vector2(center.x + extens- width, center.y - extens+ width);
        topLeft = new Vector2(center.x - extens+ width, center.y + extens- width);
        topRight = new Vector2(center.x + extens- width, center.y + extens- width);

        RaycastHit2D hitBotLeft;
        Ray rayBotLeft = new Ray(botLeft, direction);
        RaycastHit2D hitBotRight;
        Ray rayBotRight = new Ray(botRight, direction);
        RaycastHit2D hitTopLeft;
        Ray rayTopLeft = new Ray(topLeft, direction);
        RaycastHit2D hitTopRight;
        Ray rayTopRight = new Ray(topRight, direction);
        

        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, direction, width, layermask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, direction, width, layermask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, direction, width, layermask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, direction, width, layermask);

        Debug.DrawRay(currentTargetDestination, left,Color.red);
        Debug.DrawRay(currentTargetDestination, up,Color.blue);

        Debug.DrawRay(botLeft, direction, Color.cyan);
        //Debug.DrawRay(botRight, direction, Color.green);
        //Debug.DrawRay(topLeft, direction, Color.yellow);
        //Debug.DrawRay(topRight, direction, Color.red);

        if (hitBotLeft || hitBotRight || hitTopLeft || hitTopRight)
            return true;
        
        return false;
    }
    //old
    public bool Collides(Vector2 center, float extens, float width, LayerMask layermask)
    {
        botLeft = new Vector2(center.x - extens + width, center.y - extens + width);
        botRight = new Vector2(center.x + extens - width, center.y - extens + width);
        topLeft = new Vector2(center.x - extens + width, center.y + extens - width);
        topRight = new Vector2(center.x + extens - width, center.y + extens - width);

        RaycastHit2D hitBotLeft;
        Ray rayBotLeft = new Ray(botLeft, direction);
        RaycastHit2D hitBotRight;
        Ray rayBotRight = new Ray(botRight, direction);
        RaycastHit2D hitTopLeft;
        Ray rayTopLeft = new Ray(topLeft, direction);
        RaycastHit2D hitTopRight;
        Ray rayTopRight = new Ray(topRight, direction);

        Vector2 allDirection = new Vector2(0, 0);

        allDirection = left;
        
        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, allDirection, width, layermask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, allDirection, width, layermask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, allDirection, width, layermask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, allDirection, width, layermask);

        if (hitBotLeft || hitBotRight || hitTopLeft || hitTopRight)
            return true;

        allDirection = up;

        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, allDirection, width, layermask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, allDirection, width, layermask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, allDirection, width, layermask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, allDirection, width, layermask);

        if (hitBotLeft || hitBotRight || hitTopLeft || hitTopRight)
            return true;

        allDirection = right;

        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, allDirection, width, layermask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, allDirection, width, layermask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, allDirection, width, layermask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, allDirection, width, layermask);

        if (hitBotLeft || hitBotRight || hitTopLeft || hitTopRight)
            return true;

        allDirection = down;

        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, allDirection, width, layermask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, allDirection, width, layermask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, allDirection, width, layermask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, allDirection, width, layermask);

        if (hitBotLeft || hitBotRight || hitTopLeft || hitTopRight)
            return true;
        
        return false;
    }
    //old
    public bool Collides(Vector2 center, Vector2 direction)
    {
        return Collides(center, 0.5f, 0.3f, barrierMask, direction);
    }


    public float characterWidth = 0.5f;

    /// <summary>
    /// bacha, dělá to skoky po 1, přitom charWidth = 0.5..uvidí se, jak to pofachá
    /// </summary>
    /// <param name="node"></param>
    /// <param name="visited"></param>
    /// <returns></returns>
    public List<Vector2> GetPossibleDirections(Vector2 node, List<Vector2> visited)
    {
        List<Vector2> possibleDirections = new List<Vector2>();

        if (!Collides(node + left, left) && !visited.Contains(node + left))
        {
            Debug.Log("L");
            possibleDirections.Add(node + left);
        }
        if (!Collides(node + up, up) && !visited.Contains(node + up))
        {
            Debug.Log("U");
            possibleDirections.Add(node + up);
        }
        if (!Collides(node + right,right) && !visited.Contains(node + right))
        {
            Debug.Log("R");
            possibleDirections.Add(node + right);
        }
        if (!Collides(node + down,down) && !visited.Contains(node + down))
        {
            Debug.Log("D");
            possibleDirections.Add(node + down);
        }
        Debug.Log("found directions:");
        Debug.Log(PrintNodeList(possibleDirections));

        return possibleDirections;
    }

    public string PrintNodeList(List<Vector2> list)
    {
        string result = "";
        foreach(Vector2 node in list)
        {
            result += "[" + node.x + "," + node.y + "],";
        }
        return result;
    }

    private Vector2 lastDirection;

    public bool FindPath()
    {
        //Vector2 currentPosition = transform.position;

        if(Collides(up))
        {
            //Debug.Log("col-U");
            if (!Collides(right) && lastDirection != left)
            {
                MoveRight();
                lastDirection = right;
            }
            else if (!Collides(left) && lastDirection != right)
            {
                MoveLeft();
                lastDirection = left;
            }

            else if(lastDirection!=up)
            {
                MoveDown();
                lastDirection = down;
            }
            //Debug.Log(lastDirection);
            return false;
        }
        if (Collides(right))
        {
            //Debug.Log("col-R");
            if (!Collides(up) && lastDirection != down)
            {
                MoveUp();
                lastDirection = up;
                //Debug.Log("goUp");
            }
            else if (!Collides(down) && lastDirection != up)
            {
                MoveDown();
                lastDirection = down;
                //Debug.Log("goDown");
                
            }

            else if (lastDirection != right)
            {
                MoveLeft();
                lastDirection = left;
                //Debug.Log("goLeft");
            }
            //Debug.Log(lastDirection);
            return false;
        }
        if (Collides(down))
        {
            //Debug.Log("col-D");

            if (!Collides(left) && lastDirection != right)
            {
                MoveLeft();
                lastDirection = left;
            }
            else if (!Collides(right) && lastDirection != left)
            {
                MoveRight();
                lastDirection = right;
            }

            else if (lastDirection != down)
            {
                MoveUp();
                lastDirection = up;
            }
            //Debug.Log(lastDirection);
            return false;
        }
        if (Collides(left))
        {
            //Debug.Log("col-L");
            if (!Collides(up) && lastDirection != down)
            {
                MoveUp();
                lastDirection = up;
            }
            else if (!Collides(down) && lastDirection != up)
            {
                MoveDown();
                lastDirection = down;
            }

            else if (lastDirection != left)
            {
                MoveRight();
                lastDirection = right;
            }
            //Debug.Log(lastDirection);
            return false;
        }

        //Debug.Log("---");
        return true;
    }

    //s tímto návrhem pohybu k ničemu
    /*
        for (int i=0;i<visited.Count;i++)
        {
            Vector2 node = visited[i];
            Debug.Log("visited:");
            Debug.Log(PrintNodeList(visited));
            if (GetDistance(node, currentTargetDestination) < characterWidth)
            {
                return true;
            }
            possibleDirections = GetPossibleDirections(node, visited);
            Debug.Log("possible:");
            foreach(Vector2 newNode in possibleDirections)
            {
                visited.Add(newNode);
            }
            possibleDirections.Clear();

        }
        */



    //detect only collision with barriers (assigned manually to prefab)
    public LayerMask barrierMask;
    public bool decided = false;

    /// <summary>
    /// returns false while there is still some collision with barrier
    /// </summary>
    /// <returns>state of avoiding barrier</returns>
    public bool AvoidBariers()
    {
        if (Collides(0.1f))
        {
            //Debug.Log("COL");
            return FindPath();
        }
        else
        {
            //Debug.Log("avoided");
            return true;
        }
    }



    /// <summary>
    /// moves to [x,y] preferably on x axis
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
    /// moves  to [x,y] preferably on y axis
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

    public void Move(Vector2 direction)
    {
        if (direction == left)
            MoveLeft();
        else if (direction == right)
            MoveRight();
        else if (direction == up)
            MoveUp();
        else if (direction == down)
            MoveDown();
    }
    public void MoveUp()
    {
        rb2d.velocity = Vector2.up * speed;
        direction = up;
        //Debug.Log("moving up");
        //Debug.Log("new dir:" + direction);
    }
    public void MoveLeft()
    {
        rb2d.velocity = Vector2.left * speed;
        direction = left;
    }
    public void MoveDown()
    {
        rb2d.velocity = Vector2.down * speed;
        direction = down;
    }
    public void MoveRight()
    {
        rb2d.velocity = Vector2.right * speed;
        direction = right;
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
