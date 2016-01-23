using UnityEngine;
using System.Collections;

public class WeaponEinsteinSpecial : WeaponBase {

    public WeaponEinsteinSpecial()
    {
        base.weaponType = WeaponEnum.specialCurie;
        isSpecial = true;

        base.damage = 100;

        base.ammo = 300;
        base.maxAmmo = 300;

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
