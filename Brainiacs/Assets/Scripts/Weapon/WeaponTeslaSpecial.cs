using UnityEngine;
using System.Collections;

public class WeaponTeslaSpecial : WeaponBase
{

    public WeaponTeslaSpecial()
    {
        base.weaponType = WeaponEnum.specialTesla;
        isSpecial = true;

        base.damage = 100;

        base.ammo = 500;
        base.maxAmmo = 500;

        base.reloadTime = 3f;
        base.readyToFire = true;

        base.sprite = "Sprites/Special/einsteinSpecial";
        //base.bulletAnimControler = "Animations/bullets_animators/bullet_curie_animator";

        base.setUpSounds("sniper");

        kadency = 3.0f;
        kadReady = true;

        bulletSpeed = 1f;

        loadSprites(sprite, bulletAnimControler);
    }
}
