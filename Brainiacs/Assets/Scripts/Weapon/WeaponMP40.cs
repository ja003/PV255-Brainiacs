using UnityEngine;
using System.Collections;

public class WeaponMP40 : WeaponBase {

    public WeaponMP40()
    {
        base.damage = 30;
        base.weaponType = WeaponEnum.sniper;

        base.ammo = 10;
        base.maxAmmo = 10;

        base.reloadTime = 2f;
        base.ready = true;

        base.sprite = "Sprites/Weapons/MP40";
        base.bulletAnimControler = "Sprites/Bullets/bullet";

        kadency = 0.0f;
        kadReady = true;

        loadSprites(sprite, bulletAnimControler);
    }
}
