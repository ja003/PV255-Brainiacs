using UnityEngine;
using System.Collections;

public class TeleportBase : MonoBehaviour {

    protected Vector2 position;
    public TeleportBase otherTeleport;

    void OnTriggerEnter2D(Collider2D coll)
    {   
        coll.gameObject.GetComponent<HumanBase>().Teleport(otherTeleport.position);
    }


}
