﻿using UnityEngine;
using System.Collections;

public class WeaponFlamethrower : WeaponBase
{
    public WeaponFlamethrower()
    {
        base.damage = 35;
        base.weaponType = WeaponEnum.sniper;

        base.ammo = 3;
        base.maxAmmo = 3;

        base.reloadTime = 4f;
        base.readyToFire = true;

        base.sprite = "Sprites/Weapons/flamethrower";
        base.bulletAnimControler = "Animations/bullet_MP40_animator";

        kadency = 0.3f;
        kadReady = true;

        loadSprites(sprite, bulletAnimControler);
    }
}
