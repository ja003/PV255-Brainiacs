using UnityEngine;
using System.Collections;
using System;

public class WeaponSpecialTeslaLogic : MonoBehaviour {

    private BulletManager bulletManager;
    private PlayerBase playerBase;

    private SpriteRenderer renderer;
    private Animator animator;

    private FireProps fireProps;
    private bool update = false;

    private WeaponBase wb;
    private WeaponHandling wh;

    private Ai special;
    private GameObject goSpecial;

    private int clicksExist;

    public void SetUpVariables(PlayerBase pb, BulletManager bm, WeaponBase wb)
    {
        this.wb = wb;
        playerBase = pb;
        bulletManager = bm;
        animator = GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	    if (update)
	    {
	        if (special.hitPoints <= 0)
	        {
	            update = false;
	            Destroy(goSpecial);
	        }
	        if (clicksExist <= 0)
	        {
	            update = false;
	            Destroy(goSpecial);
	        }
	        clicksExist--;
	    }
	}

    public void fire()
    {
        update = true;
        clicksExist = wb.existingClicks;

        GameObject aiPrefab = (GameObject)Resources.Load("Prefabs/AiManagement");
        goSpecial = Instantiate(aiPrefab);
        
        Vector2 newPosition;
        
        try{   
            newPosition = new Vector2(
                gameObject.transform.parent.GetComponentInChildren<Player>().posX + 1,
                gameObject.transform.parent.GetComponentInChildren<Player>().posY);
        }
        catch (Exception e)
        {
            newPosition = new Vector2(
                gameObject.transform.parent.GetComponentInChildren<Ai>().posX + 1,
                gameObject.transform.parent.GetComponentInChildren<Ai>().posY);

        }

        goSpecial.transform.position = newPosition;

        goSpecial.name = "Tesla CLONE [" + playerBase.playerNumber + "]";
        special = goSpecial.transform.GetChild(0).GetComponent<Ai>();
        special.isClone = true;
        PlayerInfo specialInfo = new PlayerInfo();


        specialInfo.playerNumber = playerBase.playInfo.playerNumber;
        specialInfo.charEnum = playerBase.playInfo.charEnum;
        specialInfo.playerColor = playerBase.playInfo.playerColor;
        specialInfo.lifes = playerBase.playInfo.lifes;

        special.SetUpPlayer(specialInfo);
        special.hitPoints = 70;

        //special.weaponHandling.inventory.Remove(special.weaponHandling.weapons[WeaponEnum.specialTesla]);

        //suposing pistol is on position 0 in inventory
        special.weaponHandling.RemoveFromInventoryAllBut(WeaponEnum.pistol);
        special.weaponHandling.activeWeapon = special.weaponHandling.inventory[0];

        /*
        for (int i = special.weaponHandling.inventory.Count-1; i > 0;i--)
        {
            special.weaponHandling.inventory.RemoveAt(i);
        }*/

    }
}
