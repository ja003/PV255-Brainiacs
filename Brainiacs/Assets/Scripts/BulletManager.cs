using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

    public float speed = 5;
    public Vector2 direction { get; set; }
    public bool directionAssigned = false;


    // Update is called once per frame
    void Update()
    {
        if (!directionAssigned)
        {
            //Debug.Log("Dir assigned");
            direction = gameObject.transform.parent.parent.GetComponent<PlayerBase>().direction;
            directionAssigned = true;
        }

        transform.Translate(direction.x * speed * Time.deltaTime, direction.y * speed * Time.deltaTime, 0);     //vystrelenie projektilu
        

    }
    /// <summary>
    /// Ak je zdetekovana kolizia gulka sa zneaktivni a je pripravena na dalsie pouzitie
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Barrier") || (coll.gameObject.tag == "Player"))
        {
            if(coll.gameObject.tag == "Player"){
                coll.gameObject.SendMessage("ApplyDamage", gameObject.transform.parent.gameObject.GetComponent<BulletShooter>().damage);
            }
            gameObject.SetActive(false);
            directionAssigned = false;
        }
    }
}
