using UnityEngine;
using System.Collections;

public class WeaponCurieSpecial : WeaponBase{

    public WeaponCurieSpecial()
    {
        base.weaponType = WeaponEnum.specialCurie;
        isSpecial = true;


        base.damage = 30;
        
        base.ammo = 300;
        base.maxAmmo = 300;

        base.reloadTime = 3f;
        base.readyToFire = true;

        base.sprite = "Sprites/Special/curieSpecial";
        base.bulletAnimControler = "Animations/bullets_animators/bullet_sniper_animator";

        base.setUpSounds("sniper");

        kadency = 0.0f;
        kadReady = true;

        bulletSpeed = 3f;

        loadSprites(sprite, bulletAnimControler);
    }

}
