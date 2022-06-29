using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprSkeMovementAI : EnemyMovementAI
{
    protected override bool StopCriteraFufilled()
    {
        return enemyMovement.playerDistanceWithin(1) &&
            enemyMovement.enemyToPlayerYDifferenceWithin(-playerBoxColliderHeight / 2, 0);
    }
}
