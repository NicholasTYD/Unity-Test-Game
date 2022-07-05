using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiAttack : EnemySkill
{
    [SerializeField] SamuraiBossMovementAI samuraiBossMovementAI;
    [SerializeField] float dashHealthPercentageThreshold;
    [SerializeField] float dashForce;
    [SerializeField] List<float> attackTimings;

    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        Debug.Log("casting skill1");
        base.ExecuteSkill(enemy, player);
        if (enemyHealth.GetHealthPercentage() <= dashHealthPercentageThreshold)
        {
            samuraiBossMovementAI.initiateCharge(enemy, player, dashForce, attackTimings);
        }
    }
}
