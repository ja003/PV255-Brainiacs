using UnityEngine;
using System.Collections;

public class WeaponFlamethrowerLogic : MonoBehaviour
{
    private bool initialized;
    private Animator animator;
    private Collider2D colider;
    private Vector2 dir;
    private PlayerBase pb;
    private FireProps fp;
    private WeaponBase wb;

    private bool fired = false;
    private int ticks = 0;

    void Start()
    {
        colider = GetComponent<Collider2D>();
        colider.enabled = false;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate ()
    {
        if (fired)
        {
            GameObject go = pb.gameObject;
            Transform t = go.transform;
            dir = pb.direction; //t.position;

            if (dir == Vector2.up)
            {
                transform.position =
                    new Vector2(pb.gameObject.transform.position.x + 0.775f, pb.gameObject.transform.position.y) +
                    2.65f*dir;
                transform.rotation = Quaternion.Euler(0, 0, -90);
            }
            else if (dir == Vector2.down)
            {
                transform.position =
                    new Vector2(pb.gameObject.transform.position.x - 0.625f, pb.gameObject.transform.position.y) +
                    2.75f*dir;
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (dir == Vector2.left)
            {
                transform.position =
                    new Vector2(pb.gameObject.transform.position.x, pb.gameObject.transform.position.y + 0.35f) +
                    2.6f*dir;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (dir == Vector2.right)
            {
                transform.position =
                    new Vector2(pb.gameObject.transform.position.x, pb.gameObject.transform.position.y + 0.35f) +
                    2.6f*dir;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }


            if (wb.ammo > 0)
            {
                wb.ammo--;
            }
        }
    }

    public void fire(FireProps fp, PlayerBase pb, WeaponBase wb)
    {
        this.pb = pb;
        this.fp = fp;
        this.wb = wb;
        fired = true;
        colider.enabled = true;
        nullAllAnimBools();
        setBoolAnimator("flameStart", true);
        
    }

    public void stopFire()
    {
        nullAllAnimBools();
        setBoolAnimator("flameEnd", true);
        colider.enabled = false;
        fired = false;
    }

    private void setBoolAnimator(string boolName, bool value)
    {
        animator.SetBool(boolName, value);
    
    }

    private void nullAllAnimBools()
    {
        animator.SetBool("flameStart", false);
        animator.SetBool("flameEnd", false);

    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player"))
        {

            coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(1, pb);

        }

    }
    /*
    void OnTriggerStay2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player"))
        {

            coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(1, pb);

        }

    }*/
}
