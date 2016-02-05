using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2 : HumanBase
{

    
    void Start()
    {
        base.playerNumber = 2;
        //createBullets();

        //base.character = CharacterEnum.Tesla;


        base.speed = 2f;
        base.direction = base.right;

        SetControlKeys(KeyCode.Keypad8, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad9);

        base.rb2d = gameObject.GetComponent<Rigidbody2D>();

        base.pressedKeys = new List<KeyCode>();
        base.inventory = new List<WeaponBase>();

        WeaponBase pistol = new WeaponPistol();
        base.inventory.Add(pistol);
        base.activeWeapon = base.inventory[0];
        

    }
    void Update()
    {
        base.Movement();
    }



}
