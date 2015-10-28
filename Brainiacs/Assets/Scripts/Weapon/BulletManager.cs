using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

    public List<GameObject> prefabBullets = new List<GameObject>();
    public GameObject bullet;
    public int maxBullets = 20;
    public int indexUsedBullet = 0;

    public void createBullets()
    {

        bullet = (GameObject)Resources.Load("Prefabs/Projectile");

        for (int i = 0; i < 20; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);
            prefabBullets.Add(obj);
        }

    }

    public void fire(Vector2 direction, Vector2 position, string sprite)
    {
        prefabBullets[indexUsedBullet].GetComponent<Bullet>().iniciate(direction, position, sprite);
        indexUsedBullet = (indexUsedBullet + 1) % maxBullets;
    }
}
