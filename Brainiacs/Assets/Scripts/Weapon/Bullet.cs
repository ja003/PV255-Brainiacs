using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Animator animator;
    public Vector2 direction;
    public float bulletSpeed;
    int damage;
    public bool isActive = false;
    public PlayerBase owner;
    public AudioClip hitSound;

    public void iniciate(Vector2 dir, Vector2 pos, RuntimeAnimatorController animController, float bulletSpd, int dmg, string weapon) {
        damage = dmg;
        direction = new Vector2(dir.x, dir.y);
        bulletSpeed = bulletSpd;

        if (dir == Vector2.up){
            transform.position = pos + new Vector2(0.1f, 0.35f);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dir == Vector2.down) {
            transform.position = pos + new Vector2(-0.1f, -0.75f);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else{
            transform.position = pos + new Vector2(direction.normalized.x*0.5f, -0.17f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        //Debug.Log(animController);
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = animController;
        //load hitSound

        string soundLoaderString = "Sounds/Weapon/";
        string hitSoundString = weapon + "Hit";

        if(Resources.Load(soundLoaderString + hitSoundString) != null)
            hitSound = Resources.Load(soundLoaderString + hitSoundString) as AudioClip;

        gameObject.SetActive(true);
        isActive = true;
    }
    
    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = transform.position + new Vector3(direction.x, direction.y).normalized / (10 / bulletSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Barrier") || (coll.gameObject.tag == "Player") || (coll.gameObject.tag == "Border"))
        {
            if (coll.gameObject.tag == "Player")
            {
                //Debug.Log(coll.name);
                coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(damage, owner);
                SoundManager.instance.RandomizeSfx(hitSound);
            }
            else
            {
                SoundManager.instance.RandomizeSfx(hitSound); //todo barrier hitSound..maybe
            }
            //Debug.Log(coll.name);
            gameObject.SetActive(false);
            isActive = false;
        }
    }
}
