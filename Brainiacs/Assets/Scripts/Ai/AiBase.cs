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
    
    public int frameCountSinceLvlLoad;
    
    //////////////////////////////MAP shit
    public GameObject[] barriers;

    /// //////////////////////////////////// MASKS /////////////////////////////
    //detect only collision with barriers and borders (assigned manually to prefab)
    public LayerMask barrierMask;
    public LayerMask bulletMask; //assigned manualy, but..,  mask = 1 << 12;
    public LayerMask itemMask;

    /// //////////////////////////////////// CHARACTER COORDINATES /////////////////////////////
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
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        barriers = GameObject.FindGameObjectsWithTag("Barrier");
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
        frameCountSinceLvlLoad++;

        if (frameCountSinceLvlLoad == playerNumber)
        {
            SetPlayers();
            weaponHandling.activeWeapon = weaponHandling.inventory[0];
            RandomizeDirection();
            RefreshAnimatorState();
            weaponHandling.weaponRenderer.sprite = 
                weaponHandling.activeWeapon.weaponSprites[weaponHandling.player.directionMapping[weaponHandling.player.direction]];            
        }

        //////////CHEK ONCE PER SECOND
        if (Time.frameCount % 30 == 6)
        {
            aiPriorityLogic.UpdatePriorities();
            
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
                    break;
            }
            
            //aiPriorityLogic.PrintPriorities();
            aiActionLogic.UpdateCurrentAction();
        }
        
        aiActionLogic.DoCurrentAction();
        UpdateDirection();
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
                    break;
            }
        }
    }
    
}
