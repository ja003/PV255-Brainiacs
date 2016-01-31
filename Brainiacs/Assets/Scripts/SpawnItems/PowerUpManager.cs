using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//--MG
public class PowerUpManager : MonoBehaviour
{
    public PowerUpEnum type;
    private bool assignedType = false;

    private float floatingSpeed = 0.2f;
    private float time = 0.0f;

    void Update()
    {
        if (!assignedType)
        {
            type = gameObject.transform.parent.gameObject.GetComponent<PowerUpGenerator>().powerUpType;
            assignedType = true;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if ((coll.gameObject.tag == "Player"))
        {
            if (!assignedType)
            {
                type = gameObject.transform.parent.gameObject.GetComponent<PowerUpGenerator>().powerUpType;
                assignedType = true;
            }
            coll.gameObject.SendMessage("AddPowerUp", type);
            assignedType = false;
            gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        transform.Translate(0.0f, floatingSpeed * Time.deltaTime * (float)System.Math.Sin(time), 0.0f);
        time += Time.deltaTime;
    }
}
