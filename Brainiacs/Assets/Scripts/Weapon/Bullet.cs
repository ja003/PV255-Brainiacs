using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage = 20;
    public Vector2 direction;
    public float speed;
    public bool isActive = false;

    public void iniciate(Vector2 dir, Vector2 pos, string sprt) {
        direction = new Vector2(dir.x, dir.y);
        //transform.position = pos + direction.normalized;
        

        if (dir == Vector2.up){
            transform.position = pos + new Vector2(0.2f, 0.3f);
        }
        else if (dir == Vector2.down) {
            //Debug.Log("down");
            //Debug.Log(pos + new Vector2(-0.07f, -0.5f));
            transform.position = pos + new Vector2(-0.1f, -0.75f);
        }
        else{
            transform.position = pos + new Vector2(direction.normalized.x*0.5f, -0.17f);
        } 
        
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprt);
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
            transform.position = transform.position + new Vector3(direction.x, direction.y).normalized / 10;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Barrier") || (coll.gameObject.tag == "Player"))
        {
            if (coll.gameObject.tag == "Player")
            {
                //Debug.Log(coll.name);
                coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(damage);
            }
            //Debug.Log(coll.name);
            gameObject.SetActive(false);
            isActive = false;
        }
    }
}
