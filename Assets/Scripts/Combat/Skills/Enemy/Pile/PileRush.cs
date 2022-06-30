using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileRush : EnemySkill
{
    [SerializeField] float xStopBounds = 0.3f;
    [SerializeField] float yStopBounds = 0.4f;

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        while (!StopCriteraFufilled())
        {
            enemyMovement.ForceMove();
        }
        base.ExecuteSkill(enemy, player);
    }
    
    private bool StopCriteraFufilled()
    {
        return enemyMovement.enemyToPlayerXDifferenceWithin(-xStopBounds, xStopBounds) &&
            enemyMovement.enemyToPlayerYDifferenceWithin(-yStopBounds, yStopBounds);
    }
}
