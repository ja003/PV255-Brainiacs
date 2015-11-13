using System;
using System.Collections.Generic;
using UnityEngine;
using Brainiacs.Generate;

public abstract class PlayerBase : MonoBehaviour
{
    //public CharacterEnum character;

    public int playerNumber { get; set; }

    // JP - farba spritu
    //   public string color { get; set; }

    public float posX;
    public float posY;

  //  public string playerName { get; set; }

    public float speed { get; set; }

    //rozměry mapy
    public float mapMinX;
    public float mapMinY;
    public float mapMaxX;
    public float mapMaxY;

    public float characterWidth = 1f;

    //////////////////////ANIMATOR VARIABLES/////////////////////////
    public Animator characterAnimator;
    public bool walkUp = false;
    public bool walkRight = false;
    public bool walkDown = true;
    public bool walkLeft = false;
    public bool dead = false;

    // //////////////////////////////////////// HP /////////////////////////////////////////////
    //<<MG.. added
    private static int maxHP = 100;
    public int hitPoints = maxHP;
    private bool isShielded = false;
    
    // ///////////////////////////////////// POWER UPS ///////////////////////////////////////////
    private bool speedBuffIsActive = false;
    private bool slowDebuffIsActive = false;
    private float time = 0.0f;                  //for speed/slow
    private float speedAmount;                  //to remember if it is boost of slow
    private float speedBuffDuration = 10.0f;
    //..MG>>
    
    // /////////////////////////////////////// Movement ///////////////////////////////////////////
    public Vector2 direction;
    protected Vector2 up = Vector2.up;
    protected Vector2 down = Vector2.down;
    protected Vector2 left = Vector2.left;
    protected Vector2 right = Vector2.right;
    protected Vector2 stop = Vector2.zero;
    // ///////////////////////////////////////////////////////////////////////////////////////////

    /// //////////////////////////////////// WEAPON HANDLING ///////////////////////////////////
    public WeaponHandling weaponHandling;

    public List<WeaponBase> inventory { get; set; }
    public WeaponBase activeWeapon { get; set; }
    public Dictionary<Vector2, int> directionMapping = new Dictionary<Vector2, int>();

    /// //////////////////////////////////////// END ///////////////////////////////////////////


    // //////////////////////////////////////  Components ///////////////////////////////////////
    //rigid body of controlled object
    public Rigidbody2D rb2d { get; set; }
    Components comp;
    /////////////////////////////////////////////////////////////////////////////////////////////
    PlayerInfo playInfo;

    public void setUpPB(Components c, PlayerInfo p)
    {
        comp = c;
        playInfo = p;
        weaponHandling = GetComponent<WeaponHandling>();
        
        setUpSprites();
        weaponHandling.buletManager = transform.parent.GetComponent<BulletManager>();
        weaponHandling.buletManager.createBullets();

        characterAnimator = GetComponent<Animator>();

        //pičovina, pak to napojím na PLayerInfo a atribut playerNumber uplně smažu
        playerNumber = p.playerNumber;

        mapMinX = -4.75f;
        mapMinY = -4.75f;
        mapMaxX = 8.6f;
        mapMaxY = 4f;

        directionMapping.Add(up, 3);
        directionMapping.Add(down, 0);
        directionMapping.Add(left, 1);
        directionMapping.Add(right, 2);
    }

    private void setUpSprites() {
        string sprite = "";
        sprite += playInfo.charEnum.ToString();
        sprite += playInfo.playerColor;

        comp.spriteRend.sprite = Resources.Load<Sprite>("Sprites/Players/" + sprite);
        Debug.Log("setting sprite: " + sprite);
    }

