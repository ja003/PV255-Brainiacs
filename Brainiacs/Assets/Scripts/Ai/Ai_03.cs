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

        inventory = new List<WeaponBase>();
        WeaponBase pistol = new WeaponPistol(CharacterEnum.Tesla);
        inventory.Add(pistol);
        WeaponBase special = new WeaponTeslaSpecial();
        inventory.Add(special);

        activeWeapon = inventory[0];

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

        setUpAiB(comp, playInfo);
        setUpPB(comp, playInfo);

        setUpWeapons(pi);
    }

    public void setUpWeapons(PlayerInfo pi)
    {
        WeaponPistol pistol = new WeaponPistol(pi.charEnum); //OLD
        WeaponSniper sniper = new WeaponSniper(); //just for now

        weaponHandling.player = GetComponent<PlayerBase>();

        // Tu sa vytvoria vsetky zbrane ktore sa priradia do weapon handling aby sa nemusel volat zbytocne load na sprajtoch
        weaponHandling.weapons.Add(WeaponEnum.sniper, sniper);

        weaponHandling.inventory.Add(sniper);
        weaponHandling.activeWeapon = weaponHandling.inventory[0];
        weaponHandling.weaponRenderer = transform.Find("weapon").GetComponent<SpriteRenderer>();
    }





}
