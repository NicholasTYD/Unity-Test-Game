using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrtsAttack : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && enemyMovement.enemyToPlayerXDifferenceWithin(-1, 1)
            && enemyMovement.enemyToPlayerYDifferenceWithin(-0.5f, 0);
    }
}
