using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : HumanBase
{

    Components comp = new Components();
    PlayerInfo playInfo = new PlayerInfo();
    
    public void SetUpPlayer(PlayerInfo pi, ControlKeys ck)
    {

        
        comp.animator = gameObject.GetComponent<Animator>();
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
