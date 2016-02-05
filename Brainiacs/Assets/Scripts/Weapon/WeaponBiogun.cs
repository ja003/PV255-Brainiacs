using UnityEngine;
using System.Collections;

public class WeaponBiogun : WeaponBase
{

    public WeaponBiogun()
    {
        base.damage = 35;
        base.weaponType = WeaponEnum.biogun;

        base.ammo = 4;
        base.maxAmmo = 4;

        base.reloadTime = 1f; //2,5 -> 1 for test
        base.readyToFire = true;

        base.sprite = "Sprites/Weapons/biogun";
        base.bulletAnimControler = "Animations/bullets_animators/bullet_biogun_animator";

        base.setUpSounds("biogun");

        kadency = 0.1f;
        kadReady = true;

        bulletSpeed = 1f;

        loadSprites(sprite, bulletAnimControler);
    }

}
