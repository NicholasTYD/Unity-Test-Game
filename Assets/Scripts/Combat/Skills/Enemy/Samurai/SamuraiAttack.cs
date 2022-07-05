using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiAttack : DefaultEnemySkill
{
    [SerializeField] float dashHealthPercentageThreshold;
    [SerializeField] List<float> attackTimings;

    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
        StartCoroutine(initiateCharge(enemy, player));
    }

    IEnumerator initiateCharge(EnemyMain enemy, PlayerMain player)
    {
        float prevAttackTime = 0;
        foreach (float time in attackTimings)
        {
            yield return new WaitForSeconds(time - prevAttackTime);
            Vector2 ChargeDirection = General.Instance.GetDirectionUnitVector(this.transform.position, General.Instance.Player.transform.position);
            enemyRb.AddForce(ChargeDirection * 10, ForceMode2D.Impulse);
            prevAttackTime = time;
        }
    }
}
