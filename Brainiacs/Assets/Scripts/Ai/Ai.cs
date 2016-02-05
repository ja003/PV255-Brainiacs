using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Ai : AiBase {

    Components comp = new Components();
    PlayerInfo playInfo = new PlayerInfo();

    void Start()
    {
        frameCountSinceLvlLoad = 0;
        RandomizeDirection();
        RefreshAnimatorState();

        UpdateAnimatorState(AnimatorStateEnum.stop);


        rb2d = gameObject.GetComponent<Rigidbody2D>();
        speed = 2f;

        aiActionLogic.currentAction = AiActionEnum.stand;

        //InitializeBullets();
    }

    public void SetUpPlayer(PlayerInfo pi)
    {

        //base.playerNumber = 3;
        base.speed = 2f;
        base.direction = base.left;
        base.isAi = true;

        playInfo = pi;
        
        comp.spriteRend = GetComponent<SpriteRenderer>();
        comp.rb2d = gameObject.GetComponent<Rigidbody2D>();
        comp.animator = GetComponent<Animator>();

        setUpAiB(comp, playInfo);
        setUpPB(comp, playInfo);

        setUpWeapons(pi);

        
    }
}
