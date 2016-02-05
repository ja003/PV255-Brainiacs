using UnityEngine;
using System.Collections;

public class WeaponSpecialDaVinciLogic : MonoBehaviour
{

    private BulletManager bulletManager;
    private PlayerBase playerBase;

    private SpriteRenderer renderer;
    private Animator animator;

    private FireProps fireProps;
    public bool update = false;

    private WeaponBase wb;
    private WeaponHandling wh;

    private float oldSpeed;

    public int hp = 100;
    private int exClicks;

    void Start()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    
    public void SetUpVariables(PlayerBase pb, BulletManager bm, WeaponBase wb)
    {
        transform.localScale = new Vector3(1.1f, 1.1f, 0);
        this.wb = wb;
        playerBase = pb;
        bulletManager = bm;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (update)
        {
            if (exClicks > 0)
            {
                exClicks--;
                Vector2 v = playerBase.gameObject.transform.position;
                v.y += 0.05f;
                transform.position = v;
            }
            else
            {
                StopTank();
            }
        }
    }

    public void StopTank()
    {
        wh.shootingEnabled = true;
        update = false;
        playerBase.speed = oldSpeed;
        StartCoroutine(PlayCrash());
        GetComponent<Collider2D>().enabled = false;
        //playerBase.gameObject.GetComponent<Collider2D>().enabled = true;
    }

    public void HitTank(int dmg)
    {
        hp -= dmg;
        if (hp <= 0) StopTank();
    }

    public void fire(FireProps fp, WeaponHandling wh)
    {
        transform.position = fp.position;
        this.wh = wh;
        nullAllAnimBools();
        animator.SetBool("returnToOrig", true);
        nullAllAnimBools();
        animator.SetBool("startTank", true);
        hp = 100;
        fireProps = fp;
        update = true;
        exClicks = wb.existingClicks;
        wh.shootingEnabled = false;
        oldSpeed = playerBase.speed;
        playerBase.speed = fp.bulletSpeed;

        //playerBase.gameObject.GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = true;

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (update)
        {
            if ((coll.gameObject.tag == "Player") && coll.gameObject != playerBase.gameObject)
            {
                coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(100, playerBase);
                //StartCoroutine(PlayCrash());
            }
        }
    }

    IEnumerator PlayCrash()
    {
        nullAllAnimBools();
        animator.SetBool("stopTank", true);
        yield return new WaitForSeconds(1.0f);
        nullAllAnimBools();
        animator.SetBool("returnToOrig", false);

    }

    private void setBoolAnimator(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    }

    private void nullAllAnimBools()
    {
        animator.SetBool("startTank", false);
        animator.SetBool("stopTank", false);
        animator.SetBool("returnToOrig", false);

    }
}
