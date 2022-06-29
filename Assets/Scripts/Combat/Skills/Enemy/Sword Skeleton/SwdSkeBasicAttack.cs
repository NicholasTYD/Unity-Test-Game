using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwdSkeBasicAttack : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && enemyMovement.playerDistanceWithin(1) &&
            enemyMovement.enemyToPlayerYDifferenceWithin(-playerBoxColliderHeight, playerBoxColliderHeight / 2);
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        Debug.Log("Used skill");
        base.ExecuteSkill(enemy, player);
    }
}
