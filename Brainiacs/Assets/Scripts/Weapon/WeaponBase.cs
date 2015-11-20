using UnityEngine;
using System.Collections;


public class WeaponBase
{
    public WeaponEnum weaponType { get; set; }

    //unused
    public int maxClip { get; set; }
    public int clip { get; set; }
    //
    
    public int maxAmmo { get; set; }
    public int ammo { get; set; }
    public int damage { get; set; }
    public bool ready { get; set; }
    public float time { get; set; }
    public float kadTime { get; set; }
    public bool kadReady { get; set; }
    public float kadency { get; set; }

    public string sprite { get; set; }
    public Sprite[] weaponSprites;


    public string bulletAnimControler { get; set; }
    public RuntimeAnimatorController animController;
    public float reloadTime { get; set; }

    public void reload()
    {
        time = 0;
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

    public int fire()
    {
        ammo -= 1;  
        return ammo;
    }

    public void recycle() {
        reload();
        ready = true;
        time = 0.0f; 
    }

    protected void loadSprites(string sprt, string bullSprt)
    {
        
        weaponSprites = Resources.LoadAll<Sprite>(sprt);
        animController = Resources.Load(bullSprt) as RuntimeAnimatorController;
       
    }
}

