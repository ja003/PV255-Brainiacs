using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponHandling : MonoBehaviour {

    public PlayerBase player;

    public List<WeaponBase> inventory = new List<WeaponBase>();
    public Dictionary<WeaponEnum, WeaponBase> weapons = new Dictionary<WeaponEnum, WeaponBase>();
    public WeaponSpecial specialWeapon;
    public WeaponFlamethrowerLogic flamethrower;

    public WeaponBase activeWeapon { get; set; }
    public float lastFired { get; set; }
    public SpriteRenderer weaponRenderer;
    public BulletManager buletManager;

    public bool fireKeyUp = true;
    public bool prevKeyUp;
    public bool switchedWeapon = false;

    public bool shootingEnabled = true;

    AudioClip addWeaponSound;

    void Start()
    {
        addWeaponSound = Resources.Load<AudioClip>("Sounds/Items/item_addweapon");
    }

    public void FixedUpdate() {

        if (activeWeapon.ammo <= 0 && activeWeapon.weaponType == WeaponEnum.flamethrower)
        {
            flamethrower.stopFire();
            RemoveFromInv();
        }
        

        try
        {
            weaponRenderer.sprite = activeWeapon.weaponSprites[player.directionMapping[player.direction]];
            
        }
        catch (Exception)
        {
            //Debug.Log(player);
            //Debug.Log(player.direction);
        }

        //weaponRenderer.sprite = activeWeapon.weaponSprites[player.directionMapping[player.direction]];
        
        foreach (var weap in inventory) {
            if (!weap.readyToFire) {
                if (weap.time >= weap.reloadTime)
                {
                    weap.readyToFire = true;
                }
                else
                {
                    if (weap.weaponType == WeaponEnum.specialDaVinci)
                    {
                        //Debug.Log("WeaponDaVinciSpecial reload");
                    }
                    weap.time += Time.deltaTime;
                }
            }
            if (!weap.kadReady)
            {
                if (weap.kadTime >= weap.kadency)
                {
                    weap.kadReady = true;
                    weap.kadTime = 0f;
                }
                else
                {
                    weap.kadTime += Time.deltaTime;
                }
            }
        }
        prevKeyUp = fireKeyUp;
    }

    public void fireKeyGotUp()
    {
        if (activeWeapon.weaponType == WeaponEnum.flamethrower)
        {
            flamethrower.stopFire();
        }

    }

    public void SwitchWeapon()
    {

        if (inventory.Count == 1)
        {
            return;
        }

        activeWeapon = inventory[((inventory.IndexOf(activeWeapon) + 1) % inventory.Count)];
        tranActiveWeapon();
        switchedWeapon = true;

    }

    public void AddWeapon(WeaponEnum we)
    {
        WeaponBase weapon = weapons[we];
        if (inventory.Contains(weapon))
        {
            inventory[inventory.IndexOf(weapon)].reload();
            activeWeapon = inventory[inventory.IndexOf(weapon)];
        }
        else
        {
            weapon.recycle();
            inventory.Add(weapon);
            activeWeapon = weapon;
        }
        SoundManager.instance.PlaySingle(addWeaponSound);
    }


    public void fire(Vector2 direction)
    {
        if (!shootingEnabled) return;
        if (!activeWeapon.readyToFire || !activeWeapon.kadReady) return;
        activeWeapon.kadReady = false;


        
        //handle if active weapon is pistol (character is not specified in WeaponEnum)
        string weapon;
        if (activeWeapon.ToString() == "pistol")
            weapon = player.playInfo.charEnum.ToString() + "Pistol";
        else
            weapon = activeWeapon.ToString();

        FireProps fireProps = new FireProps(
            new Vector2(direction.x, direction.y),
            transform.position,
            activeWeapon.animController,
            activeWeapon.bulletSpeed,
            activeWeapon.damage,
            weapon,
            activeWeapon.weaponType
            );

        int bulletsLeft = activeWeapon.ammo;

        if (bulletsLeft > 0)
        {
            SoundManager.instance.RandomizeSfx(activeWeapon.fireSound_01);

            if (activeWeapon.isSpecial)
            {
                specialWeapon.fire(fireProps, buletManager, this);
                bulletsLeft = activeWeapon.fire();
            }
            else
            {
                if (activeWeapon.weaponType == WeaponEnum.flamethrower)
                {
                    flamethrower.fire(fireProps, player, activeWeapon);
                }
                else
                {
                    buletManager.fire(fireProps);
                }
                bulletsLeft = activeWeapon.fire();
            }
        }
        if (bulletsLeft == 0) { 
            RemoveFromInv();
        }

    
    }

    private void RemoveFromInv()
    {
        if (activeWeapon.weaponType == WeaponEnum.pistol)
        {
            activeWeapon.reload();
            activeWeapon.readyToFire = false;
        }
        else if (activeWeapon.isSpecial)
        {
            switch (activeWeapon.weaponType)
            {
                case WeaponEnum.specialCurie:
                    activeWeapon.reload();
                    activeWeapon.readyToFire = false;
                    break;
                case WeaponEnum.specialEinstein:
                    activeWeapon.reload();
                    activeWeapon.readyToFire = false;
                    break;
                case WeaponEnum.specialDaVinci:
                    activeWeapon.reload();
                    activeWeapon.readyToFire = false;
                    break;
                case WeaponEnum.specialTesla:
                    activeWeapon.reload();
                    activeWeapon.readyToFire = false;
                    break;
            }
        }else if (activeWeapon.weaponType == WeaponEnum.flamethrower)
        {
            flamethrower.stopFire();
            WeaponBase toDel = activeWeapon;
            SwitchWeapon();
            inventory.Remove(toDel);
        }
        else
        {
            WeaponBase toDel = activeWeapon;
            SwitchWeapon();
            inventory.Remove(toDel);
        }
    }

    public void RemoveFromInventory(WeaponEnum weapon)
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if(inventory[i].weaponType == weapon)
                inventory.RemoveAt(i);
        }
    }

    public void RemoveFromInventoryAllBut(WeaponEnum weapon)
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (inventory[i].weaponType != weapon)
                inventory.RemoveAt(i);
        }
    }

    public string InventoryToString()
    {
        string s = "";
        foreach(WeaponBase w in inventory)
        {
            s += w + ", ";
        }
        return s;
    }

    public bool IsWeaponReady(WeaponEnum weapon)
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (inventory[i].weaponType == weapon && inventory[i].readyToFire)
                return true;
        }
        return false;
    }

    public void tranActiveWeapon()
    {
        weaponRenderer.sprite = activeWeapon.weaponSprites[player.directionMapping[player.direction]];
    }

}
