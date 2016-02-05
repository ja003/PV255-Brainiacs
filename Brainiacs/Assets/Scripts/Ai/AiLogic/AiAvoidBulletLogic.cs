using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AiAvoidBulletLogic {

    public AiBase aiBase;
    public AiMovementLogic aiMovementLogic;
    public AiPowerUpLogic aiPowerUpLogic;
    public AiWeaponLogic aiWeaponLogic;
    //public AiAvoidBulletLogic aiAvoidBulletLogic;
    public AiPriorityLogic aiPriorityLogic;
    public AiActionLogic aiActionLogic;
    public AiMapLogic aiMapLogic;

    protected Vector2 up = Vector2.up;
    protected Vector2 down = Vector2.down;
    protected Vector2 left = Vector2.left;
    protected Vector2 right = Vector2.right;
    protected Vector2 stop = Vector2.zero;

    public int killPlayer1Priority;
    public int killPlayer2Priority;
    public int killPlayer3Priority;
    public int killPlayer4Priority;
    
    float characterColliderWidth;
    float characterColliderHeight;
    
    public bool bulletIncoming;
    public Vector2 bulletFrom;
    public LayerMask bulletMask;

    public AiAvoidBulletLogic(AiBase aiBase)
    {
        this.aiBase = aiBase;
        bulletMask = aiBase.bulletMask;

        aiMovementLogic = aiBase.aiMovementLogic;

        characterColliderWidth = aiBase.characterColliderWidth;
        characterColliderHeight = aiBase.characterColliderHeight;
    }

    public bool RegisterBullets()
    {
        bulletIncoming = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(aiBase.transform.position, aiBase.mapWidth / 2, aiBase.bulletMask);
        List<Collider2D> listColliders = new List<Collider2D>(colliders);

        List<int> indicesToRemove = new List<int>();
        for (int i = colliders.Length-1; i >=0 ;i--)
        {
            try
            {
                Bullet b = colliders[i].GetComponentInParent<Bullet>();
                SpriteRenderer sr = colliders[i].GetComponentInParent<SpriteRenderer>();

                if (sr.sprite.ToString().Contains("mine"))
                {
                    listColliders.RemoveAt(i);
                }
                else
                {

                }
            }
            catch (Exception e)
            {

            }
        }


        foreach (Collider2D collider in listColliders)
        {
            Vector2 bulletPosition = collider.transform.position;
            Vector2 bulletDirection = collider.GetComponent<Bullet>().direction;

            if (aiMapLogic.AlmostEqual(bulletPosition.x, aiBase.posX, characterColliderWidth))//bullet above or bellow
            {
                if (bulletPosition.y > aiBase.posY) //bullet is above
                {
                    if (bulletDirection == down) //bullet is aiming down
                    {
                        bulletIncoming = true;
                        bulletFrom = up;
                    }
                }
                else //bullet is bellow
                {
                    if (bulletDirection == up) //bullet is aiming up
                    {
                        bulletIncoming = true;
                        bulletFrom = down;
                    }
                }
            }
            else if (aiMapLogic.AlmostEqual(bulletPosition.y, aiBase.posY, characterColliderHeight))//bullet is on left or right
            {
                if (bulletPosition.x > aiBase.posX) //bullet is on right
                {
                    if (bulletDirection == left) //bullet is aiming left
                    {
                        bulletIncoming = true;
                        bulletFrom = right;
                    }
                }
                else //bullet on left
                {
                    if (bulletDirection == right) //bullet is aiming right
                    {
                        bulletIncoming = true;
                        bulletFrom = left;
                    }
                }
            }


        }
        return bulletIncoming;
    }

    public bool decidedDirectionBool = false;
    public Vector2 decidedDirection;
    public void AvoidBullet()
    {
        float verticalDistance = aiBase.characterColliderHeight;
        float horizontalDistance = aiBase.characterColliderHeight;

        if (
            (decidedDirection == up && aiMovementLogic.CollidesBarrier(up, verticalDistance)) ||
            (decidedDirection == right && aiMovementLogic.CollidesBarrier(right, horizontalDistance)) ||
            (decidedDirection == down && aiMovementLogic.CollidesBarrier(down, verticalDistance)) ||
            (decidedDirection == left && aiMovementLogic.CollidesBarrier(left, horizontalDistance))
            )
        {
            decidedDirectionBool = false;
        }

        if (decidedDirectionBool)
        {
            if (decidedDirection == up)
            {
                aiMovementLogic.MoveTo(aiBase.posX, aiBase.posY + verticalDistance);
            }
            else if (decidedDirection == right)
            {
                aiMovementLogic.MoveTo(aiBase.posX + horizontalDistance, aiBase.posY);
            }
            else if (decidedDirection == down)
            {
                aiMovementLogic.MoveTo(aiBase.posX, aiBase.posY - verticalDistance);
            }
            else if (decidedDirection == left)
            {
                aiMovementLogic.MoveTo(aiBase.posX - horizontalDistance, aiBase.posY);
            }
            else
            {
                
            }
        }
        else
        {
            if (bulletFrom == up)
            {
                if (!aiMovementLogic.CollidesBarrier(left, horizontalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX - horizontalDistance, aiBase.posY);
                    decidedDirection = left;
                }
                else if (!aiMovementLogic.CollidesBarrier(right, horizontalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX + horizontalDistance, aiBase.posY);
                    decidedDirection = right;
                }
                else if (!aiMovementLogic.CollidesBarrier(down, verticalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX, aiBase.posY - verticalDistance);
                    decidedDirection = down;
                }
            }
            else if (bulletFrom == right)
            {
                if (!aiMovementLogic.CollidesBarrier(up, verticalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX, aiBase.posY + verticalDistance);
                    decidedDirection = up;
                }
                else if (!aiMovementLogic.CollidesBarrier(down, verticalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX, aiBase.posY - verticalDistance);
                    decidedDirection = down;
                }
                else if (!aiMovementLogic.CollidesBarrier(left, horizontalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX - horizontalDistance, aiBase.posY);
                    decidedDirection = left;
                }
                else
                {

                }
            }
            else if (bulletFrom == down)
            {
                if (!aiMovementLogic.CollidesBarrier(left, horizontalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX - horizontalDistance , aiBase.posY);
                    decidedDirection = left;
                }
                else if (!aiMovementLogic.CollidesBarrier(right, horizontalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX + horizontalDistance , aiBase.posY);
                    decidedDirection = right;
                }
                else if (!aiMovementLogic.CollidesBarrier(up, verticalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX, aiBase.posY + verticalDistance);
                    decidedDirection = up;
                }
            }
            else if (bulletFrom == left)
            {
                if (!aiMovementLogic.CollidesBarrier(up, verticalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX, aiBase.posY + verticalDistance);
                    decidedDirection = up;
                }
                else if (!aiMovementLogic.CollidesBarrier(down, verticalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX, aiBase.posY - verticalDistance);
                    decidedDirection = down;
                }
                else if (!aiMovementLogic.CollidesBarrier(right, horizontalDistance))
                {
                    aiMovementLogic.MoveTo(aiBase.posX + horizontalDistance , aiBase.posY);
                    decidedDirection = right;
                }
            }
            decidedDirectionBool = true;
        }

        //chek if you avoided
        if (!RegisterBullets())
        {
            decidedDirectionBool = false;
            decidedDirection = down;
            aiPriorityLogic.SetAvoidBulletPriority(0);
            aiActionLogic.UpdateCurrentAction();
        }
    }

    
}
