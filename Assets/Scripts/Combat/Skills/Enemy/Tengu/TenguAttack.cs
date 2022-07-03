using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenguAttack : EnemySkill
{
    [SerializeField] TenguMovementAI tenguMovementAI;
    [SerializeField] float chargeStart;
    [SerializeField] float chargeEnd;
    [SerializeField] float chargeSpeed;

    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
        StartCoroutine(ExecuteCharge(enemy, player));
    }

    IEnumerator ExecuteCharge(EnemyMain enemy, PlayerMain player)
    {
        yield return new WaitForSeconds(chargeStart);
        tenguMovementAI.ToggleCharge(true, chargeSpeed);
        yield return new WaitForSeconds(chargeEnd - chargeStart);
        Debug.Log("end");
        tenguMovementAI.ToggleCharge(false, chargeSpeed);
    }
}
