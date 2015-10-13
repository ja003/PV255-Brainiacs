using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileShooter : MonoBehaviour {

    //GameObject prefab;
    //Vector3 forward = new Vector3(1, 0, 0);

    public float speed = 1;
    public float delayTime = 0.5f;
    public float counter = 0;
    public GameObject bullet;                   //priradi sa sem prefab daneho projektilu

    public int pooledAmount;                    //pocet projektily ktore sa vytvoria na zaciatku a dalej sa budu len recyklovat
    private List<GameObject> bullets;           //list pre tieto projektily
    private Vector3 forward;

	// Use this for initialization
	void Start () {
        //delayTime = 0.5f;
        //speed = 1;
        forward = new Vector3(0.8f,0,0);
        bullets = new List<GameObject>();
        for (int i=0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);           //nastavenie toho, ze sa gulka nepouziva
            bullets.Add(obj);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	    if(Input.GetKey(KeyCode.Q) && counter > delayTime)
        {
            //GameObject projectile = Instantiate(prefab) as GameObject;
            //Destroy(projectile.gameObject, 1);
            //projectile.transform.position = transform.position + forward;
            //Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            //rb.velocity = forward * speed;

            Fire();
            counter = 0;
        }

        counter += 1 * Time.deltaTime;
	}

    void Fire()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].transform.position = (transform.position + forward);
                bullets[i].transform.rotation = transform.rotation;
                bullets[i].SetActive(true);
                break;

            }
        }
    }
}
