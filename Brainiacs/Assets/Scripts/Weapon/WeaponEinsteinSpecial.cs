using UnityEngine;
using System.Collections;

public class WeaponEinsteinSpecial : WeaponBase {

    public WeaponEinsteinSpecial()
    {
        base.weaponType = WeaponEnum.specialEinstein;
        isSpecial = true;

        base.damage = 100;

        base.ammo = 1;
        base.maxAmmo = 1;

        base.reloadTime = 60f;
        base.readyToFire = true;

        base.sprite = "Sprites/Special/einsteinSpecial";
        //base.bulletAnimControler = "Animations/bullets_animators/bullet_curie_animator";

        base.setUpSounds("einsteinSpecial");

        kadency = 3.0f;
        kadReady = true;

        bulletSpeed = 1f;

        loadSprites(sprite, bulletAnimControler);
    }

}
