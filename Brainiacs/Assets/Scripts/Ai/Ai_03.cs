using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ai_03 : AiBase {

    Components comp = new Components();
    PlayerInfo playInfo = new PlayerInfo();

    void Start()
    {
        playerNumber = 3;
        base.aiPriorityLogic.killPlayer1Priority = 0;
        base.aiPriorityLogic.killPlayer2Priority = 0;
        base.aiPriorityLogic.killPlayer3Priority = 0;
        base.aiPriorityLogic.killPlayer4Priority = 0;


        
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        speed = 2f;

        aiActionLogic.currentAction = AiActionEnum.stand;

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
        WeaponBase pistol = new WeaponPistol(pi.charEnum);
        WeaponBase sniper = new WeaponSniper();
        WeaponBase biogun = new WeaponBiogun();
        //WeaponBase flameTh = new WeaponFlamethrower();
        WeaponBase MP40 = new WeaponMP40();

        weaponHandling.player = GetComponent<PlayerBase>();

        // Tu sa vytvoria vsetky zbrane ktore sa priradia do weapon handling aby sa nemusel volat zbytocne load na sprajtoch
        weaponHandling.weapons.Add(WeaponEnum.sniper, sniper);
        weaponHandling.weapons.Add(WeaponEnum.pistol, pistol);
        weaponHandling.weapons.Add(WeaponEnum.biogun, biogun);
        weaponHandling.weapons.Add(WeaponEnum.MP40, MP40);

        // Inicializacia prvej zbrane
        weaponHandling.inventory.Add(pistol);
        //weaponHandling.inventory.Add(sniper);
        //weaponHandling.inventory.Add(biogun);
        //weaponHandling.inventory.Add(MP40);
        weaponHandling.activeWeapon = weaponHandling.inventory[0];
        //weaponHandling.

        

    }

    /*
    public void setUpWeapons(PlayerInfo pi)
    {
        WeaponPistol pistol = new WeaponPistol(pi.charEnum); //OLD


        weaponHandling.player = GetComponent<PlayerBase>();

        // Tu sa vytvoria vsetky zbrane ktore sa priradia do weapon handling aby sa nemusel volat zbytocne load na sprajtoch
        weaponHandling.weapons.Add(WeaponEnum.pistol, pistol);
        weaponHandling.inventory.Add(pistol);
        weaponHandling.activeWeapon = weaponHandling.inventory[0];
        Debug.Log("active = " + weaponHandling.activeWeapon);

    }*/





}
