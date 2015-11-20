﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    Animator animator;
    public int damage = 20;
    public Vector2 direction;
    public float speed;
    public bool isActive = false;

    public void iniciate(Vector2 dir, Vector2 pos, RuntimeAnimatorController animController) {
        direction = new Vector2(dir.x, dir.y);
        //transform.position = pos + direction.normalized;
        

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
        animator = GetComponent<Animator>();
        Debug.Log(animController);
        animator.runtimeAnimatorController = animController;
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
