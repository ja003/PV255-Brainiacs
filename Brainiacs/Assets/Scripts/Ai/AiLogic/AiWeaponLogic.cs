﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiWeaponLogic {
    public List<GameObject> itemWeapons;
    public AiBase aiBase;

    //logics
    public AiPowerUpLogic aiPowerUpLogic;
    //public AiWeaponLogic aiWeaponLogic;
    public AiMovementLogic aiMovementLogic;
    public AiAvoidBulletLogic aiAvoidBulletLogic;
    public AiPriorityLogic aiPriorityLogic;
    public AiMapLogic aiMapLogic;

    public AiWeaponLogic(AiBase aiBase)
    {
        this.aiBase = aiBase;
        itemWeapons = new List<GameObject>();
    }


    public void RegisterWeapons()
    {
        itemWeapons.Clear();
        Collider2D[] items = Physics2D.OverlapCircleAll(aiBase.transform.position, aiBase.mapWidth / 2, aiBase.itemMask);

        foreach (Collider2D item in items)
        {
            if (item.transform.tag == "Weapon" && item.gameObject.activeSelf)
            {
                itemWeapons.Add(item.gameObject);
            }
        }
    }
    public GameObject bestWeaponItem;
    

    public bool CheckAmmo()
    {
        bool hasAmmo = false;
        foreach(WeaponBase weapon in aiBase.weaponHandling.inventory)
        {
            if (aiBase.weaponHandling.IsWeaponReady(weapon.weaponType))
                hasAmmo = true;
        }
        
        if (!hasAmmo)
        {

            aiPriorityLogic.killPlayer1Priority -= 50;
            aiPriorityLogic.killPlayer2Priority -= 50;
            aiPriorityLogic.killPlayer3Priority -= 50;
            aiPriorityLogic.killPlayer4Priority -= 50;
        }

        return hasAmmo;
    }

    public bool CanShoot(Vector2 center, Vector2 direction)
    {

        Ray rayGun = new Ray(center, direction);

        float mapLenght = 15;
        RaycastHit2D[] hitGun = Physics2D.RaycastAll(rayGun.origin, direction, mapLenght);

        Debug.DrawRay(rayGun.origin, direction, Color.cyan);
        Debug.DrawRay(rayGun.origin, direction * -1, Color.red);

        if (hitGun.Length != 0)
        {
            //Borders have tag "Barrier" and it sometimes doesnt hit player first, but the border
            if (hitGun[0].transform.tag == "Barrier"
                && hitGun[0].transform.gameObject.layer != LayerMask.NameToLayer("Border"))
            {
                return false;
            }
        }
        return true;
    }
}
