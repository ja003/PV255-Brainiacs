using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

    List<GameObject> prefabBullets = new List<GameObject>();
    List<GameObject> prefabSpecialBullets = new List<GameObject>();
    public GameObject bullet;
    public int maxBullets = 20;
    public int indexUsedBullet = 0;

    protected Vector2 up = Vector2.up;
    protected Vector2 down = Vector2.down;
    protected Vector2 left = Vector2.left;
    protected Vector2 right = Vector2.right;

    public void createBullets(PlayerBase owner)
    {
        //Debug.Log("created");
        bullet = (GameObject)Resources.Load("Prefabs/Projectile");

        for (int i = 0; i < 20; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            prefabBullets.Add(obj);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);

            Bullet bulletClass = obj.GetComponent<Bullet>();
            bulletClass.owner = owner;
        }

        bullet = (GameObject)Resources.Load("Prefabs/SpecialProjectile");
        for (int i = 0; i < 5; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            prefabSpecialBullets.Add(obj);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);

            Bullet bulletClass = obj.GetComponent<Bullet>();
            bulletClass.owner = owner;
        }
        //Debug.Log(prefabBullets.Count);
        //Debug.Log(prefabBullets[indexUsedBullet]);
    }

    public void fire(Vector2 direction, Vector2 position, RuntimeAnimatorController animController, float bulletSpeed, int damage, string weapon)
    {
        prefabBullets[indexUsedBullet].GetComponent<Bullet>().iniciate(
            direction, 
            position, 
            animController, 
            bulletSpeed, 
            damage, 
            weapon
            );

        indexUsedBullet = (indexUsedBullet + 1) % maxBullets;
    }

}
