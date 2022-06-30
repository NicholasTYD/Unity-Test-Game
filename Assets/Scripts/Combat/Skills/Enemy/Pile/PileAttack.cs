using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileAttack : EnemySkill
{
    [SerializeField] float xBounds;
    [SerializeField] float yBounds;

    public override bool CanUse()
    {
        return base.CanUse() && enemyMovement.enemyToPlayerXDifferenceWithin(-xBounds, xBounds) &&
            enemyMovement.enemyToPlayerYDifferenceWithin(-yBounds, yBounds);
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
    }
}
