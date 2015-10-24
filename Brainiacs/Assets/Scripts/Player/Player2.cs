using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player2 : HumanBase
{


    void Start()
    {
        base.playerNumber = 2;

        base.character = CharacterEnum.Tesla;

        base.playerName = "Player1";
        base.speed = 2f;
        base.direction = base.right;

        SetControlKeys(KeyCode.Keypad8, KeyCode.Keypad4, KeyCode.Keypad5, KeyCode.Keypad6, KeyCode.Keypad7, KeyCode.Keypad9);

        base.rb2d = gameObject.GetComponent<Rigidbody2D>();

        base.pressedKeys = new List<KeyCode>();
        //base.pressedKeys.Add(KeyCode.X);
        //Debug.Log(base.rb2d.ToString());
        base.inventory = new List<WeaponBase>();

        WeaponBase pistol = new WeaponPistol();
        base.inventory.Add(pistol);
        switch (base.character)
        {
            case CharacterEnum.Tesla:
                WeaponBase teslaSpecial = new WeaponTeslaSpecial();
                base.inventory.Add(teslaSpecial);
                break;

            default:
                WeaponBase defaultSpecial = new WeaponTeslaSpecial();
                base.inventory.Add(defaultSpecial);
                break;
        }

        base.activeWeapon = base.inventory[0];
        //Debug.Log(base.inventory[0]);


    }
    void Update()
    {
        base.Movement();

        //base.SwitchWeapon();
    }



}
