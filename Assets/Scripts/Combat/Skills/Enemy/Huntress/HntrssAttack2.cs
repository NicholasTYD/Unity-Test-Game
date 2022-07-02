using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HntrssAttack2 : EnemySkill
{
    [SerializeField] EnemySkill HntrssAttack3;

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        enemyMovement.FaceTowards(player.transform.position);
        base.ExecuteSkill(enemy, player);
        StartCoroutine(executeAttack2(enemy, player));
    }

    IEnumerator executeAttack2(EnemyMain enemy, PlayerMain player)
    {
        yield return new WaitForSeconds(skillCooldownTimer);
        HntrssAttack3.ExecuteSkill(enemy, player);
    }
}
