using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    //Player1 player1;
    //Player1 p1;
    //GameObject player1;

    private List<PowerUp> powerUps;
    private int powerUpPool = 10;
    private int indexOfReadiedPowerUp;

    private bool isReady = false;
    private float spawnInterval = 15.0f;
    private float activationDelay = 1.0f;
    private float time = 0.0f;

    void Start()
    {
        //player1.AddComponent("p1");

        powerUps = new List<PowerUp>();
        for (int i = 0; i < powerUpPool; i++)
        {
            powerUps.Add(new PowerUp());
            //powerUps[i].SetReady();
        }
    }

    void FixedUpdate()
    {
        if (isReady)
        {
            if (time > activationDelay)
            {
                time = 0.0f;
                powerUps[indexOfReadiedPowerUp].Activate();
                isReady = false;
            }
        }
        else
        {
            if (time > spawnInterval)
            {
                time = 0.0f;
                for (int i = 0; i < powerUpPool; i++)
                {
                    if (powerUps[i].prefab == null)
                    {
                        indexOfReadiedPowerUp = i;
                        powerUps[i].SetReady();
                        isReady = true;
                        break;
                    }
                }
            }
        }
        time += Time.deltaTime;
    }
}