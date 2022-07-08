using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherShoot : EnemySkill
{
    [SerializeField] GameObject spawnable;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;

    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
    }

    public void ShootProjectile()
    {
        CombatMechanics.Instance.InstantiateProjectile(spawnable, getProjectileSpawnPoint(),
            getRangedAimDirection(),
            enemyCombat.GetAbilityDamage(), projectileSpeed, projectileLifetime);
    }
}
