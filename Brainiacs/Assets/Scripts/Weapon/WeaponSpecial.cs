using UnityEngine;
using System.Collections;

public class WeaponSpecial : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private BulletManager bulletManager;
    private GameObject specialPrefab;
    private GameObject specialInstance;
    private PlayerBase playerBase;

    public void SetUp(PlayerInfo pi, BulletManager bm, PlayerBase pb)
    {
        playerBase = pb;
        playerInfo = pi;
        bulletManager = bm;
        LoadSpecials();
    }

    public void LoadSpecials()
    {
        switch (playerInfo.charEnum)
        {
            case CharacterEnum.Tesla:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Einstein.ToString());
                LoadGameObject("WeaponSpecial" + CharacterEnum.Einstein.ToString());
                break;
            case CharacterEnum.Curie:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Curie.ToString());
                break;
            case CharacterEnum.DaVinci:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Curie.ToString());
                Debug.Log("WeaponSpecial" + CharacterEnum.Einstein.ToString());
                break;
            case CharacterEnum.Einstein:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Einstein.ToString());
                break;
        }
    }

    public void LoadGameObject(string name)
    {

        specialPrefab = (GameObject)Resources.Load("Prefabs/" + name);
        Debug.Log(specialPrefab);
        specialInstance = (GameObject)Instantiate(specialPrefab);
        specialInstance.transform.parent = gameObject.transform.parent;
        specialInstance.SetActive(false);

        switch (playerInfo.charEnum)
        {
            case CharacterEnum.Tesla:
                //specialInstance.SetActive(true);
                //specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpGraphics();
                specialInstance.GetComponent<WeaponSpecialEinsteinLogic>().SetUpVariables(playerBase, bulletManager);
                break;
            case CharacterEnum.Curie:
               // specialInstance.SetActive(true);
               // specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpGraphics();
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpVariables(playerBase, bulletManager);
                break;
            case CharacterEnum.DaVinci:
                //specialInstance.SetActive(true);
              //  specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpGraphics();
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpVariables(playerBase, bulletManager);
                break;
            case CharacterEnum.Einstein:
                //specialInstance.SetActive(true);
           //     specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpGraphics();
                specialInstance.GetComponent<WeaponSpecialEinsteinLogic>().SetUpVariables(playerBase, bulletManager);
                break;
        }
    }

    public void LoadHeroModifier()
    {
    }

    public void fire(FireProps fireProps, BulletManager bm)
    {
        switch (playerInfo.charEnum)
        {
            case CharacterEnum.Tesla:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().fire(fireProps);
                break;
            case CharacterEnum.Curie:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().fire(fireProps);
                break;
            case CharacterEnum.DaVinci:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().fire(fireProps);
                break;
            case CharacterEnum.Einstein:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().fire(fireProps);
                break;
        }
    }
}
