using UnityEngine;
using System.Collections;

public class WeaponSniper : WeaponBase
{

    public WeaponSniper()
    {
        base.damage = 10;
        base.weaponType = WeaponEnum.sniper;

        base.ammo = 5;
        base.maxAmmo = 5;
        

        base.reloadTime = 1f;
        base.ready = true;
        base.sprite = "Sprites/Weapons/sniper";
        base.bulletSprite = "Sprites/Bullets/bullet";

        kadency = 0.25f;
        kadTime = 0f;
        kadReady = true;

        loadSprites(sprite, bulletSprite);
    }

}
