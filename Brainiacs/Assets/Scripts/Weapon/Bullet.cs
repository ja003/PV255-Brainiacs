using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int damage = 20;
    public Vector2 direction;
    public float speed;
    public bool isActive = false;

    public void iniciate(Vector2 dir, Vector2 pos, string sprt) {

        transform.position = pos + dir.normalized;
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(sprt);
        direction = dir;
        gameObject.SetActive(true);
        isActive = true;
    }
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isActive)
        {
            transform.position = transform.position + new Vector3(direction.x, direction.y).normalized / 10;
            Debug.Log("fire3");
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Barrier") || (coll.gameObject.tag == "Player"))
        {
            if (coll.gameObject.tag == "Player")
            {
                coll.gameObject.GetComponent<PlayerBase>().ApplyDamage(damage);
            }
            gameObject.SetActive(false);
            isActive = false;
        }
    }
}
