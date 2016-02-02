using UnityEngine;
using System.Collections;

public class WeaponSpecial : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private BulletManager bulletManager;
    private GameObject specialPrefab;
    private GameObject specialInstance;
    private PlayerBase playerBase;
    private WeaponBase weaponBase;

    public void SetUp(PlayerInfo pi, BulletManager bm, PlayerBase pb, WeaponBase wb)
    {
        playerBase = pb;
        playerInfo = pi;
        bulletManager = bm;
        weaponBase = wb;
        LoadSpecials();
    }

    public void LoadSpecials()
    {
        switch (playerInfo.charEnum)
        {
            case CharacterEnum.Tesla:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Tesla.ToString());
                break;
            case CharacterEnum.Curie:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Curie.ToString());
                break;
            case CharacterEnum.DaVinci:
                LoadGameObject("WeaponSpecial" + CharacterEnum.DaVinci.ToString());
                //LoadGameObject("WeaponSpecial" + CharacterEnum.Tesla.ToString());
                break;
            case CharacterEnum.Einstein:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Einstein.ToString());
                break;
            case CharacterEnum.Nobel:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Nobel.ToString());
                break;
        }
    }

    public void LoadGameObject(string name)
    {

        specialPrefab = (GameObject)Resources.Load("Prefabs/" + name);
        Debug.Log(specialPrefab);
        specialInstance = (GameObject)Instantiate(specialPrefab);
        specialInstance.transform.parent = gameObject.transform.parent;
       

        switch (playerInfo.charEnum)
        {
            case CharacterEnum.Tesla:
                specialInstance.GetComponent<WeaponSpecialTeslaLogic>().SetUpVariables(playerBase, bulletManager, weaponBase);
                specialInstance.SetActive(false);
                break;
            case CharacterEnum.Curie:
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpVariables(playerBase, bulletManager);
                specialInstance.SetActive(false);
                break;
            case CharacterEnum.DaVinci:
                specialInstance.GetComponent<WeaponSpecialDaVinciLogic>().SetUpVariables(playerBase, bulletManager, weaponBase);
                //specialInstance.GetComponent<WeaponSpecialTeslaLogic>().SetUpVariables(playerBase, bulletManager, weaponBase);
                break;
            case CharacterEnum.Einstein:
                specialInstance.GetComponent<WeaponSpecialEinsteinLogic>().SetUpVariables(playerBase, bulletManager);
                specialInstance.SetActive(false);
                break;
            case CharacterEnum.Nobel:
                specialInstance.GetComponent<WeaponSpecialNobelLogic>().SetUpVariables(playerBase, bulletManager, weaponBase);
                break;
        }
    }

    public void LoadHeroModifier()
    {
    }

    public void fire(FireProps fireProps, BulletManager bm, WeaponHandling wh)
    {
        switch (playerInfo.charEnum)
        {
            case CharacterEnum.Tesla:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialTeslaLogic>().fire();
                break;
            case CharacterEnum.Curie:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().fire(fireProps);
                break;
            case CharacterEnum.DaVinci:
                
                specialInstance.GetComponent<WeaponSpecialDaVinciLogic>().fire(fireProps, wh);
                specialInstance.SetActive(true);
                //specialInstance.GetComponent<WeaponSpecialTeslaLogic>().fire();
                break;
            case CharacterEnum.Einstein:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialEinsteinLogic>().fire(fireProps);
                break;
            case CharacterEnum.Nobel:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialNobelLogic>().fire(fireProps);
                break;
        }
    }
}
