using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiBase : PlayerBase
{

    public PlayerBase player1;
    public PlayerBase player2;
    public PlayerBase player3;
    public PlayerBase player4;

    public Components comp;
    public PlayerInfo playInfo;







    //////////////////////////////MAP shit
    public GameObject[] barriers;

    /// //////////////////////////////////// MASKS /////////////////////////////
    //detect only collision with barriers and borders (assigned manually to prefab)
    public LayerMask barrierMask;
    public LayerMask bulletMask;
    public LayerMask itemMask;



    /// //////////////////////////////////// CHARACTER COORDINATES /////////////////////////////
    // Collider2D collider;

    public float characterColliderWidth;
    public float characterColliderHeight;

    

    /// //////////////////////////////////// LOGICS /////////////////////////////
    public AiPowerUpLogic aiPowerUpLogic;
    public AiWeaponLogic aiWeaponLogic;
    public AiMovementLogic aiMovementLogic;
    public AiAvoidBulletLogic aiAvoidBulletLogic;
    public AiPriorityLogic aiPriorityLogic;
    public AiActionLogic aiActionLogic;

    public void setUpAiB(Components c, PlayerInfo p)
    {
        comp = c;
        playInfo = p;
        //weaponHandling = GetComponent<WeaponHandling>();

        rb2d = gameObject.GetComponent<Rigidbody2D>();

        barriers = GameObject.FindGameObjectsWithTag("Barrier");



        //Debug.Log(barriers[0].name);
        //Debug.Log(barriers[1].name);
        //Debug.Log(barriers[2].name);
        //Debug.Log(barriers[3].name);


        characterColliderHeight = gameObject.GetComponent<BoxCollider2D>().size.y;
        characterColliderWidth = gameObject.GetComponent<BoxCollider2D>().size.x;

        aiPowerUpLogic = new AiPowerUpLogic(this);
        aiWeaponLogic = new AiWeaponLogic(this);
        aiMovementLogic = new AiMovementLogic(this);
        aiAvoidBulletLogic = new AiAvoidBulletLogic(this);
        aiPriorityLogic = new AiPriorityLogic(this);
        aiActionLogic = new AiActionLogic(this);

        AssingLogic();

    }

    public void AssingLogic()
    {
        aiPowerUpLogic.aiMovementLogic = aiMovementLogic;

        aiPriorityLogic.aiPowerUpLogic = aiPowerUpLogic;
        aiPriorityLogic.aiWeaponLogic = aiWeaponLogic;
        aiPriorityLogic.aiActionLogic = aiActionLogic;

        aiWeaponLogic.aiPriorityLogic = aiPriorityLogic;

        aiAvoidBulletLogic.aiPriorityLogic = aiPriorityLogic;
        aiAvoidBulletLogic.aiActionLogic = aiActionLogic;

        aiActionLogic.aiMovementLogic = aiMovementLogic;
        aiActionLogic.aiPriorityLogic = aiPriorityLogic;
        aiActionLogic.aiPowerUpLogic = aiPowerUpLogic;
        aiActionLogic.aiWeaponLogic = aiWeaponLogic;
        aiActionLogic.aiAvoidBulletLogic = aiAvoidBulletLogic;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.frameCount == 5)
        {
            SetPlayers();
        }

        //////////CHEK EVERY FRAME




        //////////CHEK ONCE PER SECOND
        if (Time.frameCount % 30 == 6)
        {
            aiPriorityLogic.UpdatePriorities();
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
                    aiPriorityLogic.killPlayer1Priority = 0;
                    break;
                case 2:
                    aiPriorityLogic.killPlayer2Priority = 0;
                    break;
                case 3:
                    aiPriorityLogic.killPlayer3Priority = 0;
                    break;
                case 4:
                    aiPriorityLogic.killPlayer4Priority = 0;
                    break;
                default:
                    Debug.Log(gameObject.ToString() + " has no player number");
                    break;
            }

            //Debug.Log("!");
            //aiPriorityLogic.PrintPriorities();
            //UpdateCurrentAction(); //has to be checked faster
            aiActionLogic.UpdateCurrentAction();




        }

        //////////CHECK 4 PER SECOND
        if (Time.frameCount % 8 == 0)
        {

        }

        if (Time.frameCount % 200 == 0)
        {
            //aiPriorityLogic.PrintPriorities();
            //aiActionLogic.PrintAction();
        }

        //Debug.Log("currentAction:"+currentAction);

        //CheckAmmo(); //check when firing

        //UpdateCurrentAction(); //lagz
        aiActionLogic.DoCurrentAction();
        UpdateDirection();

        //PrintPriorities();
        ///PrintAction();


    }



    
    /*
    public bool CanShoot(Vector2 center, Vector2 direction)
    {

        Ray rayGun = new Ray(center, direction);

        float mapLenght = 15;
        RaycastHit2D[] hitGun = Physics2D.RaycastAll(rayGun.origin, direction, mapLenght);

        Debug.DrawRay(rayGun.origin, direction, Color.cyan);
        Debug.DrawRay(rayGun.origin, direction * -1, Color.red);

        if (hitGun.Length != 0)
        {
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
    */

    public bool IsInPlayground(Vector2 point)
    {
        return mapMinX < point.x && point.x < mapMaxX && mapMinY < point.y && point.y < mapMaxY;
    }

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


        while (bestHorizontalDown.y > mapMinY && !aiMovementLogic.CharacterCollidesBarrier(bestHorizontalDown) && bestHorizontalDown.y > posY)
        {
            bestHorizontalDown.y -= 0.1f;
        }


        while (bestHorizontalUp.y < mapMaxY && !aiMovementLogic.CharacterCollidesBarrier(bestHorizontalUp) && bestHorizontalUp.y < posY)
        {
            bestHorizontalUp.y += 0.1f;
        }
        while (bestVerticalLeft.x > mapMinX && !aiMovementLogic.CharacterCollidesBarrier(bestVerticalLeft) && bestVerticalLeft.x > posX)
        {
            bestVerticalLeft.x -= 0.1f;
        }
        while (bestVerticalRight.x < mapMaxX && !aiMovementLogic.CharacterCollidesBarrier(bestVerticalRight) && bestVerticalRight.x < posX)
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
        //MoveTo(possibleShootSpots[spotIndex]);


        //if you are on same axis -> turn his direction and shoot

        //if (AlmostEqual(posX, player.posX, 0.1) || AlmostEqual(posY, player.posY, 0.1))
        if (aiMovementLogic.MoveTo(possibleShootSpots[spotIndex]))
        {
            //Debug.Log("i can shoot");
            //look at him (if you are not already looking at him)

            if (GetObjectDirection(player.gameObject) != direction)
                LookAt(player.gameObject);
            else
            {
                //UpdateAnimatorState(AnimatorStateEnum.stop);
            }

            if (aiWeaponLogic.CanShoot(transform.position, direction))
            {
                //Debug.Log("I can shoot from:" + transform.position + " to: " + direction);
                //CheckAmmo();
                weaponHandling.fire(direction);
            }
        }


    }

    public bool AlmostEqual(float pos1, float pos2, double e)
    {
        return pos2 - e < pos1 && pos1 < pos2 + e;
    }

    public Vector2 GetObjectDirection(GameObject obj)
    {
        double distanceX = Mathf.Abs(posX - obj.transform.position.x);
        double distanceY = Mathf.Abs(posY - obj.transform.position.y);

        //prefer more dominant axis
        if (distanceX > distanceY)
        {
            //it is on my left
            if (obj.transform.position.x < posX)
            {
                return left;
            }
            else
            {
                return right;
            }
        }
        else
        {
            //it is below me
            if (obj.transform.position.y < posY)
            {
                return down;
            }
            else
            {
                return up;
            }
        }
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
                UpdateAnimatorState(AnimatorStateEnum.walkLeft);
            }
            else
            {
                direction = right;
                UpdateAnimatorState(AnimatorStateEnum.walkRight);
            }
        }
        else
        {
            //it is below me
            if (obj.transform.position.y < posY)
            {
                direction = down;
                UpdateAnimatorState(AnimatorStateEnum.walkDown);
            }
            else
            {
                direction = up;
                UpdateAnimatorState(AnimatorStateEnum.walkUp);
            }
        }
        //UpdateAnimatorState(AnimatorStateEnum.stop);

    }

    
    public void LookAroundYourself()
    {
        aiAvoidBulletLogic.RegisterBullets();

        aiPowerUpLogic.RegisterPowerUps();

        aiWeaponLogic.RegisterWeapons();


    }

    ///ACTIONS
    /*
    public void PrintAction()
    {
        string message = "";
        message += "current action=" + currentAction;

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
            case AiActionEnum.avoidBullet:
                aiAvoidBulletLogic.AvoidBullet();
                break;
            case AiActionEnum.pickupPowerUp:
                aiPowerUpLogic.PickUp(aiPowerUpLogic.bestPowerUp);
                break;
            case AiActionEnum.pickupWeapon:
                aiPowerUpLogic.PickUp(aiWeaponLogic.bestWeapon);
                break;
            case AiActionEnum.stand:
                aiMovementLogic.Stop();
                break;
            default:
                aiMovementLogic.Stop();
                break;
        }
        //Debug.Log("processing action: " + currentAction);
    }

    public AiActionEnum currentAction;

    public void UpdateCurrentAction()
    {
        int highestPriority = aiPriorityLogic.GetCurrentHighestPriority();
        //Debug.Log("highest = " + highestPriority);

        if (aiPriorityLogic.killPlayer1Priority >= highestPriority)
        {
            currentAction = AiActionEnum.killPlayer1;
        }
        else if (aiPriorityLogic.killPlayer2Priority >= highestPriority)
        {
            currentAction = AiActionEnum.killPlayer2;
        }
        else if (aiPriorityLogic.killPlayer3Priority >= highestPriority)
        {
            currentAction = AiActionEnum.killPlayer3;
        }
        else if (aiPriorityLogic.killPlayer4Priority >= highestPriority)
        {
            currentAction = AiActionEnum.killPlayer4;
        }
        else if (aiPriorityLogic.avoidBulletPriority >= highestPriority)
        {
            currentAction = AiActionEnum.avoidBullet;
        }
        else if (aiPowerUpLogic.pickPowerUpPriority >= highestPriority)
        {
            currentAction = AiActionEnum.pickupPowerUp;
        }
        else if (aiWeaponLogic.pickWeaponPriority >= highestPriority)
        {
            currentAction = AiActionEnum.pickupWeapon;
        }
        else if (aiPriorityLogic.standPriority >= highestPriority)
        {
            currentAction = AiActionEnum.stand;
            //Debug.Log("STAND");
        }

        else
        {
            currentAction = AiActionEnum.stand;
        }

        //currentAction = AiActionEnum.stand;
    }

    */


    public float GetDistance(GameObject object1, GameObject object2)
    {
        return (object1.transform.position - object2.transform.position).sqrMagnitude;
    }

    public float GetDistance(Vector2 pos1, Vector2 pos2)
    {
        return (pos1 - pos2).sqrMagnitude;
    }

    public float GetDistanceFactor(float distance)
    {
        return (100 - distance) / 100;
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
            //Debug.Log(player);
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


    ////// ////////////////////MOVEMENT
}
