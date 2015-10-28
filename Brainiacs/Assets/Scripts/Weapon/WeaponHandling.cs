using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponHandling : MonoBehaviour {

    public List<WeaponBase> inventory = new List<WeaponBase>();
    public WeaponBase activeWeapon { get; set; }
    public float lastFired { get; set; }
    public SpriteRenderer weaponRenderer;
    public BulletManager buletManager;

    

    public void SwitchWeapon()
    {

        if (inventory.Count == 1) return;


        activeWeapon = inventory[((inventory.IndexOf(activeWeapon) + 1) % inventory.Count)];
        //transform.Find("weapon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(activeWeapon.sprite);
        tranActiveWeapon();

    }

    public void fire(Vector2 direction)
    {
        
        int bulletsLeft = activeWeapon.fire();

        buletManager.fire(new Vector2(direction.x, direction.y), transform.position, activeWeapon.bulletSprite);

        if (bulletsLeft == 0)
        {
            WeaponBase toDel = activeWeapon;
            SwitchWeapon();
            inventory.Remove(toDel);
        }

    }

    public void tranActiveWeapon()
    {
        weaponRenderer.sprite = Resources.Load<Sprite>(activeWeapon.sprite);
    }


}