    //TODO: transform.GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Hand_back";
    //až přídáme ruku
    /// <summary>
    /// změní pořádí vykreslování částí těla
    /// left,down,right: player-weapon-hand "weapon_front" + "hand_front"
    /// up: hand-weapon-player "weapon_back" + "hand_back"
    /// </summary>
    public void SortLayers()
    {
        if (direction == up)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Weapon_back";
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Weapon_front";
        }
    }

    void FixedUpdate()
    {
        SpeedBuffChecker();
        UpdatePosition();
    }



    public void UpdateAnimatorState(AnimatorStateEnum state)
    {
        switch (state)
        {
            case AnimatorStateEnum.walkUp:
                walkUp = true;
                walkRight = false;
                walkDown = false;
                walkLeft= false;
                break;
            case AnimatorStateEnum.walkRight:
                walkUp = false;
                walkRight = true;
                walkDown = false;
                walkLeft = false;
                break;
            case AnimatorStateEnum.walkDown:
                walkUp = false;
                walkRight = false;
                walkDown = true;
                walkLeft = false;
                break;
            case AnimatorStateEnum.walkLeft:
                walkUp = false;
                walkRight = false;
                walkDown = false;
                walkLeft = true;
                break;
            case AnimatorStateEnum.stop:
                walkUp = false;
                walkRight = false;
                walkDown = false;
                walkLeft = false;
                break;
            case AnimatorStateEnum.dead:
                walkUp = false;
                walkRight = false;
                walkDown = false;
                walkLeft = false;
                dead = true;
                break;
        }
        characterAnimator.SetBool("walkUp", walkUp);
        characterAnimator.SetBool("walkDown", walkDown);
        characterAnimator.SetBool("walkRight", walkRight);
        characterAnimator.SetBool("walkLeft", walkLeft);
        characterAnimator.SetBool("dead", dead);

        /*Debug.Log(
            "walkUp:" + walkUp +
            ",walkDown:" + walkDown +
            ",walkRight:" + walkDown +
            ",walkLeft:" + walkLeft +
            ",dead:" + dead
            );
            */

    }

    //zatím pouze doleva a doprava
    public void UpdateDirection()
    {
        //Debug.Log("[" + playerNumber + "]:" + direction);
        /*
        if (direction == down)
        {
            weaponHandling.weaponRenderer.sprite = weaponHandling.weaponSprites[0];
        }
        else if (direction == left)
        {
            weaponHandling.weaponRenderer.sprite = weaponHandling.weaponSprites[1];

        }
        else if (direction == right)
        {
            weaponHandling.weaponRenderer.sprite = weaponHandling.weaponSprites[2];

        }
        else if (direction == up)
        {
            weaponHandling.weaponRenderer.sprite = weaponHandling.weaponSprites[3];

        }*/
        
    }

    public void UpdatePosition()
    {
        posX = gameObject.transform.position.x;
        posY = gameObject.transform.position.y;
    }

    //PowerUp and HP management - <<<MG...>>>

    //player receives damage
    public void ApplyDamage(int dmg)
    {
        if (isShielded)
        {
            isShielded = false;
        }
        else
        {
            if ((hitPoints - dmg) <= 0) 
            {
                UpdateAnimatorState(AnimatorStateEnum.dead);
                hitPoints = 0;
                //TODO sputenie animacie smrti
                comp.rb2d.velocity = stop;
                //TODO delay nejake 2s

                //presun na novu poziciu
                Vector3 newRandomPosition = PositionGenerator.GenerateRandomPosition();
                posX = newRandomPosition.x;
                posY = newRandomPosition.y;
                transform.position = newRandomPosition;
                Debug.Log("X " + newRandomPosition.x);
                Debug.Log("Y " + newRandomPosition.y);
                hitPoints = maxHP;
                
                dead = false;
            }
            else
            {
                hitPoints -= dmg;
            }
        }
        //Debug.Log(hitPoints);
    }

    /// ////////////////////////////////////// POWER UPS ///////////////////////////////////////////
    //player receives heal
    private void ApplyHeal(int heal)
    {
        if ((hitPoints + heal) > maxHP)
        {
            hitPoints = maxHP;
        }
        else
        {
            hitPoints += heal;
        }

    }

    //checks if speed/slow buff duration has expired
    private void SpeedBuffChecker()
    {
        if (speedBuffIsActive || slowDebuffIsActive)
        {
            time += Time.deltaTime;
            if (time > speedBuffDuration && speedBuffIsActive)
            {
                speedBuffIsActive = false;
                time = 0.0f;
                speed -= speedAmount;
            }
            if (time > speedBuffDuration && slowDebuffIsActive)
            {
                slowDebuffIsActive = false;
                time = 0.0f;
                speed -= speedAmount;
            }
        }
    }

    //receives speed buff
    private void ApplySpeed(float amount)
    {
        if (!speedBuffIsActive)
        {
            if (slowDebuffIsActive)
            {
                slowDebuffIsActive = false;
                speed -= speedAmount;
                return;
            }
            speedAmount = amount;
            speedBuffIsActive = true;
            speed += speedAmount;
        }
        else
        {
            time = 0.0f;
        }
    }

    private void ApplySlow(float amount)
    {
        if (!slowDebuffIsActive)
        {
            if (speedBuffIsActive)
            {
                speedBuffIsActive = false;
                speed -= speedAmount;
                return;
            }
            speedAmount = amount;
            slowDebuffIsActive = true;
            speed += speedAmount;
        }
        else
        {
            time = 0.0f;
        }
    }

    //this way player receives info from collision with power up
    public void AddPowerUp(PowerUpEnum en)
    {
        switch (en)
        {
            case PowerUpEnum.Shield:
                isShielded = true;
                //Debug.Log("player picked up shield");
                break;
            case PowerUpEnum.Heal:
                ApplyHeal(maxHP / 2);
                //Debug.Log("player picked up heal");
                break;
            case PowerUpEnum.Ammo:
                activeWeapon.reload();
                //Debug.Log("player picked up ammo");
                break;
            case PowerUpEnum.Speed:
                ApplySpeed(1.5f);
                //Debug.Log("player picked up speed");
                break;
            case PowerUpEnum.Mystery:
                System.Random rnd = new System.Random();
                var v = Enum.GetValues(typeof(PowerUpEnum));
                //Debug.Log("player picked up mystery");
                AddPowerUp((PowerUpEnum)v.GetValue(rnd.Next(v.Length)));
                break;
            case PowerUpEnum.dealDamage:
                ApplyDamage(maxHP / 3);
                //Debug.Log("player picked up dealDamage");
                break;
            case PowerUpEnum.slowSpeed:
                ApplySlow(-1.0f);
                //Debug.Log("Player picked up slowSPeed");
                break;
            default:
                Debug.Log("ERROR: Unknown powerUp received.");
                break;
        }
    }
    //<<<...MG>>>

    
    
}

//namespace Assets.Scripts.Player
//{
//    public enum AnimatorStateEnum
//    {
//    }
//}

//proč mi to v Enums nefunguje?
public enum AnimatorStateEnum
{
    walkUp,
    walkDown,
    walkLeft,
    walkRight,
    stop,
    dead
}