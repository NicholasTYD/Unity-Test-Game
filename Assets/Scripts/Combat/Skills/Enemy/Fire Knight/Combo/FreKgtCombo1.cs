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
        return base.CanUse() && enemyMovement.playerDistanceWithin(maxCastRange);
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
        StartCoroutine(ExecuteCombo1(enemy, player));
    }

    IEnumerator ExecuteCombo1(EnemyMain enemy, PlayerMain player)
    {
        yield return new WaitForSeconds(chargeDelay);
        enemyMovement.ToggleForceMove(true);
        enemyMovement.SetSpeed(chargeSpeed);
        yield return new WaitForSeconds(chargeDuration);
        enemyMovement.SetSpeed(enemyMovement.BaseSpeed);
        enemyMovement.ToggleForceMove(false);
    }
}
