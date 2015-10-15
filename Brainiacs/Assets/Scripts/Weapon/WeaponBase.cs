using UnityEngine;
using System.Collections;

public class WeaponBase
{

    public int maxAmmo { get; set; }
    public int ammo { get; set; }
    public int charger { get; set; }
    public int damage { get; set; }
    public WeaponEnum weaponType { get; set; }
    public string sprite { get; set; }
    public string bulletPrefab { get; set;}

    public void reload()
    {
        ammo = maxAmmo;
    }

    public void reload(int numOfBullets)
    {
        if ((ammo + numOfBullets) >= maxAmmo)
        {
            ammo = maxAmmo;
        }
        else
        {
            ammo += numOfBullets;
        }
    }

    int shot()
    {
        if (weaponType == WeaponEnum.pistol) return 10;
        ammo -= 1;
        return ammo;
    }

}

