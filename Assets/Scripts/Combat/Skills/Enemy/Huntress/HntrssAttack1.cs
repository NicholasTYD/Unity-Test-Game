using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HntrssAttack1 : DefaultEnemySkill
{
    [SerializeField] EnemySkill HntrssAttack2;

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
        StartCoroutine(executeAttack2(enemy, player));
    }

    IEnumerator executeAttack2(EnemyMain enemy, PlayerMain player)
    {
        yield return new WaitForSeconds(skillCooldownTimer);
        HntrssAttack2.ExecuteSkill(enemy, player);
    }
}
