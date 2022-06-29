using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprSkeBasicAttack : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && enemyMovement.playerDistanceWithin(1) &&
            enemyMovement.enemyToPlayerYDifferenceWithin(-playerBoxColliderHeight / 2, 0);
        // return base.CanUse() && distanceCheck() && yCheck();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        Debug.Log("Used skill");
        enemyMovement.flipIfNeeded(player.gameObject.transform.position);
        base.ExecuteSkill(enemy, player);
    }
}
