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
    public AiMapLogic aiMapLogic;
    public AiKillingLogic aiKillingLogic;

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

        InitialiseLogic();

        AssingLogic();

    }

    public void InitialiseLogic()
    {
        aiPowerUpLogic = new AiPowerUpLogic(this);
        aiWeaponLogic = new AiWeaponLogic(this);
        aiMovementLogic = new AiMovementLogic(this);
        aiAvoidBulletLogic = new AiAvoidBulletLogic(this);
        aiPriorityLogic = new AiPriorityLogic(this);
        aiActionLogic = new AiActionLogic(this);
        aiMapLogic = new AiMapLogic(this);
        aiKillingLogic = new AiKillingLogic(this);
    }

    public void AssingLogic()
    {
        aiPowerUpLogic.aiMovementLogic = aiMovementLogic;
        aiPowerUpLogic.aiMapLogic = aiMapLogic;

        aiPriorityLogic.aiPowerUpLogic = aiPowerUpLogic;
        aiPriorityLogic.aiWeaponLogic = aiWeaponLogic;
        aiPriorityLogic.aiActionLogic = aiActionLogic;
        aiPriorityLogic.aiMapLogic = aiMapLogic;

        aiWeaponLogic.aiPriorityLogic = aiPriorityLogic;
        aiWeaponLogic.aiMapLogic = aiMapLogic;

        aiAvoidBulletLogic.aiPriorityLogic = aiPriorityLogic;
        aiAvoidBulletLogic.aiActionLogic = aiActionLogic;
        aiAvoidBulletLogic.aiMapLogic = aiMapLogic;

        aiActionLogic.aiMovementLogic = aiMovementLogic;
        aiActionLogic.aiPriorityLogic = aiPriorityLogic;
        aiActionLogic.aiPowerUpLogic = aiPowerUpLogic;
        aiActionLogic.aiWeaponLogic = aiWeaponLogic;
        aiActionLogic.aiAvoidBulletLogic = aiAvoidBulletLogic;
        aiActionLogic.aiKillingLogic = aiKillingLogic;

        aiKillingLogic.aiMapLogic = aiMapLogic;
        aiKillingLogic.aiMovementLogic = aiMovementLogic;
        aiKillingLogic.aiWeaponLogic = aiWeaponLogic;

        aiMovementLogic.aiMapLogic = aiMapLogic;

        aiMapLogic.aiAvoidBulletLogic = aiAvoidBulletLogic;
        aiMapLogic.aiPowerUpLogic = aiPowerUpLogic;
        aiMapLogic.aiWeaponLogic= aiWeaponLogic;
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
