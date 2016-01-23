using UnityEngine;
using System.Collections;

public class WeaponSpecial : MonoBehaviour
{
    private PlayerInfo playerInfo;
    private BulletManager bulletManager;
    private GameObject specialPrefab;
    private GameObject specialInstance;

    public void SetUp(PlayerInfo pi)
    {
        playerInfo = pi;
        bulletManager = GetComponent<BulletManager>();
        LoadSpecials();
    }

    public void LoadSpecials()
    {
        switch (playerInfo.charEnum)
        {
            case CharacterEnum.Tesla:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Curie.ToString());
                break;
            case CharacterEnum.Curie:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Curie.ToString());
                break;
            case CharacterEnum.DaVinci:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Curie.ToString());
                Debug.Log("WeaponSpecial" + CharacterEnum.Curie.ToString());
                break;
            case CharacterEnum.Einstein:
                LoadGameObject("WeaponSpecial" + CharacterEnum.Curie.ToString());
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
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpGraphics();
                break;
            case CharacterEnum.Curie:
               // specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpGraphics();
                break;
            case CharacterEnum.DaVinci:
                //specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpGraphics();
                break;
            case CharacterEnum.Einstein:
                //specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().SetUpGraphics();
                break;
        }
    }

    public void LoadHeroModifier()
    {
    }

    public void fire(FireProps fireProps)
    {
        switch (playerInfo.charEnum)
        {
            case CharacterEnum.Tesla:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().fire(bulletManager, fireProps);
                break;
            case CharacterEnum.Curie:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().fire(bulletManager, fireProps);
                break;
            case CharacterEnum.DaVinci:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().fire(bulletManager, fireProps);
                break;
            case CharacterEnum.Einstein:
                specialInstance.SetActive(true);
                specialInstance.GetComponent<WeaponSpecialCurieLogic>().fire(bulletManager, fireProps);
                break;
        }
    }
}
