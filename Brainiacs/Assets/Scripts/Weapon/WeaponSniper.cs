using UnityEngine;
using System.Collections;

public class WeaponSniper : WeaponBase
{

    public WeaponSniper()
    {
        base.damage = 50;
        base.weaponType = WeaponEnum.sniper;

        base.ammo = 5;  // 2->5 na test
        base.maxAmmo = 5;
        
        base.reloadTime = 3f;
        base.ready = true;

        base.sprite = "Sprites/Weapons/sniper";
        base.bulletAnimControler = "Sprites/Bullets/bullet";

        kadency = 0.25f;
        kadReady = true;

        loadSprites(sprite, bulletAnimControler);
    }

}
