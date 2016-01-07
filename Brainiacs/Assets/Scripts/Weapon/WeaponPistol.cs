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

        bulletSpeed = 1.1f;

        switch (type)
        {
            case (CharacterEnum.Tesla):
                base.sprite = "Sprites/Weapons/teslaPistol"; base.bulletAnimControler = "Animations/bullets_animators/bullet_tesla_animator";
                base.setUpSounds("teslaPistol");
                break;
            case (CharacterEnum.Nobel):
                base.sprite = "Sprites/Weapons/nobelPistol"; base.bulletAnimControler = "Animations/bullets_animators/bullet_nobel_animator";
                base.setUpSounds("nobelPistol");
                break;
            case (CharacterEnum.Einstein):
                base.sprite = "Sprites/Weapons/einsteinPistol"; base.bulletAnimControler = "Animations/bullets_animators/bullet_einstein_animator";
                base.setUpSounds("einsteinPistol");
                break;
            case (CharacterEnum.Curie):
                base.sprite = "Sprites/Weapons/curiePistol"; base.bulletAnimControler = "Animations/bullets_animators/bullet_curie_animator";
                base.setUpSounds("curiePistol");
                break;
            case (CharacterEnum.DaVinci):
                base.sprite = "Sprites/Weapons/davinciPistol"; base.bulletAnimControler = "Animations/bullets_animators/bullet_davinci_animator";
                base.setUpSounds("davinciPistol");
                break;
        }

        kadency = 0.1f;
        kadReady = true;
        //Debug.Log(sprite);
        //Debug.Log(bulletAnimControler);

        loadSprites(sprite, bulletAnimControler);
    }
}
