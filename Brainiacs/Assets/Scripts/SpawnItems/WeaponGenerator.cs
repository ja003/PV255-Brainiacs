using UnityEngine;
using System;
using System.Collections.Generic;

public class WeaponGenerator : MonoBehaviour
{
    public WeaponEnum weaponType;

    private GameObject tmp;
    private int pooledAmount = 5;               //count of each generated weapon
    private const int indexOfSpawnWeps = 0;     //WeaponEnum contains also nonspawnable weapons
    private const int countOfWeapons = 5;       //count of WeaponEnum items

    private List<GameObject>[] weapons;

    private float time = 0.0f;
    private float spawnInterval = 3.0f; //malý na test

    private string location = "Prefabs/SpawnItems/Weapons/weapon_";
    private string[] weaponNames = new string[countOfWeapons - indexOfSpawnWeps] { "flamethrower", "sniper", "biogun", "MP40", "mine" };

    public PositionGenerator posGenerator;
    
    void Start()
    {
        weapons = new List<GameObject>[countOfWeapons - indexOfSpawnWeps];

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = new List<GameObject>();
            tmp = (GameObject)Resources.Load(location + weaponNames[i]);
 
            for (int j = 0; j < pooledAmount; j++)
            {
                GameObject obj = (GameObject)Instantiate(tmp);
                obj.transform.parent = gameObject.transform;
                obj.SetActive(false);
                weapons[i].Add(obj);
            }
        }
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time > spawnInterval)
        {
            time = 0.0f;
            SpawnWeapon();
        }
    }

    public void SpawnWeapon()
    {
        var v = Enum.GetValues(typeof(WeaponEnum));
        int i = UnityEngine.Random.Range(indexOfSpawnWeps, countOfWeapons);
        weaponType = (WeaponEnum)v.GetValue(i);
        i -= indexOfSpawnWeps;

        for (int j = 0; j < pooledAmount; j++){
            if (!weapons[i][j].activeInHierarchy)
            {
                weapons[i][j].transform.position = posGenerator.GenerateRandomPosition();
                weapons[i][j].SetActive(true);
                break;
            }
        }
    }
}

