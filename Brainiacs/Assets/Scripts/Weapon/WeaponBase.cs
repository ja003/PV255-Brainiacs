using UnityEngine;
using System.Collections;


public class WeaponBase
{
    public WeaponEnum weaponType { get; set; }

    //unused
    public int maxClip { get; set; }
    public int clip { get; set; }
    //

    //AUDIO
    public AudioClip fireSound_01;
    
    public int maxAmmo { get; set; }
    public int ammo { get; set; }
    public int damage { get; set; }
    public bool ready { get; set; }
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

    public void reload()
    {
        time = 0;
        kadReady = true;
        ready = true;
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

    public void recycle() {
        reload();
    }

    protected void loadSprites(string sprt, string bullSprt)
    {
        //Debug.Log(sprt);
        //Debug.Log(bullSprt);
        weaponSprites = Resources.LoadAll<Sprite>(sprt);
        animController = Resources.Load(bullSprt) as RuntimeAnimatorController;
    }

    public void setUpSounds(string fireSound_01_string)
    {
        string soundLoaderString = "Sounds/Weapon/";

        fireSound_01 = Resources.Load(soundLoaderString + fireSound_01_string) as AudioClip;

    }
}

