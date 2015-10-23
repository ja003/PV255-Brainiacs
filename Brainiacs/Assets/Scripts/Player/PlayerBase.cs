﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Brainiacs.Generate;

public abstract class PlayerBase : MonoBehaviour
{
    public CharacterEnum character;

    public int playerNumber { get; set; }

    // JP - farba spritu
    public string color { get; set; }
    // JP added
    public int ID { get; set; }
    public float posX;
    public float posY;

    public string playerName { get; set; }

    public float speed { get; set; }

    public List<WeaponBase> inventory { get; set; }

    public WeaponBase activeWeapon { get; set; }

    //--MG added
    private static int maxHP = 100;
    public int hitPoints = maxHP;
    private bool isShielded = false;
    
    private bool speedBuffIsActive = false;
    private float time = 0.0f;                  //for speed/slow
    private float speedAmount;                  //to remember if it is boost of slow
    private float speedBuffDuration = 10.0f;

    public Vector2 direction;

    protected Vector2 up = Vector2.up;
    protected Vector2 down = Vector2.down;
    protected Vector2 left = Vector2.left;
    protected Vector2 right = Vector2.right;
    protected Vector2 stop = Vector2.zero;

   

    //rigid body of controlled object
    public Rigidbody2D rb2d { get; set; }

    //TODO: transform.GetChild(1).GetComponent<SpriteRenderer>().sortingLayerName = "Hand_back";
    //až přídáme ruku
    /// <summary>
    /// změní pořádí vykreslování částí těla
    /// left,down,right: player-weapon-hand "weapon_front" + "hand_front"
    /// up: hand-weapon-player "weapon_back" + "hand_back"
    /// </summary>
    public void SortLayers()
    {
        if (direction == up)
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Weapon_back";
        }
        else
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Weapon_front";
        }
    }
    
    //PowerUp and HP management - <<<MG...>>>
    public void ApplyDamage(int dmg)
    {
        if (isShielded)
        {
            isShielded = false;
        }
        else
        {
            if ((hitPoints - dmg) <= 0)
            {
                hitPoints = 0;
                //TODO sputenie animacie smrti
                rb2d.velocity = stop;
                //TODO delay nejake 2s

                //presun na novu poziciu
                var generator = new PositionGenerator();
                Vector3 newRandomPosition = generator.GenerateRandomPosition();
                posX = newRandomPosition.x;
                posY = newRandomPosition.y;
                transform.position = newRandomPosition;
                Debug.Log("X " + newRandomPosition.x);
                Debug.Log("Y " + newRandomPosition.y);
                hitPoints = maxHP;
            }
            else
            {
                hitPoints -= dmg;
            }
        }
        Debug.Log(hitPoints);
    }

    private void ApplyHeal(int heal)
    {
        if ((hitPoints + heal) > maxHP)
        {
            hitPoints = maxHP;
        }
        else
        {
            hitPoints += heal;
        }

    }

    void FixedUpdate()
    {
        if (speedBuffIsActive)
        {
            time += Time.deltaTime;
            if (time > speedBuffDuration)
            {
                speedBuffIsActive = false;
                time = 0.0f;
                speed -= speedAmount;
            }
        }
    }

    private void ApplySpeedOrSlow(float amount)
    {
        speedAmount = amount;
        speedBuffIsActive = true;
        speed += speedAmount;
    }

    public void AddPowerUp(PowerUpEnum en)
    {
        switch (en)
        {
            case PowerUpEnum.Shield:
                isShielded = true;
                break;
            case PowerUpEnum.Heal:
                ApplyHeal(maxHP / 2);
                break;
            case PowerUpEnum.Ammo:
                activeWeapon.reload(100);
                break;
            case PowerUpEnum.Speed:
                ApplySpeedOrSlow(1.5f);
                break;
            case PowerUpEnum.Mystery:
                System.Random rnd = new System.Random();
                var v = Enum.GetValues(typeof(PowerUpEnum));
                AddPowerUp((PowerUpEnum)v.GetValue(rnd.Next(v.Length)));
                break;
            case PowerUpEnum.dealDamage:
                ApplyDamage(maxHP / 3);
                break;
            case PowerUpEnum.slowSpeed:
                ApplySpeedOrSlow(-1.0f);
                break;
            default:
                Debug.Log("Unknown powerUp received.");
                break;
        }
    }
    //<<<...MG>>>


}