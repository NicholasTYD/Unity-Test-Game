using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwdSkeMovementAI : EnemyMovementAI
{
    protected override bool StopCriteraFufilled()
    {
        return enemyMovement.playerDistanceWithin(1) &&
            enemyMovement.enemyToPlayerYDifferenceWithin(-playerBoxColliderHeight, playerBoxColliderHeight / 2);
    }

    public override void Move(float speed)
    {
        if (StopCriteraFufilled())
        {
            return;
        }
        base.Move(speed);
    }
}
