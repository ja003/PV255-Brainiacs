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

    private float oldSpeed;

    private int hp = 100;
    private int exClicks;

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
	void Update () {
	
	}

    public void fire()
    {
        GameObject aiPrefab = (GameObject)Resources.Load("Prefabs/AiManagement");
        GameObject player1 = Instantiate(aiPrefab);
        Ai special = player1.transform.GetChild(0).GetComponent<Ai>();
        PlayerInfo specialInfo = new PlayerInfo();


        specialInfo.playerNumber = playerBase.playInfo.playerNumber;
        specialInfo.charEnum = playerBase.playInfo.charEnum;
        specialInfo.playerColor = "red";
        specialInfo.lifes = playerBase.playInfo.lifes;

        special.SetUpPlayer(specialInfo);
    }
}
