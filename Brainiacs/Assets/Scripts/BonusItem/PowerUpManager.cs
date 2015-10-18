using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//--MG
public class PowerUpManager : MonoBehaviour {

    private PowerUpEnum type;
    private bool assignedType = false;

    private int directionIndex;
    private List<Vector3> floatingDirection;
    private Vector3 currentDirection;
    private float floatingSpeed;
    private float floatingTime;
    private float time;

    void Start()
    {
        directionIndex = 0;
        floatingDirection = new List<Vector3>();
        floatingDirection.Add(Vector3.up);
        floatingDirection.Add(Vector3.zero);
        floatingDirection.Add(Vector3.down);
        floatingDirection.Add(Vector3.zero);
        currentDirection = floatingDirection[directionIndex];

        floatingSpeed = 0.1f;
        floatingTime = 1.0f;
        time = 0.0f;
    }

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
        if (time > floatingTime)
        {
            currentDirection = NextDirection();
            time = 0.0f;
        }
        transform.Translate(currentDirection.x * Time.deltaTime * floatingSpeed,
                            currentDirection.y * Time.deltaTime * floatingSpeed, 0);
        time += Time.deltaTime;
    }

    private Vector3 NextDirection()
    {
        if (directionIndex + 1 > floatingDirection.Count)
        {
            directionIndex = 0;
        }
        else
        {
            directionIndex += 1;
        }
        return floatingDirection[directionIndex];
    }
}
