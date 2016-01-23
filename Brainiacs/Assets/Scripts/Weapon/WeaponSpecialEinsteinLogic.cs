using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSpecialEinsteinLogic : MonoBehaviour {

    private BulletManager bulletManager;
    private PlayerBase playerBase;

    private Dictionary<Vector2, Sprite> spriteMapping = new Dictionary<Vector2, Sprite>();
    private Dictionary<Vector2, RuntimeAnimatorController> crashMapping = new Dictionary<Vector2, RuntimeAnimatorController>();

    private Dictionary<Vector2, string> animatorDirectionMapping = new Dictionary<Vector2, string> { { Vector2.up, "up" }, { Vector2.right, "right" }, { Vector2.down, "down" }, { Vector2.left, "left" } };
    private Dictionary<Vector2, string> animatorCrashDirectionMapping = new Dictionary<Vector2, string> { { Vector2.up, "upCrash" }, { Vector2.right, "rightCrash" }, { Vector2.down, "downCrash" },
        { Vector2.left, "leftCrash" } };

    private SpriteRenderer renderer;
    private Animator animator;

    private FireProps fireProps;
    private bool update = false;

    private float traveledDistance = 0;
    private float clicksShooted = 0;
    private float shootEveryDst = 0.75f;

 
    public void SetUpVariables(PlayerBase pb, BulletManager bm)
    {
        playerBase = pb;
        bulletManager = bm;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }

    public void fire(FireProps fp)
    {
        animator.SetBool("returnToOrig", true);
        nullAllAnimBools();

        fireProps = fp;
        moveInShootingDirection();
        update = true;
        //renderer.sprite = spriteMapping[fp.direction];
        setBoolAnimator(animatorDirectionMapping[fp.direction], true);

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
    }

    IEnumerator PlayCrash()
    {
        //animator.runtimeAnimatorController = null;
        //animator.runtimeAnimatorController = crashMapping[fireProps.direction];
        setBoolAnimator(animatorCrashDirectionMapping[fireProps.direction], true);
        fireProps.direction = new Vector2(0, 0);
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
