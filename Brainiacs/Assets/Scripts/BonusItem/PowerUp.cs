using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using Brainiacs.Generate;

//--MG
public class PowerUp : MonoBehaviour
{
    public PowerUpEnum powerUpType;
    
    private GameObject tmp;
    private int pooledAmount = 5;
    private int numberOfPrefabedPowerUps = 15;
    
    private List<GameObject> shields;
    private List<GameObject> heals;
    private List<GameObject> ammos;
    private List<GameObject> speeds;
    private List<GameObject> mysteries;

    private float time = 0.0f;
    private float spawnInterval = 5.0f;
    
    void Start(){
        
        shields = new List<GameObject>();
        heals = new List<GameObject>();
        ammos = new List<GameObject>();
        speeds = new List<GameObject>();
        mysteries = new List<GameObject>();
        

        tmp = (GameObject)Resources.Load("Prefabs/Shield");
        
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(tmp);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            shields.Add(obj);
        }

        tmp = (GameObject)Resources.Load("Prefabs/Heal");

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(tmp);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            heals.Add(obj);
        }

        tmp = (GameObject)Resources.Load("Prefabs/Ammo");

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(tmp);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            ammos.Add(obj);
        }

        tmp = (GameObject)Resources.Load("Prefabs/Speed");

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(tmp);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            speeds.Add(obj);
        }

        tmp = (GameObject)Resources.Load("Prefabs/Mystery");

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(tmp);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            mysteries.Add(obj);
        }
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time > spawnInterval)
        {
            time = 0.0f;
            SpawnPowerUp();
        }
    }

    public void SpawnPowerUp()
    {
        System.Random rnd = new System.Random();
        PositionGenerator generator = new PositionGenerator();

        var v = Enum.GetValues(typeof(PowerUpEnum));
        powerUpType = (PowerUpEnum)v.GetValue(rnd.Next(numberOfPrefabedPowerUps));

        switch (powerUpType)
        {
            case PowerUpEnum.Shield:
                for (int i = 0; i < pooledAmount; i++)
                {
                    if (!shields[i].activeInHierarchy)
                    {
                        shields[i].transform.position = generator.GenerateRandomPosition();
                        shields[i].SetActive(true);
                        Debug.Log("Shield is generated" + " X: " + shields[i].transform.position.x + " Y: " + shields[i].transform.position.y);
                        break;
                    }
                }
                break;
            case PowerUpEnum.Heal:
                for (int i = 0; i < pooledAmount; i++)
                {
                    if (!heals[i].activeInHierarchy)
                    {
                        heals[i].transform.position = generator.GenerateRandomPosition();
                        heals[i].SetActive(true);
                        Debug.Log("Heal is generated" + " X: " + heals[i].transform.position.x + " Y: " + heals[i].transform.position.y);
                        break;
                    }
                }
                break;
            case PowerUpEnum.Ammo:
                for (int i = 0; i < pooledAmount; i++)
                {
                    if (!ammos[i].activeInHierarchy)
                    {
                        ammos[i].transform.position = generator.GenerateRandomPosition();
                        ammos[i].SetActive(true);
                        Debug.Log("Ammo is generated" + " X: " + ammos[i].transform.position.x + " Y: " + ammos[i].transform.position.y);
                        break;
                    }
                }
                break;
            case PowerUpEnum.Speed:
                for (int i = 0; i < pooledAmount; i++)
                {
                    if (!speeds[i].activeInHierarchy)
                    {
                        speeds[i].transform.position = generator.GenerateRandomPosition();
                        speeds[i].SetActive(true);
                        Debug.Log("Speed is generated" + " X: " + speeds[i].transform.position.x + " Y: " + speeds[i].transform.position.y);
                        break;
                    }
                }
                break;
            case PowerUpEnum.Mystery:
                for (int i = 0; i < pooledAmount; i++)
                {
                    if (!mysteries[i].activeInHierarchy)
                    {
                        mysteries[i].transform.position = generator.GenerateRandomPosition();
                        mysteries[i].SetActive(true);
                        Debug.Log("Mystery is generated" + " X: " + mysteries[i].transform.position.x + " Y: " + mysteries[i].transform.position.y);
                        break;
                    }
                }
                break;
            default:
                Debug.Log("Unknown powerUp is ready.");
                break;
        }
    }
    
}
