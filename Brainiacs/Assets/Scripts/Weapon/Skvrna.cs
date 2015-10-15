using UnityEngine;
using System.Collections;

public class Skvrna : WeaponBase{

    public Skvrna() {
        base.ammo = 15;
        base.bulletPrefab = "Prefabs/bullet";
        base.maxAmmo = 15;
        base.weaponType = WeaponEnum.shotgun;
    }
	
}
