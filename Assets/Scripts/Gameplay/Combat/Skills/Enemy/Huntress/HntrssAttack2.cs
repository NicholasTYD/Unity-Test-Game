using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HntrssAttack2 : EnemySkill
{
    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        enemyMovement.FaceTowards(player.transform.position);
        base.ExecuteSkill(enemy, player);
    }
}
