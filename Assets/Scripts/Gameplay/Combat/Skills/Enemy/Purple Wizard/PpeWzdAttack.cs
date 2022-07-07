using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PpeWzdAttack : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        enemyMovement.FaceTowards(player.transform.position);
        base.ExecuteSkill(enemy, player);
    }
}
