using UnityEngine;
using System.Collections;

public class WeaponMP40 : WeaponBase {

    public WeaponMP40()
    {
        base.damage = 20;
        base.weaponType = WeaponEnum.MP40;

        base.ammo = 10;
        base.maxAmmo = 10;

        base.reloadTime = 2f;
        base.readyToFire = true;

        base.sprite = "Sprites/Weapons/MP40";
        base.bulletAnimControler = "Animations/bullets_animators/bullet_MP40_animator";
        base.setUpSounds("mp40");

        kadency = 0.05f;
        kadReady = true;

        bulletSpeed = 1.25f;

        loadSprites(sprite, bulletAnimControler);
    }
}
