using UnityEngine;
using System.Collections;

public class HUDBase : MonoBehaviour {

    protected PlayerBase player;
    protected WeaponHandling weaponHandling;
    protected SpriteRenderer renderer;
    protected string sprite;

    protected GameObject avatar;

    public int health;
    public int ammoActiveWeapon;

    bool ready = false;

    public void SetUp(PlayerBase p) {
        player = p;
        weaponHandling = p.weaponHandling;
        Debug.Log(weaponHandling.activeWeapon.ammo);
        sprite = p.playInfo.charEnum.ToString() + "_Avatar";
        SetupAvatar();
        ready = true;
    }

	void SetupAvatar () {
        avatar = transform.Find("Avatar1").gameObject;
        renderer = avatar.GetComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load(sprite) as Sprite;
	}
	
	// Update is called once per frame
	void Update () {
        if (ready)
        {
            ammoActiveWeapon = weaponHandling.activeWeapon.ammo;
            health = player.hitPoints;
        }
    }
}
