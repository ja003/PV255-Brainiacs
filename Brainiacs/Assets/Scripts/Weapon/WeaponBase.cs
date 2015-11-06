using UnityEngine;
using System.Collections;


public class WeaponBase
{
    public WeaponEnum weaponType { get; set; }

    public int maxAmmo { get; set; }
    public int ammo { get; set; }
    public int clip { get; set; }
    public int maxClip { get; set; }
    public int damage { get; set; }
    public bool ready { get; set; }
    public float time { get; set; }

    public string sprite { get; set; }
    public string bulletSprite { get; set; }

    public float reloadTime { get; set; }

    public void reload()
    {
        time = 0;
        ammo = maxAmmo;
        clip = maxClip;
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

    public int fire()
    {
      
        ammo -= 1;  
        return ammo;
    }

}

