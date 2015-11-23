using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ai_03 : AiBase {

    Components comp = new Components();
    PlayerInfo playInfo = new PlayerInfo();

    void Start()
    {
        playerNumber = 3;
        base.killPlayer1Priority = 0;
        base.killPlayer2Priority = 0;
        base.killPlayer3Priority = 0;
        base.killPlayer4Priority = 0;


        
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        speed = 2f;

        currentAction = AiActionEnum.stand;

        //InitializeBullets();
    }

    public void SetUpPlayer(PlayerInfo pi)
    {

        base.playerNumber = 3;
        base.speed = 2f;
        base.direction = base.right;

        playInfo = pi;
        
        comp.spriteRend = GetComponent<SpriteRenderer>();
        comp.rb2d = gameObject.GetComponent<Rigidbody2D>();
        comp.animator = GetComponent<Animator>();

        setUpAiB(comp, playInfo);
        setUpPB(comp, playInfo);

        setUpWeapons(pi);
    }

    public void setUpWeapons(PlayerInfo pi)
    {
        WeaponPistol pistol = new WeaponPistol(pi.charEnum); //OLD


        weaponHandling.player = GetComponent<PlayerBase>();

        // Tu sa vytvoria vsetky zbrane ktore sa priradia do weapon handling aby sa nemusel volat zbytocne load na sprajtoch
        weaponHandling.weapons.Add(WeaponEnum.pistol, pistol);
        weaponHandling.inventory.Add(pistol);
        weaponHandling.activeWeapon = weaponHandling.inventory[0];

    }





}
