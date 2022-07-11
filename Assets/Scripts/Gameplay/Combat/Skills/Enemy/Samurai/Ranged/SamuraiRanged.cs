using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiRanged : EnemySkill
{
    [SerializeField] GameObject spawnable;
    [SerializeField] float healthPercentageThreshold;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;
    [SerializeField] List<float> attackTimings;

    public override bool CanUse()
    {
        return base.CanUse() && (enemyHealth.isHealthPercentageEqualOrBelow(healthPercentageThreshold));
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
        StartCoroutine(ExecuteBladeWaves());
    }

    IEnumerator ExecuteBladeWaves()
    {
        float prevAttackTime = 0.0f;
        foreach (float time in attackTimings)
        {
            yield return new WaitForSeconds(time - prevAttackTime);
            enemyMovement.FaceTowards(player.transform.position);
            CombatMechanics.Instance.InstantiateProjectile(spawnable, getProjectileSpawnPoint(),
            getRangedAimDirection(),
            enemyCombat.GetAbilityDamage(), projectileSpeed, projectileLifetime);
            prevAttackTime = time;
        }
    }

    public void StopBladeWaves()
    {
        StopAllCoroutines();
    }
}
