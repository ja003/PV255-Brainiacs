using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HUDBase : MonoBehaviour {

    protected PlayerBase player;
    protected WeaponHandling weaponHandling;
    protected SpriteRenderer renderer;
    protected string sprite;
    private string color;
    private Colors playerColor;

    protected GameObject avatar;

    private TextDisplay textDisplay;

    private Text name;
    private Text hp;
    private Text ammo;

    public int health;
    public int ammoActiveWeapon;

    bool ready = false;

    private GameInfo gameInfo;

    public void Start()
    {
        try
        {
            gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
            //Debug.Log(gameInfo);
        }
        catch (Exception e)
        {
            Debug.Log("HUDBase - NO GAME INFO OBJECT - setting default values");
        }
        if (gameInfo == null)
        {
            GameObject gameInfoObj = new GameObject("GameInfo");
            UnityEngine.Object.DontDestroyOnLoad(gameInfoObj);

            gameInfoObj.AddComponent<GameInfo>();
            gameInfo = gameInfoObj.GetComponent<GameInfo>();
        }

        //bool redActive = gameInfo.player1type != PlayerTypeEnum.None;
        //bool greenActive = gameInfo.player2type != PlayerTypeEnum.None;
        //bool blueActive = gameInfo.player3type != PlayerTypeEnum.None;
        //bool yellowActive = gameInfo.player4type != PlayerTypeEnum.None;

        textDisplay = new TextDisplay();
        textDisplay.InitializeGameVariables(); 
        //není to uplně košer, protože HUDBase je přiřazeno 4 objektům, takže se to volá zbytečně vícekrát, ale...

    }

    public void SetUp(PlayerBase p) {
        player = p;
        weaponHandling = p.weaponHandling;
        color = p.playInfo.playerColor;
        playerColor = (Colors)Enum.Parse(typeof(Colors), UppercaseFirst(color));

        sprite = p.playInfo.charEnum.ToString() + "_portail";
        sprite = sprite.ToLower();
        SetupAvatar(sprite);
        ready = true;

        name = GameObject.Find("name_"+color).GetComponent<Text>();
        hp = GameObject.Find("hp_" + color).GetComponent<Text>();
        ammo = GameObject.Find("ammo_" + color).GetComponent<Text>();

        name.text = p.playInfo.playerName;

        
        
    }

	void SetupAvatar (string sprite) {
        avatar = transform.Find("Avatar_" + color).gameObject;
        renderer = avatar.GetComponent<SpriteRenderer>();
        //Debug.Log(renderer.sprite);
	    renderer.sprite = Resources.Load<Sprite>("Sprites/HUD/" + sprite);
        //Debug.Log(renderer.sprite);
    }
	
	// Update is called once per frame
	void Update () {
	    if (ready)
	    {
	        if (weaponHandling.activeWeapon.readyToFire)
	        {
	            ammo.color = Color.black;
	            ammo.text = weaponHandling.activeWeapon.ammo.ToString();
                textDisplay.SetClipValue(playerColor, weaponHandling.activeWeapon.ammo);
	        }
	        else
	        {
	            ammo.color = Color.red;
	            string temp = (weaponHandling.activeWeapon.reloadTime - weaponHandling.activeWeapon.time).ToString();

                float reloadingProcess = weaponHandling.activeWeapon.reloadTime - weaponHandling.activeWeapon.time;
                //Debug.Log(reloadingProcess);
                int reloadinProcessInt = (int)(reloadingProcess * 10);
                //Debug.Log(reloadinProcessInt);

                if (reloadinProcessInt < 100)
                {
                    textDisplay.SetClipValue(playerColor, reloadinProcessInt);

                }

                int l = temp.Length;                

                if (l <= 3)
	            {
	                ammo.text = temp;
                    //textDisplay.SetClipValue(playerColor, 10);
                }
	            else
	            {
	                ammo.text = temp.Substring(0, 3);
                    //textDisplay.SetClipValue(playerColor, 20);

                }
            }
	        hp.text = player.hitPoints.ToString();
            textDisplay.SetHpValue(playerColor, player.hitPoints);
	    }
	}

    static string UppercaseFirst(string s)
    {
        // Check for empty string.
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }
        // Return char and concat substring.
        return char.ToUpper(s[0]) + s.Substring(1);
    }
}
