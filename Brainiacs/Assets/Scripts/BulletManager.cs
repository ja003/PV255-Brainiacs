using UnityEngine;
using System.Collections;

public class BulletManager : MonoBehaviour {

    public float speed = 5;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);     //vystrelenie projektilu
        

    }
    /// <summary>
    /// Ak je zdetekovana kolizia gulka sa zneaktivni a je pripravena na dalsie pouzitie
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player") || (coll.gameObject.tag == "Barrier"))
            gameObject.SetActive(false);            
           

    }
    

}
