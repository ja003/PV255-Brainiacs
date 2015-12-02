using UnityEngine;
using System.Collections;

public class AiMapLogic  {

    public AiBase aiBase;
    public LayerMask itemMask;

    //logics
    public AiPowerUpLogic aiPowerUpLogic;
    public AiWeaponLogic aiWeaponLogic;
    public AiMovementLogic aiMovementLogic;
    public AiAvoidBulletLogic aiAvoidBulletLogic;
    public AiPriorityLogic aiPriorityLogic;

    public AiMapLogic(AiBase aiBase)
    {
        this.aiBase = aiBase;

    }


    public bool IsInPlayground(Vector2 point)
    {
        return aiBase.mapMinX < point.x && point.x < aiBase.mapMaxX && aiBase.mapMinY < point.y && point.y < aiBase.mapMaxY;
    }


    public bool AlmostEqual(float pos1, float pos2, double e)
    {
        return pos2 - e < pos1 && pos1 < pos2 + e;
    }

    public Vector2 GetObjectDirection(GameObject obj)
    {
        double distanceX = Mathf.Abs(aiBase.posX - obj.transform.position.x);
        double distanceY = Mathf.Abs(aiBase.posY - obj.transform.position.y);

        //prefer more dominant axis
        if (distanceX > distanceY)
        {
            //it is on my left
            if (obj.transform.position.x < aiBase.posX)
            {
                return aiBase.left;
            }
            else
            {
                return aiBase.right;
            }
        }
        else
        {
            //it is below me
            if (obj.transform.position.y < aiBase.posY)
            {
                return aiBase.down;
            }
            else
            {
                return aiBase.up;
            }
        }
    }

    public void LookAt(GameObject obj)
    {

        double distanceX = Mathf.Abs(aiBase.posX - obj.transform.position.x);
        double distanceY = Mathf.Abs(aiBase.posY - obj.transform.position.y);

        //prefer more dominant axis
        if (distanceX > distanceY)
        {
            //it is on my left
            if (obj.transform.position.x < aiBase.posX)
            {
                aiBase.direction = aiBase.left;
                aiBase.UpdateAnimatorState(AnimatorStateEnum.walkLeft);
            }
            else
            {
                aiBase.direction = aiBase.right;
                aiBase.UpdateAnimatorState(AnimatorStateEnum.walkRight);
            }
        }
        else
        {
            //it is below me
            if (obj.transform.position.y < aiBase.posY)
            {
                aiBase.direction = aiBase.down;
                aiBase.UpdateAnimatorState(AnimatorStateEnum.walkDown);
            }
            else
            {
                aiBase.direction = aiBase.up;
                aiBase.UpdateAnimatorState(AnimatorStateEnum.walkUp);
            }
        }
        //UpdateAnimatorState(AnimatorStateEnum.stop);

    }


    public void LookAroundYourself()
    {
        aiAvoidBulletLogic.RegisterBullets();

        aiPowerUpLogic.RegisterPowerUps();

        aiWeaponLogic.RegisterWeapons();


    }

    public float GetDistance(GameObject object1, GameObject object2)
    {
        return (object1.transform.position - object2.transform.position).sqrMagnitude;
    }

    public float GetDistance(Vector2 pos1, Vector2 pos2)
    {
        return (pos1 - pos2).sqrMagnitude;
    }

    public float GetDistanceFactor(float distance)
    {
        return (100 - distance) / 100;
    }

}
