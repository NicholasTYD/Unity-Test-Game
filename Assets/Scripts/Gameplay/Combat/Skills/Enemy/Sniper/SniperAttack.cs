using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAttack : EnemySkill
{
    [SerializeField] float tracerDelay;
    Vector2 laserScale;

    protected override void Start()
    {
        base.Start();
        laserScale = enemyCombat.EnemyProjectileSpawnPoint.localScale;
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        enemyMovement.FaceTowards(player.transform.position);
        base.ExecuteSkill(enemy, player);
        StartCoroutine(adjustTracer(enemy, player));
    }

    IEnumerator adjustTracer(EnemyMain enemy, PlayerMain player)
    {
        // The small 0.01f delay is to prevent occasional instances where the beam appears to
        // teleport due to frame update times.
        yield return new WaitForSeconds(tracerDelay);

        // Adjust rotation of beam
        Vector2 laserRotation = getRangedAimDirection();
        Quaternion laserQuaternion = General.Instance.ConvertRotationToQuaternion(laserRotation);
        enemyCombat.EnemyProjectileSpawnPoint.rotation = laserQuaternion;

        enemyCombat.EnemyProjectileSpawnPoint.localScale =
            General.Instance.GetLocalScale(laserScale, gameObject);
    }
}
