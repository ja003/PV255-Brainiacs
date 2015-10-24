using UnityEngine;
using System.Collections;

public class Skvrna : WeaponBase{

    public Skvrna() {
        base.ammo = 15;
        base.bulletSprite = "Sprites/Tesla_left";
        base.sprite = "Sprites/weaponTry_01";
        base.maxAmmo = 15;
        base.weaponType = WeaponEnum.shotgun;
    }
	
}
