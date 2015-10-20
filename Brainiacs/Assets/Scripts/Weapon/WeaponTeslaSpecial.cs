public class WeaponTeslaSpecial : WeaponBase {

    public WeaponTeslaSpecial()
    {
        base.damage = 1;
        base.weaponType = WeaponEnum.pistol;
        base.ammo = 1000;
        base.maxAmmo = 5;

        //just to see the change 
        sprite = "Sprites/Weapons/weapon_Einstein-pistol";
    }
}
