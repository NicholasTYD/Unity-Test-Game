using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherShoot : EnemySkill
{
    [SerializeField] GameObject spawnable;
    [SerializeField] float projectileSpeed;
    [SerializeField] float projectileLifetime;
    [SerializeField] float shootDelay;

    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
        StartCoroutine(ShootProjectile());
    }

    protected virtual IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(shootDelay);
        CombatMechanics.Instance.InstantiateProjectile(spawnable, this.transform.position, getDirectionToPlayer(),
            enemyCombat.GetAbilityDamage(), projectileSpeed, projectileLifetime);
    }
}
