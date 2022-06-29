using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMelee : EnemySkill
{
    private float ybound = 0.75f;

    public override bool CanUse()
    {
        return base.CanUse() && enemyMovement.playerDistanceWithin(1) &&
            enemyMovement.enemyToPlayerYDifferenceWithin(-ybound, ybound);
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
    }
}
