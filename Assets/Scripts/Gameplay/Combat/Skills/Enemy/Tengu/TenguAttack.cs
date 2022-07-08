using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenguAttack : EnemySkill
{
    [SerializeField] TenguMovementAI tenguMovementAI;
    [SerializeField] float chargeSpeed;

    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public void StartCharge()
    {
        tenguMovementAI.ToggleCharge(true, chargeSpeed);
    }

    public void StopCharge()
    {
        tenguMovementAI.ToggleCharge(false, chargeSpeed);
    }
}
