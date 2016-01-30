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
    private FireProps fp;

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

        animator.SetBool("explode", false);
        //load hitSound

        string soundLoaderString = "Sounds/Weapon/";
        string hitSoundString = weapon + "Hit";

        if(Resources.Load(soundLoaderString + hitSoundString) != null)
            hitSound = Resources.Load(soundLoaderString + hitSoundString) as AudioClip;

        gameObject.SetActive(true);
        isActive = true;
    }


    public Bullet iniciate(FireProps fp)
    {
        this.fp = fp;
        damage = fp.damage;
        direction = new Vector2(fp.direction.x, fp.direction.y);
        bulletSpeed = fp.bulletSpeed;

        if (fp.weapEnum != WeaponEnum.mine && fp.weapEnum != WeaponEnum.specialNobel)
        {
            if (fp.direction == Vector2.up)
            {
                transform.position = fp.position + new Vector2(0.1f, 0.35f);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else if (fp.direction == Vector2.down)
            {
                transform.position = fp.position + new Vector2(-0.1f, -0.75f);
                transform.rotation = Quaternion.Euler(0, 0, 90);
            }
            else
            {
                transform.position = fp.position + new Vector2(direction.normalized.x*0.5f, -0.17f);
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }
        }
        else
        {
            transform.position = fp.position;
            Collider2D c2D = GetComponent<Collider2D>();
            Vector2 ofset = c2D.offset;
            ofset.y -= 0.42f;
            c2D.offset = ofset;
        }

        //Debug.Log(animController);
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = fp.animController;
        //load hitSound

        string soundLoaderString = "Sounds/Weapon/";
        string hitSoundString = fp.weapon + "Hit";

        if (Resources.Load(soundLoaderString + hitSoundString) != null)
            hitSound = Resources.Load(soundLoaderString + hitSoundString) as AudioClip;

        gameObject.SetActive(true);
        isActive = true;

        StartCoroutine(delayOnPlant());
        return this;
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

                if (fp.weapEnum == WeaponEnum.mine) animator.SetBool("explode", true);

                //Debug.Log(coll.name);
                coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(damage, owner);
                SoundManager.instance.RandomizeSfx(hitSound);
            }
            else
            {
                SoundManager.instance.RandomizeSfx(hitSound); //todo barrier hitSound..maybe
            }
            //Debug.Log(coll.name);


            StartCoroutine(playExplosion());

        }

        if (coll.gameObject.tag == "Tank")
        {
            if (fp.weapEnum == WeaponEnum.mine || fp.weapEnum == WeaponEnum.specialNobel)
            {
                if (fp.weapEnum == WeaponEnum.mine) animator.SetBool("explode", true);
                coll.gameObject.GetComponent<WeaponSpecialDaVinciLogic>().HitTank(100);
                coll.transform.parent.FindChild("Player1").GetComponent<PlayerBase>().ApplyDamage(100, owner);
                SoundManager.instance.RandomizeSfx(hitSound);
                StartCoroutine(playExplosion());
            }
            else
            {
                coll.gameObject.GetComponent<WeaponSpecialDaVinciLogic>().HitTank(damage);
                StartCoroutine(playExplosion());
            }
        }
    }

    IEnumerator playExplosion()
    {
        if (fp.weapEnum == WeaponEnum.mine)
        {
            GetComponent<Collider2D>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            GetComponent<Collider2D>().enabled = true;
        }
        if (fp.weapEnum == WeaponEnum.mine) animator.SetBool("explode", false);
        gameObject.SetActive(false);
        animator.runtimeAnimatorController = null;
        isActive = false;
    }


    IEnumerator delayOnPlant()
    {
        if (fp.weapEnum == WeaponEnum.mine)
        {
            GetComponent<Collider2D>().enabled = false;
            yield return new WaitForSeconds(2f);
            GetComponent<Collider2D>().enabled = true;
        }

    }


}
