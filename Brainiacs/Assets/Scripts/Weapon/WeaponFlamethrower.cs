using UnityEngine;
using System.Collections;

public class WeaponFlamethrower : WeaponBase
{
    public WeaponFlamethrower()
    {
        base.damage = 50;
        base.weaponType = WeaponEnum.flamethrower;

        base.ammo = 100;
        base.maxAmmo = 100;

        base.reloadTime = 2f;
        base.readyToFire = true;

        base.sprite = "Sprites/Weapons/flamethrower";
        base.bulletAnimControler = "Animations/bullets_animators/bullet_flamethrower";
        base.setUpSounds("mine");

        kadency = 0.0f;
        kadReady = true;

        bulletSpeed = 0f;

        loadSprites(sprite, bulletAnimControler);
    }
}
