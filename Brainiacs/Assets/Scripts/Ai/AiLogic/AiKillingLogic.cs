using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiKillingLogic {

    public AiBase aiBase;

    public AiPowerUpLogic aiPowerUpLogic;
    public AiWeaponLogic aiWeaponLogic;
    public AiMovementLogic aiMovementLogic;
    public AiAvoidBulletLogic aiAvoidBulletLogic;
    public AiActionLogic aiActionLogic;
    public AiPriorityLogic aiPriorityLogic;
    public AiMapLogic aiMapLogic;
    //public AiKillingLogic aiKillingLogic;

    //public WeaponHandling weaponHandling;
    bool rdyToShoot;
    int frameToShoot;
    bool safeFromMine = true;
    Vector2 safeLocation = new Vector2(0, 0);

    GameObject targetObject;

    public AiKillingLogic(AiBase aiBase)
    {
        this.aiBase = aiBase;
        //weaponHandling = aiBase.weaponHandling;
        //Debug.Log(weaponHandling);
        rdyToShoot = false;
        frameToShoot = 15;
        targetObject = aiBase.gameObject;
        
        
    }


    public Vector2 GetBestShootSpot(Vector2 targetPosition)
    {
        Vector2 bestHorizontal = new Vector2(targetPosition.x, targetPosition.y);
        Vector2 bestVertical = new Vector2(targetPosition.x, targetPosition.y);

        Vector2 bestHorizontalUp = new Vector2(
            bestVertical.x, bestVertical.y + aiBase.characterColliderHeight);
        Vector2 bestHorizontalDown = new Vector2(
            bestVertical.x, bestVertical.y - aiBase.characterColliderHeight);

        Vector2 bestVerticalLeft = new Vector2(
            bestVertical.x - aiBase.characterColliderWidth, bestVertical.y);
        Vector2 bestVerticalRight = new Vector2(
            bestVertical.x + aiBase.characterColliderWidth, bestVertical.y);

        while (bestHorizontalDown.y > aiBase.mapMinY
            && !aiMovementLogic.CharacterCollidesBarrier(bestHorizontalDown)
            && !aiMovementLogic.CharacterCollidesMine(bestHorizontalDown)
            && bestHorizontalDown.y > aiBase.posY
            || aiMovementLogic.CharacterCollidesNotproofBarrier(bestHorizontalDown))
        {
            bestHorizontalDown.y -= 0.1f;
        }


        while (bestHorizontalUp.y < aiBase.mapMaxY
            && !aiMovementLogic.CharacterCollidesBarrier(bestHorizontalUp)
            && !aiMovementLogic.CharacterCollidesMine(bestHorizontalUp)
            && bestHorizontalUp.y < aiBase.posY
            || aiMovementLogic.CharacterCollidesNotproofBarrier(bestHorizontalUp))
        {
            bestHorizontalUp.y += 0.1f;
        }
        while (
            (bestVerticalLeft.x > aiBase.mapMinX
            && !aiMovementLogic.CharacterCollidesBarrier(bestVerticalLeft)
            && !aiMovementLogic.CharacterCollidesMine(bestVerticalLeft)
            && bestVerticalLeft.x > aiBase.posX)
            || aiMovementLogic.CharacterCollidesNotproofBarrier(bestVerticalLeft))
        {
            bestVerticalLeft.x -= 0.1f;
            //Debug.Log(bestVerticalLeft);
        }
        while (bestVerticalRight.x < aiBase.mapMaxX
            && !aiMovementLogic.CharacterCollidesBarrier(bestVerticalRight)
            && !aiMovementLogic.CharacterCollidesMine(bestVerticalRight)
            && bestVerticalRight.x < aiBase.posX
            || aiMovementLogic.CharacterCollidesNotproofBarrier(bestVerticalRight))
        {
            bestVerticalRight.x += 0.1f;
        }

        List<Vector2> possibleShootSpots = new List<Vector2>();
        possibleShootSpots.Add(bestHorizontalDown);
        possibleShootSpots.Add(bestHorizontalUp);
        possibleShootSpots.Add(bestVerticalLeft);
        possibleShootSpots.Add(bestVerticalRight);

        int spotIndex = 0;
        float distanceFromMe = 100; //big enough
        for (int i = 0; i < possibleShootSpots.Count; i++)
        {

            //Debug.Log(possibleShootSpots[i]);
            if (distanceFromMe > aiMapLogic.GetDistance(aiBase.gameObject.transform.position, possibleShootSpots[i]))
            {
                spotIndex = i;
                distanceFromMe = aiMapLogic.GetDistance(aiBase.gameObject.transform.position, possibleShootSpots[i]);
            }
        }

        //Debug.Log("move to: " + possibleShootSpots[spotIndex]);
        Debug.DrawRay(possibleShootSpots[spotIndex], aiBase.up, Color.green);

        return possibleShootSpots[spotIndex];
    }

    public void KillPlayer(PlayerBase player)
    {
        Vector2 targetPlayerPosition = player.transform.position;
        targetObject = player.gameObject;
        //targetPlayerPosition = targetObject.GetComponent<BoxCollider2D>().offset;
        //Debug.Log(targetPlayerPosition);

        Vector2 bestShootSpot = GetBestShootSpot(targetPlayerPosition);
        
        if (!safeFromMine)
        {
            safeFromMine = aiMovementLogic.MoveTo(safeLocation);
            //Debug.Log(safeFromMine);
        }
        else if (aiMovementLogic.MoveTo(bestShootSpot))
        {
            //Debug.Log("i can shoot");
            //look at him (if you are not already looking at him)

            if (aiMapLogic.GetObjectDirection(player.gameObject) != aiBase.direction)
                aiMapLogic.LookAt(player.gameObject);
            else
            {
                //UpdateAnimatorState(AnimatorStateEnum.stop);
            }

            if (aiWeaponLogic.CanShoot(aiBase.transform.position, aiBase.direction))
            {
                if(Time.frameCount % 30 == 0)
                {
                    rdyToShoot = false;
                }
                //Debug.Log(rdyToShoot);

                if (!rdyToShoot)
                {
                    //Debug.Log("!");
                    //pick best weapon
                    int bestWeaponIndex = 0;
                    int highestWeaponPriority = 0;

                    foreach (WeaponBase weapon in aiBase.weaponHandling.inventory)
                    {
                        if (GetWeaponPriority(weapon.weaponType) >= highestWeaponPriority)
                        {
                            highestWeaponPriority = GetWeaponPriority(weapon.weaponType);
                            bestWeaponIndex = aiBase.weaponHandling.inventory.IndexOf(weapon);
                        }
                    }
                    //Debug.Log("bestWeaponIndex: " + bestWeaponIndex);
                    //Debug.Log("highestWeaponPriority: " + highestWeaponPriority);

                    if (aiBase.weaponHandling.activeWeapon != aiBase.weaponHandling.inventory[bestWeaponIndex])
                    {
                        aiBase.weaponHandling.SwitchWeapon();
                    }
                    else
                    {
                        rdyToShoot = true;
                    }
                }
                
                //Debug.Log("I can shoot from:" + transform.position + " to: " + direction);
                //CheckAmmo();
                if(rdyToShoot && Time.frameCount > frameToShoot)
                {
                    Debug.Log(Time.frameCount);
                    //if you fire last bullet, wait a second, then you can shoot again
                    if (aiBase.weaponHandling.activeWeapon.ammo == 1)
                    {
                        frameToShoot = Time.frameCount + 30;
                        //Debug.Log("you can shoot again at " + frameToShoot);
                    }

                    aiBase.weaponHandling.fire(aiBase.direction);
                    //if you land mine, move 
                    if(safeFromMine && aiBase.weaponHandling.activeWeapon.weaponType == WeaponEnum.mine)
                    {
                        Debug.Log("MINE run");
                        frameToShoot = Time.frameCount + 30;
                        safeFromMine = false;
                        safeLocation.x = aiBase.posX;
                        safeLocation.y = aiBase.posY+1;
                    }
                    
                }
                
            }
        }


    }
    
    /*
    public void KillPlayer(PlayerBase player)
    {
        Vector2 targetPlayerPosition = player.transform.position;
        targetObject = player.gameObject;

        //move to same axis 
        //float targetX;
        //float targetY;

        Vector2 bestHorizontal = new Vector2(player.posX, player.posY);
        Vector2 bestVertical = new Vector2(player.posX, player.posY);

        Vector2 bestHorizontalUp = bestHorizontal;
        Vector2 bestHorizontalDown = bestHorizontal;

        Vector2 bestVerticalLeft = bestVertical;
        Vector2 bestVerticalRight = bestVertical;


        while (bestHorizontalDown.y > aiBase.mapMinY
            && !aiMovementLogic.CharacterCollidesBarrier(bestHorizontalDown)
            && !aiMovementLogic.CharacterCollidesMine(bestHorizontalDown)
            && bestHorizontalDown.y > aiBase.posY
            || aiMovementLogic.CharacterCollidesNotproofBarrier(bestHorizontalDown))
        {
            bestHorizontalDown.y -= 0.1f;
        }


        while (bestHorizontalUp.y < aiBase.mapMaxY
            && !aiMovementLogic.CharacterCollidesBarrier(bestHorizontalUp)
            && !aiMovementLogic.CharacterCollidesMine(bestHorizontalUp)
            && bestHorizontalUp.y < aiBase.posY
            || aiMovementLogic.CharacterCollidesNotproofBarrier(bestHorizontalUp))
        {
            bestHorizontalUp.y += 0.1f;
        }
        while (
            (bestVerticalLeft.x > aiBase.mapMinX
            && !aiMovementLogic.CharacterCollidesBarrier(bestVerticalLeft)
            && !aiMovementLogic.CharacterCollidesMine(bestVerticalLeft)
            && bestVerticalLeft.x > aiBase.posX)
            || aiMovementLogic.CharacterCollidesNotproofBarrier(bestVerticalLeft))
        {
            bestVerticalLeft.x -= 0.1f;
            //Debug.Log(bestVerticalLeft);
        }
        while (bestVerticalRight.x < aiBase.mapMaxX
            && !aiMovementLogic.CharacterCollidesBarrier(bestVerticalRight)
            && !aiMovementLogic.CharacterCollidesMine(bestVerticalRight)
            && bestVerticalRight.x < aiBase.posX
            || aiMovementLogic.CharacterCollidesNotproofBarrier(bestVerticalRight))
        {
            bestVerticalRight.x += 0.1f;
        }

        List<Vector2> possibleShootSpots = new List<Vector2>();
        possibleShootSpots.Add(bestHorizontalDown);
        possibleShootSpots.Add(bestHorizontalUp);
        possibleShootSpots.Add(bestVerticalLeft);
        possibleShootSpots.Add(bestVerticalRight);

        int spotIndex = 0;
        float distanceFromMe = 100; //big enough
        for (int i = 0; i < possibleShootSpots.Count; i++)
        {

            //Debug.Log(possibleShootSpots[i]);
            if (distanceFromMe > aiMapLogic.GetDistance(aiBase.gameObject.transform.position, possibleShootSpots[i]))
            {
                spotIndex = i;
                distanceFromMe = aiMapLogic.GetDistance(aiBase.gameObject.transform.position, possibleShootSpots[i]);
            }
        }

        //Debug.Log("move to: " + possibleShootSpots[spotIndex]);
        Debug.DrawRay(possibleShootSpots[spotIndex], aiBase.up, Color.green);
        //MoveTo(possibleShootSpots[spotIndex]);


        //if you are on same axis -> turn his direction and shoot

        //if (AlmostEqual(aiBase.posX, player.aiBase.posX, 0.1) || AlmostEqual(aiBase.posY, player.aiBase.posY, 0.1))

        if (!safeFromMine)
        {
            safeFromMine = aiMovementLogic.MoveTo(safeLocation);
            //Debug.Log(safeFromMine);
        }
        else if (aiMovementLogic.MoveTo(possibleShootSpots[spotIndex]))
        {
            //Debug.Log("i can shoot");
            //look at him (if you are not already looking at him)

            if (aiMapLogic.GetObjectDirection(player.gameObject) != aiBase.direction)
                aiMapLogic.LookAt(player.gameObject);
            else
            {
                //UpdateAnimatorState(AnimatorStateEnum.stop);
            }

            if (aiWeaponLogic.CanShoot(aiBase.transform.position, aiBase.direction))
            {
                if (Time.frameCount % 30 == 0)
                {
                    rdyToShoot = false;
                }
                //Debug.Log(rdyToShoot);

                if (!rdyToShoot)
                {
                    //Debug.Log("!");
                    //pick best weapon
                    int bestWeaponIndex = 0;
                    int highestWeaponPriority = 0;

                    foreach (WeaponBase weapon in aiBase.weaponHandling.inventory)
                    {
                        if (GetWeaponPriority(weapon.weaponType) >= highestWeaponPriority)
                        {
                            highestWeaponPriority = GetWeaponPriority(weapon.weaponType);
                            bestWeaponIndex = aiBase.weaponHandling.inventory.IndexOf(weapon);
                        }
                    }
                    //Debug.Log("bestWeaponIndex: " + bestWeaponIndex);
                    //Debug.Log("highestWeaponPriority: " + highestWeaponPriority);

                    if (aiBase.weaponHandling.activeWeapon != aiBase.weaponHandling.inventory[bestWeaponIndex])
                    {
                        aiBase.weaponHandling.SwitchWeapon();
                    }
                    else
                    {
                        rdyToShoot = true;
                    }
                }

                //Debug.Log("I can shoot from:" + transform.position + " to: " + direction);
                //CheckAmmo();
                if (rdyToShoot && Time.frameCount > frameToShoot)
                {
                    Debug.Log(Time.frameCount);
                    //if you fire last bullet, wait a second, then you can shoot again
                    if (aiBase.weaponHandling.activeWeapon.ammo == 1)
                    {
                        frameToShoot = Time.frameCount + 30;
                        //Debug.Log("you can shoot again at " + frameToShoot);
                    }

                    aiBase.weaponHandling.fire(aiBase.direction);
                    //if you land mine, move 
                    if (safeFromMine && aiBase.weaponHandling.activeWeapon.weaponType == WeaponEnum.mine)
                    {
                        Debug.Log("MINE run");
                        frameToShoot = Time.frameCount + 30;
                        safeFromMine = false;
                        safeLocation.x = aiBase.posX;
                        safeLocation.y = aiBase.posY + 1;
                    }

                }

            }
        }


    }
    */

    public int GetWeaponPriority(WeaponEnum weapon)
    {
        float distanceFromMe;

        switch (weapon)
        {
            case WeaponEnum.pistol:
                return 30;
            case WeaponEnum.sniper: //depends on distance to the taget
                distanceFromMe = aiMapLogic.GetDistance(aiBase.gameObject, targetObject);
                float factor = aiBase.mapWidth + 2 * distanceFromMe;
                return (int)factor;
            case WeaponEnum.MP40: //depends on distance to the taget
                distanceFromMe = aiMapLogic.GetDistance(aiBase.gameObject, targetObject);
                //Debug.Log(distanceFromMe);
                return 80 - (int)distanceFromMe;

            case WeaponEnum.flamethrower: //depends on distance to the taget
                distanceFromMe = aiMapLogic.GetDistance(aiBase.gameObject, targetObject);
                //Debug.Log(distanceFromMe);
                return 100 - 2*(int)distanceFromMe;
            case WeaponEnum.mine:
                return 80;
        }


        return 1;
    }


}
