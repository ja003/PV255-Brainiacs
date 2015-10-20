using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1 : HumanBase
{

   
    void Start()
    {
        base.playerNumber = 1;


        // JP - farba spritu pre playerov
        //      mala by byt na konci nazvu spritu
        color = "blue";

        base.character = CharacterEnum.Tesla;

        base.playerName = "Player1";
        base.speed = 2f;
        base.direction = base.right;

        SetControlKeys(KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.LeftControl, KeyCode.LeftShift);

        base.rb2d = gameObject.GetComponent<Rigidbody2D>();

        base.pressedKeys = new List<KeyCode>();
        //base.pressedKeys.Add(KeyCode.X);
        //Debug.Log(base.rb2d.ToString());

   ////////////////////////////////////////////////////////// WEAPON HANDLING ////////////////////////////////////////////
        base.inventory = new List<WeaponBase>();
        WeaponBase pistol = new WeaponPistol(CharacterEnum.Tesla);
        base.inventory.Add(pistol);     
        base.activeWeapon = base.inventory[0];

        transform.Find("weapon").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(activeWeapon.sprite);


        //TRY
        WeaponBase skvrna = new Skvrna();
        inventory.Add(skvrna);


        /////////////////////////////////////////////////////////// END WH ///////////////////////////////////////////////////


    }
        void Update()
    {
        base.Movement();

        base.SwitchWeapon();
    }

   

}
