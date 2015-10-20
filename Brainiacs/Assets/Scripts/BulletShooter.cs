using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public class BulletShooter : MonoBehaviour {

    //GameObject prefab;
    //Vector3 forward = new Vector3(1, 0, 0);

    public float speed = 1;
    public float delayTime = 0.5f;
    public float counter = 0;
    public GameObject bullet;                   //priradi sa sem prefab daneho projektilu

    public int pooledAmount = 5;                    //pocet projektily ktore sa vytvoria na zaciatku a dalej sa budu len recyklovat
    private List<GameObject> bullets;           //list pre tieto projektily
    public Vector3 direction {get; set; }
    private KeyCode keyFire;

    //MG
    public int damage;
    //JP 
    PlayerBase pb;



	// Use this for initialization
	void Start () {

        //JP
        pb = GetComponent<PlayerBase>();
        

        //direction = transform.Find("Tesla1").GetComponent<PlayerBase>().direction;
        //direction = new Vector2(1, 0);
        bullets = new List<GameObject>();
        //bullet = GameObject.Find("Prefabs/Electricity");
        bullet = (GameObject)Resources.Load("Prefabs/Electricity"); 
        


        pooledAmount = 5;
        for (int i=0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);           //nastavenie toho, ze sa gulka nepouziva
            bullets.Add(obj);
            
        }
        

    }   
	
	// Update is called once per frame
	void FixedUpdate () {
        direction = gameObject.transform.parent.gameObject.GetComponent<PlayerBase>().direction;
        if (Input.anyKey && keyFire == KeyCode.None)
        {
            keyFire = gameObject.transform.parent.gameObject.GetComponent<HumanBase>().keyFire;
        }

        if (Input.GetKey(keyFire) && counter > delayTime)
        {
            Fire();
            counter = 0;
        }

        counter += 1 * Time.deltaTime;
	}

    void Fire()
    {
        System.Random rnd = new System.Random();
        damage = rnd.Next(20, 30); //podla danej zbrane -> zbran musi mat min a max dmg
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].transform.position = (transform.position + direction);
                bullets[i].transform.rotation = transform.rotation;
                bullets[i].SetActive(true);
                break;

            }
        }
    }
}
