using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiBlinkNormal : EnemySkill
{
    [SerializeField] SamuraiBossMovementAI samuraiBossMovementAI;
    [SerializeField] float dashForce;
    [SerializeField] float dashDelay;

    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);

        List<float> temp = new List<float>(1);
        temp.Add(dashDelay);
        samuraiBossMovementAI.initiateDashes(enemy, player, -dashForce, temp);
    }
}
