using UnityEngine;
using System.Collections;

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
        clicksExist = wb.ammo;

        GameObject aiPrefab = (GameObject)Resources.Load("Prefabs/AiManagement");
        goSpecial = Instantiate(aiPrefab);
        special = goSpecial.transform.GetChild(0).GetComponent<Ai>();
        PlayerInfo specialInfo = new PlayerInfo();


        specialInfo.playerNumber = playerBase.playInfo.playerNumber;
        specialInfo.charEnum = playerBase.playInfo.charEnum;
        specialInfo.playerColor = playerBase.playInfo.playerColor;
        specialInfo.lifes = playerBase.playInfo.lifes;

        special.SetUpPlayer(specialInfo);

        special.weaponHandling.inventory.Remove(special.weaponHandling.weapons[WeaponEnum.specialTesla]);
    }
}
