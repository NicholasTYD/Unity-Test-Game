using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiAttackLoop : EnemySkill
{
    [SerializeField] SamuraiBossMovementAI samuraiBossMovementAI;
    [SerializeField] float healthPercentageThreshold;
    [SerializeField] float loopChance;
    [SerializeField] float dashForce;
    [SerializeField] List<float> attackTimings;

    public override bool CanUse()
    {
        return base.CanUse() && (enemyHealth.GetHealthPercentage() <= healthPercentageThreshold) &&
            (Random.Range(0f, 1) <= loopChance);
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
        samuraiBossMovementAI.initiateCharge(enemy, player, dashForce, attackTimings);
    }
}
