using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HUDBase : MonoBehaviour {

    protected PlayerBase player;
    protected WeaponHandling weaponHandling;
    protected SpriteRenderer renderer;
    protected SpriteRenderer iconRenderer;
    protected string sprite;
    private string color;
    private Colors playerColor;

    protected GameObject avatar;

    private TextDisplay textDisplay;

    //private Text name;
    //private Text hp;
    //private Text ammo;

    public int health;
    public int ammoActiveWeapon;

    bool ready = false;

    private GameInfo gameInfo;

    public void Start()
    {
        try
        {
            gameInfo = GameObject.Find("GameInfo").GetComponent<GameInfo>();
        }
        catch (Exception e)
        {

        }
        if (gameInfo == null)
        {
            GameObject gameInfoObj = new GameObject("GameInfo");
            UnityEngine.Object.DontDestroyOnLoad(gameInfoObj);

            gameInfoObj.AddComponent<GameInfo>();
            gameInfo = gameInfoObj.GetComponent<GameInfo>();
        }

        textDisplay = new TextDisplay();
        textDisplay.InitializeGameVariables(); 
        //není to uplně košer, protože HUDBase je přiřazeno 4 objektům, takže se to volá zbytečně vícekrát, ale...

    }

    public void SetUp(PlayerBase p) {
        player = p;
        weaponHandling = p.weaponHandling;
        color = p.playInfo.playerColor;
        playerColor = (Colors)Enum.Parse(typeof(Colors), UppercaseFirst(color));

        sprite = p.playInfo.charEnum.ToString() + "_portrait";
        sprite = sprite.ToLower();
        SetupAvatar(sprite);
        ready = true;

        //name = GameObject.Find("name_"+color).GetComponent<Text>();
        //hp = GameObject.Find("hp_" + color).GetComponent<Text>();
        //ammo = GameObject.Find("ammo_" + color).GetComponent<Text>();

        //name.text = p.playInfo.playerName;

        iconRenderer = transform.Find("weaponI_" + p.playInfo.playerColor).gameObject.GetComponent<SpriteRenderer>();
        iconRenderer.sprite = Resources.Load<Sprite>("Sprites/HUD/" + "icon_" + p.playInfo.charEnum.ToString().ToLower() + "Pistol");

    }

	void SetupAvatar (string sprite) {
        avatar = transform.Find("Avatar_" + color).gameObject;
        renderer = avatar.GetComponent<SpriteRenderer>();
	    renderer.sprite = Resources.Load<Sprite>("Sprites/HUD/" + sprite);
    }
	
	// Update is called once per frame
	void Update () {
	    if (ready)
	    {
            if (weaponHandling.activeWeapon.readyToFire)
	        {
	            textDisplay.SetClipValue(playerColor, weaponHandling.activeWeapon.ammo);
	        }
	        else
	        {
	            string temp = (weaponHandling.activeWeapon.reloadTime - weaponHandling.activeWeapon.time).ToString();

	            float reloadingProcess = weaponHandling.activeWeapon.reloadTime - weaponHandling.activeWeapon.time;
                int reloadinProcessInt = (int)(reloadingProcess);
                if (weaponHandling.activeWeapon.weaponType == WeaponEnum.pistol)
                {
                    reloadinProcessInt = (int)(reloadingProcess * 10);
                }

	            if (reloadinProcessInt < 100)
	            {
	                textDisplay.SetClipValue(playerColor, reloadinProcessInt);

	            }

	            int l = temp.Length;

	            if (l <= 3)
	            {
	                //ammo.text = temp;
	                //textDisplay.SetClipValue(playerColor, 10);
	            }
	            else
	            {
	                //ammo.text = temp.Substring(0, 3);
	                //textDisplay.SetClipValue(playerColor, 20);

	            }
	        }

	        if (weaponHandling.switchedWeapon)
	        {

	            if (weaponHandling.activeWeapon.weaponType == WeaponEnum.pistol)
	            {
	                iconRenderer.sprite =
	                    Resources.Load<Sprite>("Sprites/HUD/" + "icon_" + player.playInfo.charEnum.ToString().ToLower() + "Pistol");
	            }
	            else if (weaponHandling.activeWeapon.isSpecial)
	            {
	                iconRenderer.sprite =
	                    Resources.Load<Sprite>("Sprites/HUD/" + "icon_" + player.playInfo.charEnum.ToString().ToLower() + "Special");
	            }
	            else
	            {
	                iconRenderer.sprite =
	                    Resources.Load<Sprite>("Sprites/HUD/" + "icon_" + weaponHandling.activeWeapon.weaponType.ToString());
	            }
	        }


	        //hp.text = player.hitPoints.ToString();
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
