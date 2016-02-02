using UnityEngine;
using System.Collections;

public class WeaponDaVinciSpecial : WeaponBase
{

    public WeaponDaVinciSpecial()
    {
        base.weaponType = WeaponEnum.specialDaVinci;
        isSpecial = true;


        base.damage = 100;

        existingClicks = 300;

        base.ammo = 1;
        base.maxAmmo = 1;

        base.reloadTime = 60f;
        base.readyToFire = true;

        base.sprite = "Sprites/Special/davinciSpecial_plan";
        base.bulletAnimControler = "";

        base.setUpSounds("sniper");

        kadency = 0.0f;
        kadReady = true;

        bulletSpeed = 3f;

        loadSprites(sprite, bulletAnimControler);
    }
}
