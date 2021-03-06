﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSpecialCurieLogic : MonoBehaviour
{
    private BulletManager bulletManager;
    private PlayerBase playerBase;

    private Dictionary<Vector2, Sprite> spriteMapping = new Dictionary<Vector2,Sprite>();
    private Dictionary<Vector2, RuntimeAnimatorController> crashMapping = new Dictionary<Vector2,RuntimeAnimatorController>();

    private Dictionary<Vector2, string> animatorDirectionMapping = new Dictionary<Vector2, string> { { Vector2.up, "up"}, { Vector2.right, "right" }, { Vector2.down, "down" }, { Vector2.left, "left" } };
    private Dictionary<Vector2, string> animatorCrashDirectionMapping = new Dictionary<Vector2, string> { { Vector2.up, "upCrash" }, { Vector2.right, "rightCrash" }, { Vector2.down, "downCrash" },
        { Vector2.left, "leftCrash" } };

    private SpriteRenderer renderer;
    private Animator animator;

    private FireProps fireProps;
    private FireProps fireProps2;
    private bool update = false;
    private bool fall = false;

    private float traveledDistance = 0;
    private float clicksShooted = 0;
    private float shootEveryDst = 0.5f;

    AudioClip carSound;
    AudioClip carCrashSound;
    int chosenAudioSourceIndex;

    public void SetUpVariables(PlayerBase pb, BulletManager bm)
    {
        playerBase = pb;
        bulletManager = bm;
        animator = GetComponent<Animator>();

        string soundLoaderString = "Sounds/Weapon/";
        carSound = Resources.Load(soundLoaderString + "flamethrower") as AudioClip;
        carCrashSound = Resources.Load(soundLoaderString + "einsteinSpecial_explode") as AudioClip;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        fireProps2.position = transform.position;
        if (update)
        {
            transform.position = new Vector2(transform.position.x + (fireProps.direction.x/10),
                transform.position.y + (fireProps.direction.y/10));

            traveledDistance += new Vector2((fireProps.direction.x / 10), (fireProps.direction.y / 10)).magnitude;

            if (traveledDistance > (clicksShooted * shootEveryDst))
            {

                if (fireProps.direction == Vector2.left || fireProps.direction == Vector2.right)
                {
                   fireProps2.direction = Vector2.up;
                    bulletManager.fire(fireProps2);
                    fireProps2.direction = Vector2.down;
                    bulletManager.fire(fireProps2);
                }
                else
                {
                    fireProps2.direction = Vector2.left;
                    bulletManager.fire(fireProps2);
                    fireProps2.direction = Vector2.right;
                    bulletManager.fire(fireProps2);
                }
                clicksShooted++;
            }
        }
        if (fall)
        {
            transform.position = new Vector2(transform.position.x + (fireProps.direction.x / 10),
               transform.position.y + (fireProps.direction.y / 10));

            transform.localScale -= new Vector3(0.03f,0.03f,0.03f);
            if (transform.localScale.x <= 0.5f)
            {
                fall = false;
                StartCoroutine(PlayCrash());
            }
        }
    }

    public void fire(FireProps fp)
    {
        animator.SetBool("returnToOrig", true);
        nullAllAnimBools();
        transform.localScale = new Vector3(1,1,1);
        fireProps = fp;
        fireProps2 = new FireProps(fp);
        moveInShootingDirection();
        fall = false;
        update = true;
        //renderer.sprite = spriteMapping[fp.direction];
        setBoolAnimator(animatorDirectionMapping[fp.direction], true);

        chosenAudioSourceIndex = SoundManager.instance.PlaySingle(carSound, true);

    }

    void moveInShootingDirection()
    {
        Vector2 pos = fireProps.position;

        if (fireProps.direction == Vector2.up)
        {
            transform.position = pos + new Vector2(0, 0.5f);
        }
        else if (fireProps.direction == Vector2.down)
        {
            transform.position = pos + new Vector2(0, -0.7f);
        }
        else if (fireProps.direction == Vector2.left)
        {
            transform.position = pos + new Vector2(-0.5f, 0);
        }
        else
        {
            transform.position = pos + new Vector2(0.5f, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Barrier") || (coll.gameObject.tag == "Border"))
        {
            StartCoroutine(PlayCrash());
        }
        if ((coll.gameObject.tag == "Player"))
        {
            coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(1000, playerBase);
            StartCoroutine(PlayCrash());
        }
        if ((coll.gameObject.tag == "Barrier_notproof"))
        {
            update = false;
            fall = true;
        }
    }

    IEnumerator PlayCrash()
    {
        AudioSource[] AScomp = SoundManager.instance.gameObject.GetComponents<AudioSource>();
        AScomp[chosenAudioSourceIndex].loop = false;

        //animator.runtimeAnimatorController = null;
        //animator.runtimeAnimatorController = crashMapping[fireProps.direction];
        setBoolAnimator(animatorCrashDirectionMapping[fireProps.direction], true);
        fireProps.direction = new Vector2(0, 0);

        SoundManager.instance.PlaySingle(carCrashSound);

        yield return new WaitForSeconds(1.0f);                                               // tento prikaz kurvy cely kod
        animator.SetBool("returnToOrig", true);
        nullAllAnimBools();
        gameObject.SetActive(false);
        
        //animator.runtimeAnimatorController = null;
    }

    private void setBoolAnimator(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }

    private void nullAllAnimBools()
    {
        animator.SetBool("up", false);
        animator.SetBool("right", false);
        animator.SetBool("down", false);
        animator.SetBool("left", false);
        
        animator.SetBool("upCrash", false);
        animator.SetBool("rightCrash", false);
        animator.SetBool("downCrash", false);
        animator.SetBool("leftCrash", false);
        animator.SetBool("returnToOrig", false);
    }
}
