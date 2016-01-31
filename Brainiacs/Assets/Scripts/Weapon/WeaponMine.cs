using UnityEngine;
using System.Collections;

public class WeaponMine : WeaponBase
{
    public WeaponMine()
    {
        base.damage = 50;
        base.weaponType = WeaponEnum.mine;

        base.ammo = 5;
        base.maxAmmo = 5;

        base.reloadTime = 2f;
        base.readyToFire = true;

        base.sprite = "Sprites/Weapons/mine 1";
        base.bulletAnimControler = "Animations/bullets_animators/bullet_mine_animator";
        base.setUpSounds("mine");

        kadency = 1.0f;
        kadReady = true;

        bulletSpeed = 0f;

        loadSprites(sprite, bulletAnimControler);
    }
}
