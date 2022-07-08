using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreKgtCombo1 : EnemySkill
{
    [SerializeField] float maxCastRange;
    [SerializeField] float chargeDelay;
    [SerializeField] float chargeDuration;
    [SerializeField] float chargeSpeed;


    public override bool CanUse()
    {
        return base.CanUse() && enemyMovement.playerDistanceWithin(0, maxCastRange);
    }

    public void StartCharge()
    {
        enemyMovement.ToggleForceMove(true);
        enemyMovement.SetSpeed(chargeSpeed);
    }

    public void StopCharge()
    {
        enemyMovement.SetSpeed(enemyMovement.BaseSpeed);
        enemyMovement.ToggleForceMove(false);
    }
}
