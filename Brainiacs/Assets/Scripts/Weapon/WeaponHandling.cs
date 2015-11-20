using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponHandling : MonoBehaviour {

    public PlayerBase player;

    public List<WeaponBase> inventory = new List<WeaponBase>();
    public Dictionary<WeaponEnum, WeaponBase> weapons = new Dictionary<WeaponEnum, WeaponBase>();

    public WeaponBase activeWeapon { get; set; }
    public float lastFired { get; set; }
    public SpriteRenderer weaponRenderer;
    public BulletManager buletManager;

    

    public void FixedUpdate() {

        weaponRenderer.sprite = activeWeapon.weaponSprites[player.directionMapping[player.direction]];

        foreach (var weap in inventory){
            if (!weap.ready) {
                if (weap.time >= weap.reloadTime)
                {
                    weap.ready = true;
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

    public void reload() {

    }

    public void fire(Vector2 direction)
    {
        if (!activeWeapon.ready || !activeWeapon.kadReady) return;
        activeWeapon.kadReady = false;
        int bulletsLeft = activeWeapon.fire();
        
        buletManager.fire(new Vector2(direction.x, direction.y), transform.position, activeWeapon.animController);

        if (bulletsLeft == 0)
        {
            if (activeWeapon.weaponType == WeaponEnum.pistol || activeWeapon.weaponType == WeaponEnum.sniper)
            {
                activeWeapon.reload();
                activeWeapon.ready = false;
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
