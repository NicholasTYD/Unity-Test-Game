using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrtsRanged : EnemySkill
{
    [SerializeField] PrtsBossMovementAI prtsBossMovementAI;

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        StartCoroutine(helper(enemy, player));
    }

    IEnumerator helper(EnemyMain enemy, PlayerMain player)
    {
        // Move to arena center
        prtsBossMovementAI.ToggleMoveToCenter(true);
        while (!General.Instance.AtStageCenter(this.transform.position))
        {
            yield return new WaitForSeconds(0.01f);
        }
        prtsBossMovementAI.ToggleMoveToCenter(false);
        enemyMovement.FaceTowards(player.transform.position);

        // Execute attack
        anim.applyRootMotion = false;
        base.ExecuteSkill(enemy, player);
        yield return new WaitForSeconds(enemySkillBasicStats.SkillDuration);
        anim.applyRootMotion = true;
    }

    public void ShootLaser()
    {
        // Adjust rotation of beam
        Vector2 laserRotation = getRangedAimDirection();
        Quaternion laserQuaternion = General.Instance.ConvertRotationToQuaternion(laserRotation);
        enemyCombat.EnemyProjectileSpawnPoint.rotation = laserQuaternion;

        // Adjust length of beam
        float laserLength = getRangedAimDistance();
        Vector2 laserLocalScale = enemyCombat.EnemyProjectileSpawnPoint.localScale;
        laserLocalScale.x = laserLength;
        enemyCombat.EnemyProjectileSpawnPoint.localScale =
            General.Instance.GetLocalScale(laserLocalScale, gameObject);
    }
}
