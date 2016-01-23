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

    public FireProps(Vector2 direction, Vector2 position, RuntimeAnimatorController animController, float bulletSpeed, int damage, string weapon)
    {
        this.direction = direction;
        this.position = position;
        this.animController = animController;
        this.bulletSpeed = bulletSpeed;
        this.damage = damage;
        this.weapon = weapon;
    }
}
