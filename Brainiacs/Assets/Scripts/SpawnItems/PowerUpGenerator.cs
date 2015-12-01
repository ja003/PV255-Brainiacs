using UnityEngine;
using System;
using System.Collections.Generic;

//--MG
public class PowerUpGenerator : MonoBehaviour
{
    public PowerUpEnum powerUpType;
    
    private GameObject tmp;
    private int pooledAmount = 5;              //count of each generated power up
    private const int countOfPrefabedPowerUps = 5;   //count of PowerUpEnum items
    
    private List<GameObject>[] powerUps;

    private float time = 0.0f;
    private float spawnInterval = 8.0f; //malý pro testování

    private string location = "Prefabs/SpawnItems/PowerUps/powerup_";
    private string[] powerUpNames = new string[countOfPrefabedPowerUps] { "shield", "heal", "ammo", "speed", "mystery" };

    public PositionGenerator posGenerator;

    void Start(){
        powerUps = new List<GameObject>[countOfPrefabedPowerUps];

        for (int i = 0; i < countOfPrefabedPowerUps; i++)
        {
            powerUps[i] = new List<GameObject>();
            tmp = (GameObject)Resources.Load(location + powerUpNames[i]);

            for (int j = 0; j < pooledAmount; j++)
            {
                GameObject obj = (GameObject)Instantiate(tmp);
                obj.transform.parent = gameObject.transform;
                obj.SetActive(false);
                powerUps[i].Add(obj);
            }
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
        var v = Enum.GetValues(typeof(PowerUpEnum));
        int i = UnityEngine.Random.Range(0, countOfPrefabedPowerUps);
        powerUpType = (PowerUpEnum)v.GetValue(i);

        for (int j = 0; j < pooledAmount; j++)
        {
            if (!powerUps[i][j].activeInHierarchy)
            {
                powerUps[i][j].transform.position = posGenerator.GenerateRandomPosition(1.0f, 1.0f);
                powerUps[i][j].SetActive(true);
                break;
            }
        }
    }
    
}
