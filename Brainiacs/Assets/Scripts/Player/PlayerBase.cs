using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public float mapWidth;
    public float mapHeight;

    public float characterWidth = 1f;

    public PositionGenerator posGenerator;


    //////////////////////STATS/////////////////////////
    public int lifes;
    public int score;
    public GameInfo gameInfo;
    public GameManager gameManager;


    //////////////////////AUDIO SOURCE/////////////////////////
    public AudioClip deathSound_01;

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

    public int GetMaxHp()
    {
        return maxHP;
    }
    
    // ///////////////////////////////////// POWER UPS ///////////////////////////////////////////
    private bool speedBuffIsActive = false;
    private bool slowDebuffIsActive = false;
    private float time = 0.0f;                  //for speed/slow
    private float speedAmount;                  //to remember if it is boost of slow
    private float speedBuffDuration = 10.0f;
    //..MG>>
    
    // /////////////////////////////////////// Movement ///////////////////////////////////////////
    public Vector2 direction;
    public Vector2 up = Vector2.up;
    public Vector2 down = Vector2.down;
    public Vector2 left = Vector2.left;
    public Vector2 right = Vector2.right;
    public Vector2 stop = Vector2.zero;
    // ///////////////////////////////////////////////////////////////////////////////////////////

    /// //////////////////////////////////// WEAPON HANDLING ///////////////////////////////////
    public WeaponHandling weaponHandling;
    public List<WeaponBase> inventory { get; set; }
    public WeaponBase activeWeapon { get; set; }
    public Dictionary<Vector2, int> directionMapping = new Dictionary<Vector2, int>();

    /////////////////////////////////////////////HANDS//////////////////////////////////////
    public SpriteRenderer handsRenderer;
    public Sprite[] handsSprites;

    /// //////////////////////////////////////// END ///////////////////////////////////////////


    // //////////////////////////////////////  Components ///////////////////////////////////////
    //rigid body of controlled object
    public Rigidbody2D rb2d { get; set; }
    public Components comp;
    /////////////////////////////////////////////////////////////////////////////////////////////
    public PlayerInfo playInfo;

    public void setUpPB(Components c, PlayerInfo p)
    {
        lifes = p.lifes;
        score = 0;

        comp = c;
        playInfo = p;
        weaponHandling = GetComponent<WeaponHandling>();
        
        setUpSprites();

        setUpSounds();

        weaponHandling.buletManager = transform.parent.GetComponent<BulletManager>();
        weaponHandling.buletManager.createBullets(this);

        characterAnimator = GetComponent<Animator>();

        //pičovina, pak to napojím na PLayerInfo a atribut playerNumber uplně smažu
        playerNumber = p.playerNumber;

        mapMinX = -4.75f;
        mapMinY = -4.75f;
        mapMaxX = 8.6f;
        mapMaxY = 4f;
        mapHeight = Math.Abs(mapMaxY - mapMinY);
        mapWidth = Math.Abs(mapMaxX - mapMinX);

        directionMapping.Add(up, 3);
        directionMapping.Add(down, 0);
        directionMapping.Add(left, 1);
        directionMapping.Add(right, 2);

        handsRenderer = transform.Find("hands_down").GetComponent<SpriteRenderer>(); //hands_down, protože sem zrovna na prefab přetáhl hands_down a nejde to změnit..normálně
        handsSprites = Resources.LoadAll<Sprite>("Sprites/Hands");

        weaponHandling.weaponRenderer = transform.Find("weapon").GetComponent<SpriteRenderer>();

        //setup position generator
        posGenerator = GameObject.Find("PositionGenerator").GetComponent<PositionGenerator>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void setUpSounds()
    {
        //string soundLoaderString = "Sounds/Player/" + playInfo.charEnum.ToString().ToLower() + "/";
        string soundLoaderString = "Sounds/Player/" + "davinci" + "/"; //for now

        deathSound_01 = Resources.Load(soundLoaderString + "death_01") as AudioClip;

        //Debug.Log(soundLoaderString + "death_01");

        //Debug.Log("loaded sound: " + deathSound_01.name);
    }

    public void setUpWeapons(PlayerInfo pi)
    {
        
        weaponHandling.player = GetComponent<PlayerBase>();
        weaponHandling.specialWeapon = GetComponent<WeaponSpecial>();
        
        //Debug.Log(pi);
        //Debug.Log(pi.charEnum);
        WeaponBase pistol = new WeaponPistol(pi.charEnum);
        WeaponBase sniper = new WeaponSniper();
        WeaponBase biogun = new WeaponBiogun();
        //WeaponBase flameTh = new WeaponFlamethrower();
        WeaponBase MP40 = new WeaponMP40();
        WeaponBase mine = new WeaponMine();
        WeaponBase flame = new WeaponFlamethrower();
       Transform childFlame = transform.Find("Flame");
        weaponHandling.flamethrower = childFlame.GetComponent<WeaponFlamethrowerLogic>();

        //WeaponBase specialCurie = new WeaponSpecialCurieLogic();
        // Tu sa vytvoria vsetky zbrane ktore sa priradia do weapon handling aby sa nemusel volat zbytocne load na sprajtoch
        //weaponHandling.weapons.Add(WeaponEnum.specialCurie, special);
        WeaponBase special = null;

        switch (pi.charEnum)
        {
            case CharacterEnum.Tesla:
                special = new WeaponCurieSpecial();
                weaponHandling.specialWeapon.SetUp(pi, transform.parent.GetComponent<BulletManager>(), this, special);
                weaponHandling.weapons.Add(WeaponEnum.specialCurie, special);
                break;
            case CharacterEnum.Curie:
                special = new WeaponCurieSpecial();
                weaponHandling.specialWeapon.SetUp(pi, transform.parent.GetComponent<BulletManager>(), this, special);
                weaponHandling.weapons.Add(WeaponEnum.specialCurie, special);
                break;
            case CharacterEnum.DaVinci:
                special = new WeaponDaVinciSpecial();
                weaponHandling.specialWeapon.SetUp(pi, transform.parent.GetComponent<BulletManager>(), this, special);
                weaponHandling.weapons.Add(WeaponEnum.specialDaVinci, special);
                break;
            case CharacterEnum.Einstein:
                special = new WeaponEinsteinSpecial();
                weaponHandling.specialWeapon.SetUp(pi, transform.parent.GetComponent<BulletManager>(), this, special);
                weaponHandling.weapons.Add(WeaponEnum.specialEinstein, special);
                break;
            case CharacterEnum.Nobel:
                special = new WeaponNobelSpecial();
                weaponHandling.specialWeapon.SetUp(pi, transform.parent.GetComponent<BulletManager>(), this, special);
                weaponHandling.weapons.Add(WeaponEnum.specialNobel, special);
                break;
        }
        
        weaponHandling.weapons.Add(WeaponEnum.sniper, sniper);
        weaponHandling.weapons.Add(WeaponEnum.pistol, pistol);
        weaponHandling.weapons.Add(WeaponEnum.biogun, biogun);
        weaponHandling.weapons.Add(WeaponEnum.MP40, MP40);
        weaponHandling.weapons.Add(WeaponEnum.mine, mine);
        weaponHandling.weapons.Add(WeaponEnum.flamethrower, flame);


        // Inicializacia prvej zbrane
        //weaponHandling.inventory.Add(specialE);
        //weaponHandling.inventory.Add(flame);
        // weaponHandling.inventory.Add(special);
        weaponHandling.inventory.Add(pistol);
        //weaponHandling.inventory.Add(sniper);
        //weaponHandling.inventory.Add(biogun);
        //weaponHandling.inventory.Add(MP40);
        weaponHandling.inventory.Add(mine);

        weaponHandling.activeWeapon = weaponHandling.inventory[0];
        

        //Debug.Log("rend:" + weaponHandling.weaponRenderer);

    }

    private void setUpSprites() {
        string animControllerString = "";
        string character = playInfo.charEnum.ToString().ToLower();

        //Debug.Log(character);
        animControllerString +=  character + "_";
        animControllerString += playInfo.playerColor + "_";
        animControllerString += "override";
        animControllerString = "Animations/Characters/" + character + "/" + animControllerString;
        //Debug.Log(animControllerString);

        RuntimeAnimatorController rtac = Resources.Load(animControllerString) as AnimatorOverrideController;
        //Debug.Log(animControllerString);
        //Debug.Log(rtac);
        comp.animator.runtimeAnimatorController = rtac;

        
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
            //Debug.Log(playerNumber + ":" + weaponHandling.weaponRenderer.sprite);
            weaponHandling.weaponRenderer.sortingLayerName = "Weapon_back";
            handsRenderer.sortingLayerName = "Hand_back";

            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Weapon_back";
        }
        else
        {
            weaponHandling.weaponRenderer.sortingLayerName = "Weapon_front";
            handsRenderer.sortingLayerName = "Hand_front";

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
    

    /// <summary>
    /// momenátlně pouze na ruce + sortění LAYERS
    /// buď někam přesunout, nebo oatřičně přejmenovat
    /// </summary>
    public void UpdateDirection()
    {
        //Debug.Log("[" + playerNumber + "]:" + direction);

        
        if (direction == down)
        {
            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weaponHandling.activeWeapon.weaponSprites[0];
            //weaponHandling.weaponRenderer.sprite = weaponHandling.activeWeapon.weaponSprites[0];
            handsRenderer.sprite = handsSprites[0];
        }
        else if (direction == left)
        {
            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weaponHandling.activeWeapon.weaponSprites[1];
            //weaponHandling.weaponRenderer.sprite = weaponHandling.activeWeapon.weaponSprites[1];
            handsRenderer.sprite = handsSprites[1];
        }
        else if (direction == right)
        {
            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weaponHandling.activeWeapon.weaponSprites[2];
            //weaponHandling.weaponRenderer.sprite = weaponHandling.activeWeapon.weaponSprites[2];
            handsRenderer.sprite = handsSprites[2];
        }
        else if (direction == up)
        {
            //transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weaponHandling.activeWeapon.weaponSprites[3];
            //weaponHandling.weaponRenderer.sprite = weaponHandling.activeWeapon.weaponSprites[3];
            handsRenderer.sprite = handsSprites[3];
        }
        

        SortLayers();

    }

    public void UpdatePosition()
    {
        posX = gameObject.transform.position.x;
        posY = gameObject.transform.position.y;
    }

    public void UpdateGameInfoDeaths(int playerNumber, int value)
    {
        switch (playerNumber)
        {
            case 1:
                gameInfo.player1deaths += value;
                break;
            case 2:
                gameInfo.player2deaths += value;
                break;
            case 3:
                gameInfo.player3deaths += value;
                break;
            case 4:
                gameInfo.player4deaths += value;
                break;
            default:
                Debug.Log("playerNumber not valid");
                break;
        }
    }

    public void UpdateGameInfoScore(int playerNumber, int value)
    {
        switch (playerNumber)
        {
            case 1:
                gameInfo.player1score += value;
                break;
            case 2:
                gameInfo.player2score += value;
                break;
            case 3:
                gameInfo.player3score += value;
                break;
            case 4:
                gameInfo.player4score += value;
                break;
            default:
                Debug.Log("playerNumber not valid");
                break;
        }
    }

    //PowerUp and HP management - <<<MG...>>>

    //player receives damage
    public void ApplyDamage(int dmg, PlayerBase owner)
    {
        if (isShielded)
        {
            isShielded = false;
        }
        else
        {
            if ((hitPoints - dmg) <= 0 && !dead) 
            {
                //UpdateAnimatorState(AnimatorStateEnum.dead);
                hitPoints = 0;
                //TODO sputenie animacie smrti
                comp.rb2d.velocity = stop;
                UpdateGameInfoDeaths(playerNumber,1);

                //add score to owner of bullet
                owner.score++;
                UpdateGameInfoScore(owner.playerNumber, 1);

                Debug.Log(owner.playerNumber + ": " + owner.score);
                //check score
                if(gameInfo.gameMode == GameModeEnum.Score && owner.score >= gameInfo.winScore)
                {
                    gameManager.EndGame();
                }
                                
                if(!dead)
                    StartCoroutine(Die());
                
            }
            else
            {
                hitPoints -= dmg;
            }
        }
        //Debug.Log(hitPoints);
    }

    IEnumerator Die()
    {
        dead = true;
        Debug.Log("dead");

        //play death sound
        SoundManager.instance.RandomizeSfx(deathSound_01);

        UpdateAnimatorState(AnimatorStateEnum.dead);
        //disable movement
        up = stop;
        right = stop;
        down= stop;
        left = stop;
        //disable weapon
        //....

        float deadTime = characterAnimator.GetCurrentAnimatorStateInfo(0).length;
        
        yield return new WaitForSeconds(deadTime + 0.1f);
        lifes--;

        transform.position = new Vector2(-666, 666);//move to hell

        if (gameInfo.gameMode == GameModeEnum.Deathmatch && lifes <= 0)
        {
            Debug.Log("you dead");
            gameManager.CheckLifes();
        }
        else
        {
            yield return new WaitForSeconds(1f);

            hitPoints = maxHP;
            dead = false;
            Debug.Log("alive");
            //UpdateAnimatorState(AnimatorStateEnum.walkRight);

            //TODO
            //load player again (without old weapons,...)

            //Vector3 newRandomPosition = Vector3.zero;//= generator.GenerateRandomPosition();
            Vector3 newRandomPosition = posGenerator.GenerateRandomPosition(mapHeight, mapWidth);
            posX = newRandomPosition.x;
            posY = newRandomPosition.y;
            transform.position = newRandomPosition;
            //Debug.Log("X " + newRandomPosition.x);
            //Debug.Log("Y " + newRandomPosition.y);

            //enable movement
            up = Vector2.up;
            down = Vector2.down;
            left = Vector2.left;
            right = Vector2.right;

            //update weapon direction and stop player animation(AI reasons)
            UpdateAnimatorState(AnimatorStateEnum.stop);
            //weaponHandling.weaponRenderer.sprite = weaponHandling.activeWeapon.weaponSprites[weaponHandling.player.directionMapping[direction]];
            //TODO - ^ udělat metodu
        }
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
                weaponHandling.activeWeapon.reload();
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
                ApplyDamage(maxHP / 3, this); //hit yourself
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