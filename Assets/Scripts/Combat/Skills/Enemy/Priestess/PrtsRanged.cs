using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrtsRanged : EnemySkill
{
    [SerializeField] PrtsBossMovementAI prtsBossMovementAI;
    [SerializeField] List<float> attackTimings;

    public override bool CanUse()
    {
        return base.CanUse();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        StartCoroutine(helper(enemy, player));
    }

    IEnumerator helper(EnemyMain enemy, PlayerMain player)
    {
        prtsBossMovementAI.ToggleMoveToCenter(true);
        while (!General.Instance.AtStageCenter(this.transform.position))
        {
            yield return new WaitForSeconds(0.01f);
        }
        prtsBossMovementAI.ToggleMoveToCenter(false);
        enemyMovement.FaceTowards(player.transform.position);

        anim.applyRootMotion = false;
        base.ExecuteSkill(enemy, player);
        yield return new WaitForSeconds(enemySkillBasicStats.SkillDuration);
        anim.applyRootMotion = true;
    }
}
