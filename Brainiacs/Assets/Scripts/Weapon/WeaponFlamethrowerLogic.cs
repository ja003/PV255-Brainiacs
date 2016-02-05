using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponFlamethrowerLogic : MonoBehaviour
{
    private bool initialized;
    private Animator animator;
    private Collider2D colider;
    private Vector2 dir;
    private PlayerBase pb;
    private FireProps fp;
    private WeaponBase wb;

    private AudioClip flamethrowerSound;

    private bool fired = false;
    private int ticks = 0;

    private List<PlayerBase> inCollider = new List<PlayerBase>();

    void Start()
    {
        colider = GetComponent<Collider2D>();
        colider.enabled = false;
        animator = GetComponent<Animator>();

        string soundLoaderString = "Sounds/Weapon/";
        flamethrowerSound = Resources.Load(soundLoaderString + "flamethrower") as AudioClip;

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

            foreach (var ic in inCollider)
            {
                ic.ApplyDamage(1, pb);
            }

            if (wb.ammo > 0)
            {
                wb.ammo--;
            }
        }
    }

    int chosenAudioSourceIndex;


    bool soundLoop = false;

    public void fire(FireProps fp, PlayerBase pb, WeaponBase wb)
    {
        //Debug.Log("FIRE");
        this.pb = pb;
        this.fp = fp;
        this.wb = wb;
        fired = true;
        colider.enabled = true;
        nullAllAnimBools();
        setBoolAnimator("flameStart", true);

        if (!pb.isAi)
        {
            chosenAudioSourceIndex = SoundManager.instance.PlaySingle(flamethrowerSound, true);

        }
        else if(pb.isAi && !soundLoop)
        {
            chosenAudioSourceIndex = SoundManager.instance.PlaySingle(flamethrowerSound, true);
            soundLoop = true;
        }
        Debug.Log("Start");
    }

    public void stopFire()
    {
        if (!pb.isAi || soundLoop)
        {
            AudioSource[] AScomp = SoundManager.instance.gameObject.GetComponents<AudioSource>();
            AScomp[chosenAudioSourceIndex].loop = false;
            Debug.Log("Stop");
            soundLoop = false;
        }


        nullAllAnimBools();
        setBoolAnimator("flameEnd", true);
        colider.enabled = false;
        fired = false;
        inCollider.Clear();
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
    /*
    void OnTriggerStay2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player"))
        {

            coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(1, pb);

        }

    }*/
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player"))
        {
            coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(2,pb);
            inCollider.Add(coll.gameObject.GetComponent<PlayerBase>());

        }
    }


    void OnTriggerExit2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player"))
        {
            inCollider.Remove(coll.gameObject.GetComponent<PlayerBase>());
        }
    }



}
