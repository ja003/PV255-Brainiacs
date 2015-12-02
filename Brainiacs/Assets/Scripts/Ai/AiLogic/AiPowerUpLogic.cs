using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiPowerUpLogic  {
    public GameObject bestPowerUp;
    
    public List<GameObject> itemPowerUps;
    public AiBase aiBase;
    public LayerMask itemMask;

    //logics
    //public AiPowerUpLogic aiPowerUpLogic;
    public AiWeaponLogic aiWeaponLogic;
    public AiMovementLogic aiMovementLogic;
    public AiAvoidBulletLogic aiAvoidBulletLogic;
    public AiPriorityLogic aiPriorityLogic;
    public AiMapLogic aiMapLogic;

    public AiPowerUpLogic(AiBase aiBase)
    {
        this.aiBase = aiBase;
        itemMask = aiBase.itemMask;
        itemPowerUps = new List<GameObject>();
        
    }
    public void PickUp(GameObject obj)
    {
        if (aiMovementLogic.MoveTo(obj.transform.position))
        {
            //Debug.Log("picked up");
            aiMapLogic.LookAroundYourself();
        }
    }

    public void RegisterPowerUps()
    {
        itemPowerUps.Clear();
        Collider2D[] items = Physics2D.OverlapCircleAll(aiBase.transform.position, aiBase.mapWidth / 2, itemMask);

        foreach (Collider2D item in items)
        {
            if (item.transform.tag == "PowerUp" && item.gameObject.activeSelf)
            {
                itemPowerUps.Add(item.gameObject);
            }
        }
    }

}
