using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ai_03 : AiBase {
    
    void Start()
    {
        playerNumber = 3;
        killPlayer1Priority = 0;
        killPlayer2Priority = 0;
        killPlayer3Priority = 0;
        killPlayer4Priority = 0;

        

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        speed = 2f;

        currentAction = AiActionEnum.stand;

        inventory = new List<WeaponBase>();
        WeaponBase pistol = new WeaponPistol(CharacterEnum.Tesla);
        inventory.Add(pistol);
        WeaponBase special = new WeaponTeslaSpecial();
        inventory.Add(special);

        activeWeapon = inventory[0];

        //InitializeBullets();
    }
    



   
}
