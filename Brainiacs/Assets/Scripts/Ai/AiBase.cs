using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiBase : PlayerBase
{

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

    ////MAP shit
    public GameObject[] barriers;


    /// //////////////////////////////////// CHARACTER COORDINATES /////////////////////////////
    Renderer renderer;
    Collider2D collider;

    public Vector2 charBotLeft;
    public Vector2 charBotRight;
    public Vector2 charTopLeft;
    public Vector2 charTopRight;

    public Vector2 charBot;
    public Vector2 charTop;
    public Vector2 charLeft;
    public Vector2 charRight;

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

        walkingFront = new List<Vector2>();

        barriers = GameObject.FindGameObjectsWithTag("Barrier");
        //Debug.Log(barriers[0].name);
        //Debug.Log(barriers[1].name);
        //Debug.Log(barriers[2].name);
        //Debug.Log(barriers[3].name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount == 5)
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
            default:
                Stand();
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
        Debug.DrawRay(rayGun.origin, direction*-1, Color.red);

        if (hitGun.Length != 0)
        {
            //for (int i = 0; i < hitGun.Length; i++)
            //{
            //    Debug.Log("hit from " + center + " to " + hitGun[i].transform.name + " in dir:" + direction);
            //}
            //Debug.Log("---");
            //Borders have tag "Barrier" and it sometimes doesnt hit player first, but the border
            if (hitGun[0].transform.tag == "Barrier" 
                && hitGun[0].transform.gameObject.layer != LayerMask.NameToLayer("Border"))
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

        
        while (bestHorizontalDown.y > mapMinY && !CharacterCollidesBarrier(bestHorizontalDown) && bestHorizontalDown.y > posY)
        {
            bestHorizontalDown.y -= 0.1f;
        }

        //mapMaxY = 4f;

        





        while (bestHorizontalUp.y < mapMaxY && !CharacterCollidesBarrier(bestHorizontalUp) && bestHorizontalUp.y < posY)
        {
            bestHorizontalUp.y += 0.1f;
        }
        while (bestVerticalLeft.x > mapMinX && !CharacterCollidesBarrier(bestVerticalLeft) && bestVerticalLeft.x > posX)
        {
            bestVerticalLeft.x -= 0.1f;
        }
        while (bestVerticalRight.x < mapMaxX && !CharacterCollidesBarrier(bestVerticalRight) && bestVerticalRight.x < posX)
        {
            bestVerticalRight.x += 0.1f;
        }

        List<Vector2> possibleShootSpots = new List<Vector2>();
        possibleShootSpots.Add(bestHorizontalDown);
        possibleShootSpots.Add(bestHorizontalUp);
        possibleShootSpots.Add(bestVerticalLeft);
        possibleShootSpots.Add(bestVerticalRight);

        int spotIndex = 0;
        float distanceFromMe = 100; //big enough
        for (int i = 0; i < possibleShootSpots.Count; i++)
        {
            
            //Debug.Log(possibleShootSpots[i]);
            if (distanceFromMe > GetDistance(gameObject.transform.position, possibleShootSpots[i]))
            {
                spotIndex = i;
                distanceFromMe = GetDistance(gameObject.transform.position, possibleShootSpots[i]);
            }
        }

        //Debug.Log("move to: " + possibleShootSpots[spotIndex]);
        Debug.DrawRay(possibleShootSpots[spotIndex], up, Color.cyan);
        MoveTo(possibleShootSpots[spotIndex]);


        //if you are on same axis -> turn his direction and shoot
        
        if (AlmostEqual(posX,player.posX,0.1)||AlmostEqual(posY,player.posY,0.1))
        {
            //Debug.Log("i can shoot");
            LookAt(player.gameObject);
            if (CanShoot(transform.position, direction))
            {
                //Debug.Log("I can shoot from:" + transform.position + " to: " + direction);
                //chybí implementace kadence zbraně
                if (Time.frameCount % 10 == 0)
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
        if (distanceX > distanceY)
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
        Stop();
    }

    // <<...COMMANDS>>

    public void CheckPriorities()
    {
        SetKillPriorities();

        //....
    }
    public void UpdateCurrentAction()
    {
        if (killPlayer1Priority >= GetCurrentHighestPriority())
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
        killPlayer4Priority = 200 - (int)(GetDistance(gameObject, player4.gameObject));


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
        foreach (GameObject obj in allPlayers)
        {
            allPlayersBase.Add(obj.GetComponent<PlayerBase>());
        }
        return allPlayersBase;
    }

    public void SetPlayers()
    {
        List<PlayerBase> allPlayers = new List<PlayerBase>();
        allPlayers = GetPlayers();
        foreach (PlayerBase player in allPlayers)
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
            Debug.Log("setting player: " + player.gameObject.name + ", number: " + player.playerNumber);
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

    public List<Vector2> walkingFront;

    public List<Vector2> GetNodes()
    {
        List<Vector2> nodes = new List<Vector2>();
        nodes.Add(new Vector2(0, 0));
        nodes.Add(new Vector2(1, 0));
        nodes.Add(new Vector2(1, 1));
        nodes.Add(new Vector2(2, 1));
        return nodes;
    }

    /// <summary>
    /// node for pathfinding algorithm
    /// </summary>
    public class PathNode
    {
        public Vector2 node;
        public int parentIndex;

        public PathNode(Vector2 node, int parentIndex)
        {
            this.node = node;
            this.parentIndex = parentIndex;

        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            PathNode p = obj as PathNode;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return node == p.node;
        }

        public override string ToString()
        {
            return node.ToString();
        }
    }

    public List<Vector2> GetPathTo(Vector2 target)
    {
        float step = 0.3f;
        //float step = characterWidth/2;
        //Debug.Log(characterWidth);
        //um....wtf, proč když nenapíšu ručně 0.5f tak to nejde?

        List<Vector2> path = new List<Vector2>();
        PathNode startNode = new PathNode(new Vector2(posX, posY), 0);
        List<PathNode> visitedNodes = new List<PathNode>();
        //Debug.Log("start: " + startNode.node);

        bool found = false;
        int finalNodeIndex = 0;
        //5!!!!!!!!!!!!!!!
        for(int start = 0; start < 5; start++){
            //Debug.Log("start:"+start);
            visitedNodes.Clear();
            switch (start)
            {
                case 0://center
                    startNode = new PathNode(new Vector2(posX, posY), 0);
                    break;
                case 1://left
                    startNode = new PathNode(new Vector2(posX-step/2, posY), 0);
                    break;
                case 2://up
                    startNode = new PathNode(new Vector2(posX, posY+ step / 2), 0);
                    break;
                case 3://right
                    startNode = new PathNode(new Vector2(posX+ step / 2, posY), 0);
                    break;
                case 4://down
                    startNode = new PathNode(new Vector2(posX, posY- step / 2), 0);
                    break;
                default:
                    break;
            }   
            //Debug.Log("startNode:"+ startNode);

            visitedNodes.Add(startNode);

            for (int i = 0; i < visitedNodes.Count; i++)
            {
                //Debug.Log(i);
                PathNode currentNode = visitedNodes[i];
                //end process when current node is close to target
                if (GetDistance(currentNode.node, target) < 2 * step)
                {
                    //Debug.Log("final = " + currentNode.node);
                    finalNodeIndex = i;
                    found = true;
                    break;                    
                }
                if (i > 5000)
                {
                    Debug.Log("FAIL");
                    finalNodeIndex = 100;
                    break;
                }

                if (ValueEquals(currentNode.node.x, 2.4) && ValueEquals(currentNode.node.y, -1.7))
                {
                    Debug.Log("!");
                }

                //set neighbouring nodes
                PathNode nodeLeft = new PathNode(new Vector2(currentNode.node.x - step, currentNode.node.y), i);
                PathNode nodeUp = new PathNode(new Vector2(currentNode.node.x, currentNode.node.y + step), i);
                PathNode nodeRight = new PathNode(new Vector2(currentNode.node.x + step, currentNode.node.y), i);
                PathNode nodeDown = new PathNode(new Vector2(currentNode.node.x, currentNode.node.y - step), i);
                //add to list if there is no collision and they are not already in list
                if (!CharacterCollidesBarrier(nodeLeft.node) && !visitedNodes.Contains(nodeLeft))
                { visitedNodes.Add(nodeLeft); }
                if (!CharacterCollidesBarrier(nodeUp.node) && !visitedNodes.Contains(nodeUp))
                { visitedNodes.Add(nodeUp); }
                if (!CharacterCollidesBarrier(nodeRight.node) && !visitedNodes.Contains(nodeRight))
                { visitedNodes.Add(nodeRight); }
                if (!CharacterCollidesBarrier(nodeDown.node) && !visitedNodes.Contains(nodeDown))
                { visitedNodes.Add(nodeDown); }

                if (ValueEquals(currentNode.node.x, 2.4) && ValueEquals(currentNode.node.y, -1.7))
                {
                    Debug.Log("!-------------------------------------------------------------------------");
                    Debug.Log("current:" + currentNode);
                    Debug.Log("index:" + i);
                    Debug.Log("CharacterCollides-right: " + CharacterCollides(nodeRight.node));
                    Debug.Log("visitedNodes.Contains-right: " + visitedNodes.Contains(nodeRight));
                    Debug.Log("!CharacterCollides(nodeRight.node) && !visitedNodes.Contains(nodeRight)");
                    Debug.Log(!CharacterCollides(nodeRight.node) && !visitedNodes.Contains(nodeRight));

                    Debug.Log("right:" + nodeRight);
                }
                if (ValueEquals(currentNode.node.x, 2.7) && ValueEquals(currentNode.node.y, -1.7))
                {
                    Debug.Log("!-------------------------------------------------------------------------");
                    Debug.Log("current:" + currentNode);
                    Debug.Log("index:" + i);
                    Debug.Log("parentIndex:" + currentNode.parentIndex);
                    Debug.Log("parent:" + visitedNodes[currentNode.parentIndex]);
                }

            }

            if (found)
                break;
        }

        //reversly find the way back to starting node
        int index = finalNodeIndex;
        while (index != 0)
        {
            path.Add(visitedNodes[index].node);
            index = visitedNodes[index].parentIndex;
            //Debug.Log(visitedNodes[index].node);
            
        }
        //reverse path
        path.Reverse();

        
        

        return path;
    }

    /// <summary>
    /// gives commands to unit in order to get to the given coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void MoveTo(float x, float y)
    {
        if(ValueEquals(posX, x) && ValueEquals(posY, y))
        {
            Stand();
            //Debug.Log("you there");
            return;
        }

        //refresh path only when target moves
        if (!ValueEquals(currentTargetDestination.x, x) || !ValueEquals(currentTargetDestination.y, y))
        {
            //Debug.Log("oldTarget:" + currentTargetDestination);
            //Debug.Log("newTarget:" + x + "," +y);
            //Debug.Log("recalculating");
            walkingFront = GetPathTo(new Vector2(x, y));
            currentTargetDestination = new Vector2(x, y);
        }

        if (Time.frameCount % 30 == 0)
        {
            //Debug.Log("walkFront:" + walkingFront.Count);
            foreach(Vector2 v in walkingFront)
            {
                //Debug.Log(v);
            }
        }
        if (walkingFront.Count == 0)
        {
            if (Time.frameCount % 30 < 15)
            {
                PreferX(x, y);
            }
            else
            {
                PreferY(x, y);
            }
        }
        else
        {
            //draw path
            for(int i = 0; i< walkingFront.Count; i++)
            {
                if(i+1 != walkingFront.Count)
                    Debug.DrawLine(walkingFront[i], walkingFront[i + 1], Color.blue);
            }
            
            Vector2 currentNode = walkingFront[0];
            if (ValueEquals(gameObject.transform.position.y, currentNode.y) && ValueEquals(gameObject.transform.position.x, currentNode.x))
            {
                walkingFront.RemoveAt(0);
            }
            else
            {
                if (Time.frameCount % 30 < 15)
                {
                    PreferX(currentNode.x, currentNode.y);
                }
                else
                {
                    PreferY(currentNode.x, currentNode.y);
                }
            }
        }
    }

    
    /// <summary>
    /// chekovat pouze jeden směr nestačí
    /// </summary>
    /// <param name="center"></param>
    /// <returns></returns>
    public bool CharacterCollides(Vector2 center)
    {
        Vector2 colliderOffset = GetComponent<Collider2D>().offset;
        float colliderWidth = GetComponent<Collider2D>().bounds.size.x;
        float distance = 0.1f;

        bool colRight = ObjectCollides(center + colliderOffset, colliderWidth, right, distance);
        //bool colLeft = ObjectCollides(center + colliderOffset, colliderWidth, left, distance);
        bool colUp = ObjectCollides(center + colliderOffset/2, colliderWidth, up, distance);
        //bool colDown = ObjectCollides(center + colliderOffset, colliderWidth, down, distance);

        //return colRight || colLeft || colUp || colDown;
        //return colRight || colLeft;
        //return colRight;
        return  colUp;
    }
    

    /// <summary>
    /// new collisioncheck using box collider bounds
    /// </summary>
    /// <returns></returns>
    public bool CharacterCollidesBarrier(Vector2 center)
    {
        //Debug.Log("colCheck");
        float width = gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2;
        Vector2 colliderOffset = GetComponent<Collider2D>().offset / 2;
        float offset = 0.1f;

        Vector2 botLeft = new Vector2(center.x - width - offset, center.y - width - offset);
        Vector2 botRight = new Vector2(center.x + width + offset, center.y - width - offset);
        Vector2 topLeft = new Vector2(center.x - width - offset, center.y + width + offset);
        Vector2 topRight = new Vector2(center.x + width + offset, center.y + width + offset);

        

        for (int i = 0; i < barriers.Length; i++)
        {
            if (barriers[i].GetComponent<BoxCollider2D>().bounds.Contains(botLeft + colliderOffset))
                return true;
            if (barriers[i].GetComponent<BoxCollider2D>().bounds.Contains(botRight + colliderOffset))
                return true;
            if (barriers[i].GetComponent<BoxCollider2D>().bounds.Contains(topLeft + colliderOffset))
                return true;
            if (barriers[i].GetComponent<BoxCollider2D>().bounds.Contains(topRight + colliderOffset))
                return true;
        }

        //Debug.DrawLine(botLeft, botRight, Color.green, 2f);
        //Debug.DrawLine(botLeft, topLeft, Color.green, 2f);
        //Debug.DrawLine(topLeft, topRight, Color.green, 2f);
        //Debug.DrawLine(topRight, botRight, Color.green, 2f);

        return false;
    }


    /// <summary>
    /// bot, top, left, right might be obsolete
    /// </summary>
    /// <returns></returns>
    public bool ObjectCollides(Vector2 center, float width, Vector2 direction, float distance)
    {
        //Debug.Log("width:" + width);
        LayerMask layerMask = barrierMask;
        charBotLeft = new Vector2(center.x - width / 2, center.y - width / 2);
        charBotRight = new Vector2(center.x + width / 2, center.y - width / 2);
        charTopLeft = new Vector2(center.x - width / 2, center.y + width / 2);
        charTopRight = new Vector2(center.x + width / 2, center.y + width / 2);
        

        //charBot = new Vector2(center.x - width / 2, center.y - width / 2);
        //charTop = new Vector2(center.x + width / 2, center.y - width / 2);
        //charLeft = new Vector2(center.x - width / 2, center.y + width / 2);
        //charRight = new Vector2(center.x + width / 2, center.y + width / 2);

        
        /*
        for(int i = 0; i < barriers.Length; i++)
        {
            if (barriers[i].GetComponent<BoxCollider2D>().bounds.Contains(charBotLeft))
                return true;
            if (barriers[i].GetComponent<BoxCollider2D>().bounds.Contains(charBotRight))
                return true;
            if (barriers[i].GetComponent<BoxCollider2D>().bounds.Contains(charTopLeft))
                return true;
            if (barriers[i].GetComponent<BoxCollider2D>().bounds.Contains(charTopRight))
                return true;
        }
        */
        
        
        RaycastHit2D hitBotLeft;
        Ray rayBotLeft = new Ray(charBotLeft, direction);
        RaycastHit2D hitBotRight;
        Ray rayBotRight = new Ray(charBotRight, direction);
        RaycastHit2D hitTopLeft;
        Ray rayTopLeft = new Ray(charTopLeft, direction);
        RaycastHit2D hitTopRight;
        Ray rayTopRight = new Ray(charTopRight, direction);

        //RaycastHit2D hitTop;
        //Ray rayBot= new Ray(charBot, direction);
        //RaycastHit2D hitBot;
        //Ray rayTop = new Ray(charTop, direction);
        //RaycastHit2D hitLeft;
        //Ray rayLeft = new Ray(charLeft, direction);
        //RaycastHit2D hitRight;
        //Ray rayRight = new Ray(charRight, direction);


        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, direction, distance, layerMask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, direction, distance, layerMask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, direction, distance, layerMask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, direction, distance, layerMask);

        //hitTop = Physics2D.Raycast(rayTop.origin, direction, distance, layerMask);
        //hitBot = Physics2D.Raycast(rayBot.origin, direction, distance, layerMask);        
        //hitLeft = Physics2D.Raycast(rayLeft.origin, direction, distance, layerMask);
        //hitRight = Physics2D.Raycast(rayRight.origin, direction, distance, layerMask);

        //Debug.DrawRay(currentTargetDestination, left, Color.red);
        //Debug.DrawRay(currentTargetDestination, up, Color.blue);

        //Debug.DrawRay(rayBotLeft.origin , direction, Color.blue);
        //Debug.DrawRay(rayBotRight.origin , direction, Color.red);


        if (hitBotLeft || hitBotRight || hitTopLeft || hitTopRight)
        {
            //Debug.DrawLine(charBotLeft, charBotRight, Color.red, 2f);
            //Debug.DrawLine(charBotLeft, charTopLeft, Color.red, 2f);
            //Debug.DrawLine(charBotRight, charTopRight, Color.red, 2f);
            //Debug.DrawLine(charTopLeft, charTopRight, Color.red, 2f);
            return true;
        }
        else
        {
            //Debug.DrawLine(charBotLeft, charBotRight, Color.green, 1f);
            //Debug.DrawLine(charBotLeft, charTopLeft, Color.green, 1f);
            //Debug.DrawLine(charBotRight, charTopRight, Color.green, 1f);
            //Debug.DrawLine(charTopLeft, charTopRight, Color.green, 1f);
        }
        
        return false;
    }

    public bool Collides(Vector2 direction, float distance)
    {
        LayerMask layerMask = barrierMask;

        charBotLeft = new Vector2(collider.bounds.min.x, collider.bounds.min.y);
        charBotRight = new Vector2(collider.bounds.max.x, collider.bounds.min.y);
        charTopLeft = new Vector2(collider.bounds.min.x, collider.bounds.max.y);
        charTopRight = new Vector2(collider.bounds.max.x, collider.bounds.max.y);

        RaycastHit2D hitBotLeft;
        Ray rayBotLeft = new Ray(charBotLeft, direction);
        RaycastHit2D hitBotRight;
        Ray rayBotRight = new Ray(charBotRight, direction);
        RaycastHit2D hitTopLeft;
        Ray rayTopLeft = new Ray(charTopLeft, direction);
        RaycastHit2D hitTopRight;
        Ray rayTopRight = new Ray(charTopRight, direction);


        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, direction, distance, layerMask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, direction, distance, layerMask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, direction, distance, layerMask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, direction, distance, layerMask);

        //Debug.DrawRay(currentTargetDestination, left, Color.red);
        //Debug.DrawRay(currentTargetDestination, up, Color.blue);

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
        return Collides(left, distance) || Collides(up, distance) || Collides(right, distance) || Collides(down, distance);
    }

    //old
    public bool Collides(Vector2 center, float extens, float width, LayerMask layermask, Vector2 direction)
    {
        charBotLeft = new Vector2(center.x - extens + width, center.y - extens + width);
        charBotRight = new Vector2(center.x + extens - width, center.y - extens + width);
        charTopLeft = new Vector2(center.x - extens + width, center.y + extens - width);
        charTopRight = new Vector2(center.x + extens - width, center.y + extens - width);

        RaycastHit2D hitBotLeft;
        Ray rayBotLeft = new Ray(charBotLeft, direction);
        RaycastHit2D hitBotRight;
        Ray rayBotRight = new Ray(charBotRight, direction);
        RaycastHit2D hitTopLeft;
        Ray rayTopLeft = new Ray(charTopLeft, direction);
        RaycastHit2D hitTopRight;
        Ray rayTopRight = new Ray(charTopRight, direction);


        hitBotLeft = Physics2D.Raycast(rayBotLeft.origin, direction, width, layermask);
        hitBotRight = Physics2D.Raycast(rayBotRight.origin, direction, width, layermask);
        hitTopLeft = Physics2D.Raycast(rayTopLeft.origin, direction, width, layermask);
        hitTopRight = Physics2D.Raycast(rayTopRight.origin, direction, width, layermask);

        //Debug.DrawRay(currentTargetDestination, left, Color.red);
        //Debug.DrawRay(currentTargetDestination, up, Color.blue);

       // Debug.DrawRay(botLeft, direction, Color.cyan);
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
        charBotLeft = new Vector2(center.x - extens + width, center.y - extens + width);
        charBotRight = new Vector2(center.x + extens - width, center.y - extens + width);
        charTopLeft = new Vector2(center.x - extens + width, center.y + extens - width);
        charTopRight = new Vector2(center.x + extens - width, center.y + extens - width);

        RaycastHit2D hitBotLeft;
        Ray rayBotLeft = new Ray(charBotLeft, direction);
        RaycastHit2D hitBotRight;
        Ray rayBotRight = new Ray(charBotRight, direction);
        RaycastHit2D hitTopLeft;
        Ray rayTopLeft = new Ray(charTopLeft, direction);
        RaycastHit2D hitTopRight;
        Ray rayTopRight = new Ray(charTopRight, direction);

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

    

    /// <summary>
    /// bacha, dělá to skoky po 1, přitom charWidth = 0.5..uvidí se, jak to pofachá
    /// </summary>
    /// <param name="node"></param>
    /// <param name="visited"></param>
    /// <returns></returns>
    public List<Vector2> GetPossibleDirections(Vector2 node, List<Vector2> visited)
    {
        List<Vector2> possibleDirections = new List<Vector2>();

        if (!CharacterCollides(node + left) && !visited.Contains(node + left))
        {
            Debug.Log("L");
            possibleDirections.Add(node + left);
        }
        if (!CharacterCollides(node + up) && !visited.Contains(node + up))
        {
            Debug.Log("U");
            possibleDirections.Add(node + up);
        }
        if (!CharacterCollides(node + right) && !visited.Contains(node + right))
        {
            Debug.Log("R");
            possibleDirections.Add(node + right);
        }
        if (!CharacterCollides(node + down) && !visited.Contains(node + down))
        {
            Debug.Log("D");
            possibleDirections.Add(node + down);
        }
        //Debug.Log("found directions:");
        //Debug.Log(PrintNodeList(possibleDirections));

        return possibleDirections;
    }

    public string PrintNodeList(List<Vector2> list)
    {
        string result = "";
        foreach (Vector2 node in list)
        {
            result += "[" + node.x + "," + node.y + "],";
        }
        return result;
    }

    private Vector2 lastDirection;

    public bool FindPath()
    {
        //Vector2 currentPosition = transform.position;

        if (Collides(up))
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

            else if (lastDirection != up)
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

        if (ValueEquals(gameObject.transform.position.x, x)) {  }
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
        if (ValueEquals(gameObject.transform.position.x, x)) {  }
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
        UpdateAnimatorState(AnimatorStateEnum.walkUp);
    }
    public void MoveLeft()
    {
        rb2d.velocity = Vector2.left * speed;
        direction = left;
        UpdateAnimatorState(AnimatorStateEnum.walkLeft);
    }
    public void MoveDown()
    {
        rb2d.velocity = Vector2.down * speed;
        direction = down;
        UpdateAnimatorState(AnimatorStateEnum.walkDown);
    }
    public void MoveRight()
    {
        rb2d.velocity = Vector2.right * speed;
        direction = right;
        UpdateAnimatorState(AnimatorStateEnum.walkRight);
    }
    public void Stop()
    {
        rb2d.velocity = Vector2.zero;
        UpdateAnimatorState(AnimatorStateEnum.stop);
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
