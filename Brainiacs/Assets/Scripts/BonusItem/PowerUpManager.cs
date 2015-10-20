using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//--MG
public class PowerUpManager : MonoBehaviour
{

    private PowerUpEnum type;
    private bool assignedType = false;

    private float floatingSpeed = 1.0f;
    private float time = 0.0f;

    void Update()
    {
        if (!assignedType)
        {
            type = gameObject.transform.parent.gameObject.GetComponent<PowerUp>().powerUpType;
            assignedType = true;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player"))
        {
            coll.gameObject.SendMessage("AddPowerUp", type);
            Debug.Log(type);
            assignedType = false;
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(0, (float)System.Math.Sin(floatingSpeed * Time.deltaTime * time), 0);
        time += Time.deltaTime;
    }
}
