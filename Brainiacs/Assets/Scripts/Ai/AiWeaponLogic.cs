using UnityEngine;
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
    public GameObject bestWeapon;
    public int pickWeaponPriority;
    

}
