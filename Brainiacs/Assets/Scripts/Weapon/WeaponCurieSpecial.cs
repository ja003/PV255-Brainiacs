using UnityEngine;
using System.Collections;

public class WeaponCurieSpecial : WeaponBase{

    public WeaponCurieSpecial()
    {
        base.weaponType = WeaponEnum.specialCurie;
        isSpecial = true;


        base.damage = 85;
        
        base.ammo = 1;
        base.maxAmmo = 1;

        base.reloadTime = 60f;
        base.readyToFire = true;

        base.sprite = "Sprites/Special/curieSpecial";
        base.bulletAnimControler = "Animations/bullets_animators/bullet_curie_animator";

        base.setUpSounds("sniper");

        kadency = 3.0f;
        kadReady = true;

        bulletSpeed = 1f;

        loadSprites(sprite, bulletAnimControler);
    }

}
