using UnityEngine;
using System.Collections;

public class Skvrna : WeaponBase{

    public Skvrna() {
        base.ammo = 15;
        base.bulletSprite = "Prefabs/bullet";
        base.sprite = "Sprites/Weapons/bullet";
        base.maxAmmo = 15;
        base.weaponType = WeaponEnum.shotgun;
    }
	
}
