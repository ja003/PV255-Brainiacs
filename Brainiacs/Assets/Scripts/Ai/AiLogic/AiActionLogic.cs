using UnityEngine;
using System.Collections;

public class AiActionLogic {

    public AiBase aiBase;

    public AiPowerUpLogic aiPowerUpLogic;
    public AiWeaponLogic aiWeaponLogic;
    public AiMovementLogic aiMovementLogic;
    public AiAvoidBulletLogic aiAvoidBulletLogic;
    public AiPriorityLogic aiPriorityLogic;
    //public AiActionLogic aiActionLogic;
    public AiKillingLogic aiKillingLogic;

    public AiActionLogic(AiBase aiBase)
    {
        this.aiBase = aiBase;
    }

    public void PrintAction()
    {
        string message = "";
        message += "current action=" + currentAction;

        Debug.Log(message);
    }

    public void DoCurrentAction()
    {
        switch (currentAction)
        {
            case AiActionEnum.killPlayer1:
                aiKillingLogic.KillPlayer(aiBase.player1);
                break;
            case AiActionEnum.killPlayer2:
                aiKillingLogic.KillPlayer(aiBase.player2);
                break;
            case AiActionEnum.killPlayer3:
                aiKillingLogic.KillPlayer(aiBase.player3);
                break;
            case AiActionEnum.killPlayer4:
                aiKillingLogic.KillPlayer(aiBase.player4);
                break;
            case AiActionEnum.avoidBullet:
                aiAvoidBulletLogic.AvoidBullet();
                break;
            case AiActionEnum.pickupPowerUp:
                aiPowerUpLogic.PickUp(aiPowerUpLogic.bestPowerUp);
                break;
            case AiActionEnum.pickupWeapon:
                aiPowerUpLogic.PickUp(aiWeaponLogic.bestWeaponItem);
                break;
            case AiActionEnum.stand:
                aiMovementLogic.Stop();
                break;
            default:
                aiMovementLogic.Stop();
                break;
        }
        //Debug.Log("processing action: " + currentAction);
    }

    public AiActionEnum currentAction;

    public void BackFromDead()
    {

    }

    public void ProcessDeath()
    {
        //Debug.Log("DEATH");
        //aiPriorityLogic.standPriority = 666;
    }

    public void UpdateCurrentAction()
    {
        int highestPriority = aiPriorityLogic.GetCurrentHighestPriority();
        //Debug.Log("highest = " + highestPriority);

        if (aiPriorityLogic.killPlayer1Priority >= highestPriority)
        {
            currentAction = AiActionEnum.killPlayer1;
        }
        else if (aiPriorityLogic.killPlayer2Priority >= highestPriority)
        {
            currentAction = AiActionEnum.killPlayer2;
        }
        else if (aiPriorityLogic.killPlayer3Priority >= highestPriority)
        {
            currentAction = AiActionEnum.killPlayer3;
        }
        else if (aiPriorityLogic.killPlayer4Priority >= highestPriority)
        {
            currentAction = AiActionEnum.killPlayer4;
        }
        else if (aiPriorityLogic.avoidBulletPriority >= highestPriority)
        {
            currentAction = AiActionEnum.avoidBullet;
        }
        else if (aiPriorityLogic.pickPowerUpPriority >= highestPriority)
        {
            currentAction = AiActionEnum.pickupPowerUp;
        }
        else if (aiPriorityLogic.pickWeaponPriority >= highestPriority)
        {
            currentAction = AiActionEnum.pickupWeapon;
        }
        else if (aiPriorityLogic.standPriority >= highestPriority)
        {
            currentAction = AiActionEnum.stand;
            //Debug.Log("STAND");
        }

        else
        {
            currentAction = AiActionEnum.stand;
        }

        //Debug.Log("currentAction: " +currentAction);
        //currentAction = AiActionEnum.stand;
    }


}
