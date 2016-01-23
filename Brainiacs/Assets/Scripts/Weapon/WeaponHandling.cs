using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponHandling : MonoBehaviour {

    public PlayerBase player;

    public List<WeaponBase> inventory = new List<WeaponBase>();
    public Dictionary<WeaponEnum, WeaponBase> weapons = new Dictionary<WeaponEnum, WeaponBase>();
    public WeaponSpecial specialWeapon;


    public WeaponBase activeWeapon { get; set; }
    public float lastFired { get; set; }
    public SpriteRenderer weaponRenderer;
    public BulletManager buletManager;



    public void FixedUpdate() {

        
        if(activeWeapon == null)
            Debug.Log("activeWeapon");
        if (activeWeapon.weaponSprites == null)
            Debug.Log("weaponSprites");
        
        if (activeWeapon.weaponSprites[player.directionMapping[player.direction]] == null)
            Debug.Log("direction");
        
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
    }

    public void SwitchWeapon()
    {

        if (inventory.Count == 1)
        {
            return;
        }

        activeWeapon = inventory[((inventory.IndexOf(activeWeapon) + 1) % inventory.Count)];
        tranActiveWeapon();

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

        
                SoundManager.instance.RandomizeSfx(activeWeapon.fireSound_01);
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
                weapon);

        int bulletsLeft = activeWeapon.fire(); 
        if (activeWeapon.isSpecial)
        {
            specialWeapon.fire(fireProps);
        }
        else
        { 
            buletManager.fire(
                new Vector2(direction.x, direction.y),
                transform.position,
                activeWeapon.animController,
                activeWeapon.bulletSpeed,
                activeWeapon.damage,
                weapon
                );
        }

        if (bulletsLeft == 0)
        {
            if (activeWeapon.weaponType == WeaponEnum.pistol)
            {
                activeWeapon.reload();
                activeWeapon.readyToFire = false;
            }
            else
            {
                WeaponBase toDel = activeWeapon;
                SwitchWeapon();
                inventory.Remove(toDel);
            }
        }

    }

    public void tranActiveWeapon()
    {
        weaponRenderer.sprite = activeWeapon.weaponSprites[player.directionMapping[player.direction]];
    }

}
