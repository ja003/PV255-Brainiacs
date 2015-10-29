using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {

    List<GameObject> prefabBullets = new List<GameObject>();
    public GameObject bullet;
    public int maxBullets = 20;
    public int indexUsedBullet = 0;

    public void createBullets()
    {
        Debug.Log("created");
        bullet = (GameObject)Resources.Load("Prefabs/Projectile");

        for (int i = 0; i < 20; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            prefabBullets.Add(obj);
            obj.transform.parent = gameObject.transform;
            obj.SetActive(false);

        }
        Debug.Log(prefabBullets.Count);
        Debug.Log(prefabBullets[indexUsedBullet]);
    }

    public void fire(Vector2 direction, Vector2 position, string sprite)
    {
        prefabBullets[indexUsedBullet].GetComponent<Bullet>().iniciate(direction, position, sprite);
        indexUsedBullet = (indexUsedBullet + 1) % maxBullets;
    }
}
