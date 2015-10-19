using UnityEngine;
using System.Collections;

public class WeaponPistol : WeaponBase {
    
    public WeaponPistol()
    {
        base.damage = 10;
        base.weaponType = WeaponEnum.pistol;

        base.ammo = int.MaxValue;
        base.maxAmmo = int.MaxValue;

    }

    public WeaponPistol(CharacterEnum type)
    {
        switch (type)
        {
            case (CharacterEnum.Tesla): base.sprite = "Sprites/Weapons/TeslaPistol"; base.bulletSprite = "Sprites / Weapons / NobelPistol"; break;
            case (CharacterEnum.Nobel): base.sprite = "Sprites/Weapons/NobelPistol"; break;
            case (CharacterEnum.Einstein): base.sprite = "Sprites/Weapons/EinsteinPistol"; break;
        }
    }
}
