using UnityEngine;
using System.Collections;

public class WeaponFlamethrower : WeaponBase
{
    public WeaponFlamethrower()
    {
        base.damage = 35;
        base.weaponType = WeaponEnum.sniper;

        base.ammo = 3;
        base.maxAmmo = 3;

        base.reloadTime = 4f;
        base.ready = true;

        base.sprite = "Sprites/Weapons/flamethrower";
        base.bulletAnimControler = "Sprites/Bullets/bullet";

        kadency = 0.3f;
        kadReady = true;

        loadSprites(sprite, bulletAnimControler);
    }
}
