using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : EnemySkill
{ 
    Vector2 laserScale;

    protected override void Start()
    {
        base.Start();
        laserScale = enemyCombat.EnemyProjectileSpawnPoint.localScale;
    }

    public void AdjustTracer()
    {
        // Adjust rotation of beam
        Vector2 laserRotation = getRangedAimDirection();
        Quaternion laserQuaternion = General.Instance.ConvertRotationToQuaternion(laserRotation);
        enemyCombat.EnemyProjectileSpawnPoint.rotation = laserQuaternion;

        enemyCombat.EnemyProjectileSpawnPoint.localScale =
            General.Instance.GetLocalScale(laserScale, gameObject);
    }
}
