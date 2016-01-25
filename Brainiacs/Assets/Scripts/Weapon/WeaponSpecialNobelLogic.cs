using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WeaponSpecialNobelLogic : MonoBehaviour {

    private BulletManager bulletManager;
    private PlayerBase playerBase;
    private WeaponBase weaponBase;

    private FireProps fireProps;
    private bool update = false;

    private float traveledDistance = 0;
    private float clicksShooted = 0;
    private float shootEveryDst = 0.75f;

    private List<Bullet> usedBullets = new List<Bullet>(); 


    public void SetUpVariables(PlayerBase pb, BulletManager bm, WeaponBase wb)
    {
        playerBase = pb;
        bulletManager = bm;
        weaponBase = wb;
    }

    void FixedUpdate()
    {
       

        List<Bullet> to_remove = new List<Bullet>();

        foreach (var b in usedBullets)
        {
            bool active = b.gameObject.activeInHierarchy;
            if (!active)
            {
                to_remove.Add(b);
                weaponBase.reload(1);
            }

        }

        foreach (var b in to_remove)
        {
            usedBullets.Remove(b);
        }
    }

    public void fire(FireProps fp)
    {

        fireProps = fp;

        if (usedBullets.Count <= 5)
        {
            Bullet b = bulletManager.fire(fireProps);
            usedBullets.Add(b);
        }



    }

}
