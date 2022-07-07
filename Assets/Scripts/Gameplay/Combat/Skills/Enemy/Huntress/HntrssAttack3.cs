using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HntrssAttack3 : EnemySkill
{
    [SerializeField] HuntressMovementAI huntressMovementAI;
    [SerializeField] GameObject spawnable;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;
    [SerializeField] float shootDelay;

    [SerializeField] float runTime;
    
    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    { 
        StartCoroutine(runAndShoot(enemy, player));
    }

    protected virtual IEnumerator runAndShoot(EnemyMain enemy, PlayerMain player)
    {
        // Run away from player
        enemy.AttackLockoutDuration = runTime;
        huntressMovementAI.RunningAway = true;
        yield return new WaitForSeconds(runTime);
        huntressMovementAI.RunningAway = false;

        enemyMovement.FaceTowards(player.transform.position);
        base.ExecuteSkill(enemy, player);
        yield return new WaitForSeconds(shootDelay);
        if (!enemy.isDead)
        {
            CombatMechanics.Instance.InstantiateProjectile(spawnable, getProjectileSpawnPoint(),
            getRangedAimDirection(),
            enemyCombat.GetAbilityDamage(), projectileSpeed, projectileLifetime);
        }
    }
}
