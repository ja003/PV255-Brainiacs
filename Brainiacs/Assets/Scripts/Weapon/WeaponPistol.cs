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
            //ten bulletSprite se ještě nikde nevyužívá? -AJ
            case (CharacterEnum.Tesla): base.sprite = "Sprites/Weapons/weapon_Tesla-pistol"; base.bulletSprite = "Sprites / Weapons / NobelPistol"; break;
            case (CharacterEnum.Nobel): base.sprite = "Sprites/Weapons/weapon_Nobel-pistol"; break;
            case (CharacterEnum.Einstein): base.sprite = "Sprites/Weapons/weapon_Einstein-pistol"; break;
        }
    }
}
