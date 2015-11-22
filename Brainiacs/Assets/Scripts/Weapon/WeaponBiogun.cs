using UnityEngine;
using System.Collections;

public class WeaponBiogun : WeaponBase
{

    public WeaponBiogun()
    {
        base.damage = 35;
        base.weaponType = WeaponEnum.sniper;

        base.ammo = 4;
        base.maxAmmo = 4;

        base.reloadTime = 1f; //2,5 -> 1 for test
        base.ready = true;

        base.sprite = "Sprites/Weapons/biogun";
        base.bulletAnimControler = "Animations/bullets_animators/bullet_biogun_animator";

        kadency = 0.1f;
        kadReady = true;

        loadSprites(sprite, bulletAnimControler);
    }

}
