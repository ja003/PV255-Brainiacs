using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1 : PlayerBase {

   
    void Start()
    {
        //Debug.Log("!");

        base.character = CharacterEnum.Tesla;

        base.playerName = "Player1";
        base.speed = 2f;
        base.direction = base.right;

        SetControlKeys(KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftControl, KeyCode.LeftShift);

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

        base.SwitchWeapon();
    }

   

}
