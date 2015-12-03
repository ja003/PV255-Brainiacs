using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1 : HumanBase
{

    Components comp = new Components();
    PlayerInfo playInfo = new PlayerInfo();

    void Start()
    {
        /*
        weaponHandling = GetComponent<WeaponHandling>();
        Debug.Log(weaponHandling);
        comp.spriteRend = GetComponent<SpriteRenderer>();
        comp.rb2d       = gameObject.GetComponent<Rigidbody2D>();

        
        playInfo.charEnum       = CharacterEnum.Tesla;
        playInfo.playerNumber   = 1;
        playInfo.playerColor    = "Blue";
        
        base.playerNumber = 1;
        base.speed = 2f;
        base.direction = base.right;
        
        SetControlKeys(KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftControl, KeyCode.LeftShift);
        
        ////////////////////////////////////////////////////////// WEAPON HANDLING ////////////////////////////////////////////
        
        WeaponPistol pistol = new WeaponPistol(CharacterEnum.Tesla);
        weaponHandling.inventory.Add(pistol);
        weaponHandling.activeWeapon = weaponHandling.inventory[0];
        weaponHandling.weaponRenderer = transform.Find("weapon").GetComponent<SpriteRenderer>();
        weaponHandling.weaponRenderer.sprite = Resources.Load<Sprite>(pistol.sprite);
        //TRY
        WeaponBase skvrna = new Skvrna();
        weaponHandling.inventory.Add(skvrna);
        
        /////////////////////////////////////////////////////////// END WH ///////////////////////////////////////////////////
        
        S
        setUpHB(comp, playInfo);
        setUpPB(comp, playInfo);
        */
        //Debug.Log("start");
    }

    public void SetUpPlayer(PlayerInfo pi, ControlKeys ck)
    {

        
        comp.animator = gameObject.GetComponent<Animator>();
        //comp.animator.runtimeAnimatorController = Resources.Load("Animations/currie_red_override") as RuntimeAnimatorController;
    
        base.playerNumber = 1;
        base.speed = 2f;
        base.direction = base.right;

        playInfo = pi;
        SetControlKeys(ck.keyUp, ck.keyLeft, ck.keyDown, ck.keyRight, ck.keyFire, ck.keySwitchWeapon);
       
        comp.spriteRend = GetComponent<SpriteRenderer>();
        comp.rb2d = gameObject.GetComponent<Rigidbody2D>();
        comp.animator = GetComponent<Animator>();

        setUpHB(comp, playInfo);
        setUpPB(comp, playInfo);

        setUpWeapons(pi);
        
    }

    

    void Update()
    {
        base.Movement();
    }

   

}
