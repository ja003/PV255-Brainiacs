using UnityEngine;
using System.Collections;

public class WeaponPistol : WeaponBase {
    
    public WeaponPistol()
    {
        base.damage = 1;
        base.weaponType = WeaponEnum.pistol;
        base.ammo = int.MaxValue;
        base.maxAmmo = 5;
    }
}
