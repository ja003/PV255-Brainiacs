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
            Debug.Log(player.direction);
        }

        weaponRenderer.sprite = activeWeapon.weaponSprites[player.directionMapping[player.direction]];
        
        foreach (var weap in inventory) {
            if (!weap.readyToFire) {
                if (weap.time >= weap.reloadTime)
                {
                    weap.readyToFire = true;
                }
                else
                {
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
    }


    public void fire(Vector2 direction)
    {
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
                specialWeapon.fire(fireProps, buletManager);
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
                    break;
                case WeaponEnum.specialEinstein:
                    activeWeapon.reload();
                    break;
                case WeaponEnum.specialNobel:
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

    public void tranActiveWeapon()
    {
        weaponRenderer.sprite = activeWeapon.weaponSprites[player.directionMapping[player.direction]];
    }

}
