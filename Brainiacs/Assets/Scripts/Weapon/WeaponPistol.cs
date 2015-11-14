using UnityEngine;
using System.Collections;

public class WeaponPistol : WeaponBase {
    
    public WeaponPistol()
    {
       
    }

    public WeaponPistol(CharacterEnum type)
    {
        base.damage = 10;
        base.weaponType = WeaponEnum.pistol;

        base.ammo = 10;
        base.maxAmmo = 10;

        base.clip = 5;
        base.maxClip = 5;

        base.reloadTime = 0.5f;
        base.ready = true;
        switch (type)
        {
            //ten bulletSprite se ještě nikde nevyužívá? -AJ
            case (CharacterEnum.Tesla): base.sprite = "Sprites/Weapons/weapon_Tesla-pistol"; base.bulletSprite = "Sprites/Electricity"; break;
            case (CharacterEnum.Nobel): base.sprite = "Sprites/Weapons/weapon_Nobel-pistol"; break;
            case (CharacterEnum.Einstein): base.sprite = "Sprites/Weapons/weapon_Einstein-pistol"; break;
        }
    }
}
