using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherShoot : EnemySkill
{
    [SerializeField] Projectile projectile;
    [SerializeField] float ShootTime = 0.9166667f;

    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
        StartCoroutine(ShootArrow());
    }

    IEnumerator ShootArrow()
    {
        yield return new WaitForSeconds(ShootTime);
        CombatMechanics.Instance.InstantiateProjectile(projectile, )
    }
}
