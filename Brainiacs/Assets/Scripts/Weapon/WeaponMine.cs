using UnityEngine;
using System.Collections;

public class WeaponMine : WeaponBase
{
    public WeaponMine()
    {
        base.damage = 50;
        base.weaponType = WeaponEnum.mine;

        base.ammo = 1;
        base.maxAmmo = 1;

        base.reloadTime = 2f;
        base.ready = true;

        base.sprite = "Sprites/Weapons/MP40";
        base.bulletAnimControler = "Animations/bullets_animators/bullet_MP40_animator";
        base.setUpSounds("mine");

        kadency = 0.0f;
        kadReady = true;

        bulletSpeed = 0f;

        loadSprites(sprite, bulletAnimControler);
    }
}
