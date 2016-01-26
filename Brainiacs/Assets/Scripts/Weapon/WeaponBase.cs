using UnityEngine;
using System.Collections;


public class WeaponBase
{

    public WeaponEnum weaponType { get; set; }
    public bool isSpecial { get; set; }

    // 
    public int clip;

    //AUDIO
    public AudioClip fireSound_01;
    
    // ammo
    public int maxAmmo { get; set; }
    public int ammo { get; set; }

    public int existingClicks;
   
    public int damage { get; set; }
    public bool readyToFire { get; set; }

    public float time { get; set; }
    public float kadTime { get; set; }

    public bool kadReady { get; set; }
    public float kadency { get; set; }

    // 1 min 3 max
    public float bulletSpeed { get; set; }

    public string sprite { get; set; }
    public Sprite[] weaponSprites;

    public string bulletAnimControler { get; set; }
    public RuntimeAnimatorController animController;
    public float reloadTime { get; set; }
    public bool shouldBeReloaded { get; set; }

    public void reload()
    {
        time = 0;
        kadReady = true;
        readyToFire = true;
        kadTime = 0;
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

    public int fireSpecial()
    {
        Debug.Log("fire");
        ammo -= 1;
        return ammo;
    }

    public void recycle() {
        reload();
    }

    protected void loadSprites(string sprt, string bullSprt)
    {
        weaponSprites = Resources.LoadAll<Sprite>(sprt);
        animController = Resources.Load(bullSprt) as RuntimeAnimatorController;
    }

    public void setUpSounds(string fireSound_01_string)
    {
        string soundLoaderString = "Sounds/Weapon/";

        fireSound_01 = Resources.Load(soundLoaderString + fireSound_01_string) as AudioClip;

    }

    public override string ToString()
    {
        return weaponType.ToString();
    }
}

