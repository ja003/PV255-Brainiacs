using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AiPriorityLogic
{

    public AiBase aiBase;

    public int killPlayer1Priority;
    public int killPlayer2Priority;
    public int killPlayer3Priority;
    public int killPlayer4Priority;

    public int pickPowerUpPriority;
    public int pickWeaponPriority;

    public int avoidBulletPriority;

    public int standPriority;

    public int deathPriority;

    //logics
    public AiPowerUpLogic aiPowerUpLogic;
    public AiWeaponLogic aiWeaponLogic;
    public AiMovementLogic aiMovementLogic;
    public AiAvoidBulletLogic aiAvoidBulletLogic;
    public AiActionLogic aiActionLogic;
    public AiMapLogic aiMapLogic;

    public AiPriorityLogic(AiBase aiBase)
    {
        this.aiBase = aiBase;
    }

    public void PrintPriorities()
    {
        string message = "priorities \n";
        message += "standPriority=" + standPriority + ",\n";
        message += "pickWeaponPriority=" + pickWeaponPriority + ",\n";
        message += "pickPowerUpPriority=" + pickPowerUpPriority + ",\n";

        message += "Kill-1=" + killPlayer1Priority;
        message += ",Kill-2=" + killPlayer2Priority;
        message += ",Kill-3=" + killPlayer3Priority;
        message += ",Kill-4=" + killPlayer4Priority;
        message += ",avoidBulletPriority=" + avoidBulletPriority;

        //Debug.Log(message);
    }


    public void UpdatePriorities()
    {
        //set kill priorities only when you can shoot
        if (aiBase.hitPoints > 0)
            SetDeathPriority(0);

        if (aiWeaponLogic.CheckAmmo())
            SetKillPriorities();


        //register incoming bullets, powerups,...
        aiMapLogic.LookAroundYourself();

        SetPowerUpsPriority();

        SetWeaponsItemPriority();

        if (aiBase.aiAvoidBulletLogic.bulletIncoming)
        {
            //Debug.Log("avoiding");
            SetAvoidBulletPriority(100);
        }

        if (aiBase.hitPoints <= 0)
            SetDeathPriority(666);

        //debuggig - just stand
        //standPriority = 666;

        //check bullets
        //CheckAmmo(); //has to be updated faster


        //PrintPriorities();

        //....
    }

    public void SetDeathPriority(int priority)
    {
        deathPriority = priority;
        killPlayer1Priority = 0;
        killPlayer2Priority = 0;
        killPlayer3Priority = 0;
        killPlayer4Priority = 0;
    }

    public int GetCurrentHighestPriority()
    {
        /*
        int highestPriority = 0;
        if (killPlayer1Priority > highestPriority)
            highestPriority = killPlayer1Priority;
        if (killPlayer2Priority > highestPriority)
            highestPriority = killPlayer2Priority;
        if (killPlayer3Priority > highestPriority)
            highestPriority = killPlayer3Priority;
        if (killPlayer4Priority > highestPriority)
            highestPriority = killPlayer4Priority;
        if (avoidBulletPriority > highestPriority)
            highestPriority = avoidBulletPriority;
        if (aiBase.aiPowerUpLogic.pickPowerUpPriority > highestPriority)
            highestPriority = aiBase.aiPowerUpLogic.pickPowerUpPriority;
        if (deathPriority > highestPriority)
            highestPriority = deathPriority;

        if (standPriority > highestPriority)
            highestPriority = standPriority;
        //Debug.Log("highestPriority:" + highestPriority);
        if (highestPriority == 0)
            highestPriority++;
        return highestPriority;
        */
        return Mathf.Max(
            1,
            killPlayer1Priority,
            killPlayer2Priority,
            killPlayer3Priority,
            killPlayer4Priority,
            avoidBulletPriority,
            pickPowerUpPriority,
            pickWeaponPriority,
            deathPriority,
            standPriority
            );
    }

    /// <summary>
    /// nastaví kill priority podle zdálenosti
    /// 200 je cca max vzdálenost na mapě
    /// </summary>
    public void SetKillPriorities()
    {
        if (aiBase.player1 != null && aiBase.playerNumber != 1)
            killPlayer1Priority = (int)(90 * aiMapLogic.GetDistanceFactor(aiMapLogic.GetDistance(aiBase.gameObject, aiBase.player1.gameObject)));
        if (aiBase.player2 != null && aiBase.playerNumber != 2)
            killPlayer2Priority = (int)(90 * aiMapLogic.GetDistanceFactor(aiMapLogic.GetDistance(aiBase.gameObject, aiBase.player2.gameObject)));
        if (aiBase.player3 != null && aiBase.playerNumber != 3)
            killPlayer3Priority = (int)(90 * aiMapLogic.GetDistanceFactor(aiMapLogic.GetDistance(aiBase.gameObject, aiBase.player3.gameObject)));
        if (aiBase.player4 != null && aiBase.playerNumber != 4)
            killPlayer4Priority = (int)(90 * aiMapLogic.GetDistanceFactor(aiMapLogic.GetDistance(aiBase.gameObject, aiBase.player4.gameObject)));


    }

    //pickup weapons
    public void SetWeaponsItemPriority()
    {
        List<int> weaponsPriorities = new List<int>();

        if (aiWeaponLogic.itemWeapons.Count == 0)
        {
            //Debug.Log("no items around");
            pickWeaponPriority = 0;
            return;
        }

        int highestPriority = 0;
        foreach (GameObject weapon in aiWeaponLogic.itemWeapons)
        {
            //Debug.Log("I see " + powerUp.name);
            WeaponManager manager = weapon.GetComponent<WeaponManager>();
            float distanceFromMe = aiMapLogic.GetDistance(aiBase.gameObject, weapon);
            float distanceFactor = aiMapLogic.GetDistanceFactor(distanceFromMe);

            int priority = 0;
            switch (manager.type)
            {

                case WeaponEnum.flamethrower:
                    float flamethrowerFactor = distanceFactor;// * weaponHandling.activeWeapon.ammo / weaponHandling.activeWeapon.clip;
                    /*if (HasWeapon(WeaponEnum.flamethrower))
                    {
                        //flamethrowerFactor /= ...
                    }
                    */

                    priority = (int)flamethrowerFactor * 10;
                    break;
                /*
                                case WeaponEnum.shotgun:
                                    float shotgunFactor = distanceFactor;// * weaponHandling.activeWeapon.ammo / weaponHandling.activeWeapon.clip;
                                    /*if (HasWeapon(WeaponEnum.flamethrower))
                                    {
                                        //flamethrowerFactor /= ...
                                    }


                                    priority = (int)shotgunFactor * 10;
                                    break;
                */
                default:
                    priority = 0;
                    break;
            }
            if (priority == 0)
                priority = 10;
            weaponsPriorities.Add(priority);

            //Debug.Log("setting: " + powerUpsPriorities[powerUpsPriorities.Count - 1]);

            if (weaponsPriorities[weaponsPriorities.Count - 1] > highestPriority)
            {
                highestPriority = weaponsPriorities[weaponsPriorities.Count - 1];
            }
        }

        aiWeaponLogic.bestWeaponItem = aiWeaponLogic.itemWeapons[weaponsPriorities.IndexOf(highestPriority)];

        pickWeaponPriority = highestPriority;
        //Debug.Log("pickPowerUpPriority:" + pickPowerUpPriority);


    }
    

    //pickup powerups
    public void SetPowerUpsPriority()
    {
        List<int> powerUpsPriorities = new List<int>();

        if (aiPowerUpLogic.itemPowerUps.Count == 0)
        {
            //Debug.Log("no items around");
            pickPowerUpPriority = 0;
            return;
        }

        int highestPriority = 0;
        foreach (GameObject powerUp in aiPowerUpLogic.itemPowerUps)
        {
            //Debug.Log("I see " + powerUp.name);
            PowerUpManager manager = powerUp.GetComponent<PowerUpManager>();
            if (manager == null)
            {
                Debug.Log(powerUp + " has no manager");
                return;
            }

            float distanceFromMe = aiMapLogic.GetDistance(aiBase.gameObject, powerUp);
            float distanceFactor = aiMapLogic.GetDistanceFactor(distanceFromMe);

            int priority = 0;
            switch (manager.type)
            {

                case PowerUpEnum.Ammo:
                    float ammoFactor = distanceFactor * aiBase.weaponHandling.activeWeapon.ammo / aiBase.weaponHandling.activeWeapon.clip;
                    priority = (int)ammoFactor * 10;
                    break;
                case PowerUpEnum.Heal:
                    float healthFactor = distanceFactor * aiBase.hitPoints / aiBase.GetMaxHp();
                    priority = (int)healthFactor * 10;
                    break;
                case PowerUpEnum.Mystery:
                    int mysteryFactor = (int)distanceFactor * Random.Range(0, 80);
                    mysteryFactor += (int)distanceFactor * 25;
                    priority = mysteryFactor;
                    break;
                case PowerUpEnum.Shield:
                    float shieldFactor = distanceFactor * (50 + Random.Range(0, 20));
                    priority = (int)shieldFactor;
                    break;
                case PowerUpEnum.Speed:
                    float speedFactor = distanceFactor * (50 + Random.Range(0, 20));
                    priority = (int)speedFactor;
                    break;
            }
            if (priority == 0)
                priority = 10;
            powerUpsPriorities.Add(priority);

            //Debug.Log("setting: " + powerUpsPriorities[powerUpsPriorities.Count - 1]);

            if (powerUpsPriorities[powerUpsPriorities.Count - 1] > highestPriority)
            {
                highestPriority = powerUpsPriorities[powerUpsPriorities.Count - 1];
            }
        }

        aiPowerUpLogic.bestPowerUp = aiPowerUpLogic.itemPowerUps[powerUpsPriorities.IndexOf(highestPriority)];

        pickPowerUpPriority = highestPriority;
        //Debug.Log("pickPowerUpPriority:" + pickPowerUpPriority);


    }

    public void SetAvoidBulletPriority(int priority)
    {
        avoidBulletPriority = priority;

        killPlayer1Priority = 0;
        killPlayer2Priority = 0;
        killPlayer3Priority = 0;
        killPlayer4Priority = 0;
    }
}