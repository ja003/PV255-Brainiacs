using UnityEngine;
using System.Collections;

public class FireProps
{
    public Vector2 direction { get; set; }
    public Vector2 position { get; set; }
    public RuntimeAnimatorController animController { get; set; }
    public float bulletSpeed { get; set; }
    public int damage { get; set; }
    public string weapon { get; set; }
    public WeaponEnum weapEnum;

    public FireProps(Vector2 direction, Vector2 position, RuntimeAnimatorController animController, float bulletSpeed, int damage, string weapon, WeaponEnum weapEnum)
    {
        this.direction = direction;
        this.position = position;
        this.animController = animController;
        this.bulletSpeed = bulletSpeed;
        this.damage = damage;
        this.weapon = weapon;
        this.weapEnum = weapEnum;
    }

    public FireProps(FireProps fp)
    {
        this.direction = fp.direction;
        this.position = fp.position;
        this.animController = fp.animController;
        this.bulletSpeed = fp.bulletSpeed;
        this.damage = fp.damage;
        this.weapon = fp.weapon;
        this.weapEnum = fp.weapEnum;
    }
}
