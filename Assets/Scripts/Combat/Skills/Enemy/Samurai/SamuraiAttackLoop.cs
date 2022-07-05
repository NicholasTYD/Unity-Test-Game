using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiAttackLoop : EnemySkill
{
    [SerializeField] float healthPercentageThreshold;
    [SerializeField] float loopChance;
    [SerializeField] List<float> attackTimings;

    public override bool CanUse()
    {
        return base.CanUse() && (enemyHealth.GetHealthPercentage() <= healthPercentageThreshold) &&
            (Random.Range(0f, 1) <= loopChance);
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
