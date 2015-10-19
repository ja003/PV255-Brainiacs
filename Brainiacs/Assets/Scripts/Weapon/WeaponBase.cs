using UnityEngine;
using System.Collections;

public class WeaponBase
{
    public WeaponEnum weaponType { get; set; }

    public int maxAmmo { get; set; }
    public int ammo { get; set; }

    public int damage { get; set; }
  
    public string sprite { get; set; }
    public string bulletSprite { get; set; }

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

    int shoot()
    {
        if (weaponType == WeaponEnum.pistol) return 10;
        ammo -= 1;
        return ammo;
    }

}

