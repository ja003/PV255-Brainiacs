using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponHandling : MonoBehaviour {

    public List<WeaponBase> inventory = new List<WeaponBase>();
    public WeaponBase activeWeapon { get; set; }
    public float lastFired { get; set; }
    public SpriteRenderer weaponRenderer;
    public BulletManager buletManager;


    public void FixedUpdate() {
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
        }
    }

    public void SwitchWeapon()
    {

        if (inventory.Count == 1) return;


        activeWeapon = inventory[((inventory.IndexOf(activeWeapon) + 1) % inventory.Count)];
        //transform.Find("weapon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(activeWeapon.sprite);
        tranActiveWeapon();

    }

    public void reload() {

    }

    public void fire(Vector2 direction)
    {
        if (!activeWeapon.ready) return; 
        int bulletsLeft = activeWeapon.fire();
        Debug.Log(bulletsLeft);
        buletManager.fire(new Vector2(direction.x, direction.y), transform.position, activeWeapon.bulletSprite);

        if (bulletsLeft == 0)
        {
            if (activeWeapon.weaponType == WeaponEnum.pistol)
            {
                activeWeapon.reload();
                activeWeapon.ready = false;
                Debug.Log(activeWeapon.ready);
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
        weaponRenderer.sprite = Resources.Load<Sprite>(activeWeapon.sprite);
    }


}
