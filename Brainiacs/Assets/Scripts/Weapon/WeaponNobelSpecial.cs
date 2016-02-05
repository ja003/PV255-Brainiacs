using UnityEngine;
using System.Collections;

public class WeaponNobelSpecial : WeaponBase
{
    public WeaponNobelSpecial()
    {
        base.weaponType = WeaponEnum.specialNobel;
        isSpecial = true;

        base.damage = 50;
        base.weaponType = WeaponEnum.mine;

        base.ammo = 5;
        base.maxAmmo = 5;

        base.reloadTime = 60f;
        base.readyToFire = true;

        base.sprite = "Sprites/Weapons/mine 1";
        base.bulletAnimControler = "Animations/bullets_animators/bullet_mine_animator";
        base.setUpSounds("mine");

        kadency = 0.5f;
        kadReady = true;

        bulletSpeed = 0f;

        loadSprites(sprite, bulletAnimControler);
    }
}
