using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponSpecialCurieLogic : MonoBehaviour
{
    private BulletManager bulletManager;

    private Dictionary<Vector2, Sprite> spriteMapping = new Dictionary<Vector2,Sprite>();
    private Dictionary<Vector2, RuntimeAnimatorController> crashMapping = new Dictionary<Vector2,RuntimeAnimatorController>();

    private SpriteRenderer renderer;
    private RuntimeAnimatorController rtac;

    private FireProps fireProps;
    private bool update = false;

    private float traveledDistance = 0;
    private float clicksShooted = 0;
    private float shootEveryDst = 0.75f;

    private int framecount = 0;

    // Use this for initialization
    void Start ()
    {
    }

    public void SetUpGraphics()
    {
        renderer = GetComponent<SpriteRenderer>();
        rtac = GetComponent<RuntimeAnimatorController>();

        Debug.Log("Start from curie special");
        spriteMapping.Add(Vector2.up, Resources.LoadAll<Sprite>("Sprites/Special/bullet_curieSpecial_up")[0]);
       // crashMapping.Add(Vector2.up, Resources.Load<Sprite>(""));

        spriteMapping.Add(Vector2.left, Resources.LoadAll<Sprite>("Sprites/Special/bullet_curieSpecial_left")[0]);
       // crashMapping.Add(Vector2.left, Resources.Load<Sprite>(""));

        spriteMapping.Add(Vector2.right, Resources.LoadAll<Sprite>("Sprites/Special/bullet_curieSpecial_right")[0]);
     //   crashMapping.Add(Vector2.right, Resources.Load<Sprite>(""));

        spriteMapping.Add(Vector2.down, Resources.LoadAll<Sprite>("Sprites/Special/bullet_curieSpecial_down")[0]);
    //    crashMapping.Add(Vector2.down, Resources.Load<Sprite>(""));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (update)
        {
            transform.position = new Vector2(transform.position.x + (fireProps.direction.x/10),
                transform.position.y + (fireProps.direction.y/10));

            traveledDistance += new Vector2((fireProps.direction.x / 10), (fireProps.direction.y / 10)).magnitude;

            if (framecount % 50 == 0)
            {
                bulletManager.fire(Vector2.up, transform.position , fireProps.animController, fireProps.bulletSpeed, fireProps.damage, "sniper" );
                bulletManager.fire(Vector2.down, transform.position, fireProps.animController, fireProps.bulletSpeed, fireProps.damage, "sniper");
            }
        }
        framecount ++;
    }

    public void fire(BulletManager bm, FireProps fp)
    {
        bulletManager = bm;
        fireProps = fp;
        transform.position = fp.position;
        update = true;
        renderer.sprite = spriteMapping[fp.direction];
        Debug.Log("fire from Logic");
    }

    void OnCollisionEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Barrier") || (coll.gameObject.tag == "Border"))
        {
         //   yield WaitForSeconds (2);
            gameObject.SetActive(false);
        }
    }
}
