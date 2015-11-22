using UnityEngine;
using System.Collections;

public class WeaponPistol : WeaponBase {
    
    public WeaponPistol()
    {
       
    }

    public WeaponPistol(CharacterEnum type)
    {
        base.damage = 25;
        base.weaponType = WeaponEnum.pistol;

        base.ammo = 5;
        base.maxAmmo = 5;

        base.reloadTime = 2f;
        base.ready = true;

        switch (type)
        {
            case (CharacterEnum.Tesla): base.sprite = "Sprites/Weapons/teslaPistol"; base.bulletAnimControler = "Animations/bullet_tesla_animator"; break;
            case (CharacterEnum.Nobel): base.sprite = "Sprites/Weapons/nobelPistol"; break;
            case (CharacterEnum.Einstein): base.sprite = "Sprites/Weapons/einsteinPistol"; break;
            case (CharacterEnum.Curie): base.sprite = "Sprites/Weapons/curiePistol"; break;
        }

        kadency = 0.05f;
        kadReady = true;

        loadSprites(sprite, bulletAnimControler);
    }
}
