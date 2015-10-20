using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using Brainiacs.Generate;

//--MG
public class PowerUp : BonusItemSpawnBase
{
    public PowerUpEnum powerUpType;

    public PowerUp()
    {
        prefab = (GameObject)Resources.Load("Prefabs/Ammo");
        prefab.gameObject.SetActive(true);
        SetReady();
        //GameObject obj = (GameObject)Instantiate(prefab);...nelze, není monobehaviour
    }

    override public void SetReady()
    {
        System.Random rnd = new System.Random();
        var v = Enum.GetValues(typeof(PowerUpEnum));
        powerUpType = (PowerUpEnum)v.GetValue(rnd.Next(v.Length));

        switch (powerUpType)
        {
            case PowerUpEnum.shield:
                prefab = (GameObject)Resources.Load("Prefabs/Shield");
                //Debug.Log("Shield is generated");
                break;
            case PowerUpEnum.heal:
                prefab = (GameObject)Resources.Load("Prefabs/Heal");
                //Debug.Log("Heal is generated");
                break;
            case PowerUpEnum.ammo:
                prefab = (GameObject)Resources.Load("Prefabs/Ammo");
                //Debug.Log("Ammo is generated");
                break;
            default:
                //Debug.Log("Unknown powerUp is ready.");
                break;
        }
        
        PositionGenerator generator = new PositionGenerator();
        prefab.transform.position = generator.GenerateRandomPosition();
        //Debug.Log("X: " + prefab.transform.position.x + " Y: " + prefab.transform.position.y);
        prefab.gameObject.SetActive(false);
    }
    
}
