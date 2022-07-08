using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileRush : EnemySkill
{
    [SerializeField] float xStopBounds = 0.4f;
    [SerializeField] float yStopBounds = 0.4f;

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        StartCoroutine(helper(enemy, player));
    }

    IEnumerator helper(EnemyMain enemy, PlayerMain player)
    {
        enemyMovement.ToggleForceMove(true);
        while (!RushStopCriteraFufilled())
        {
            yield return new WaitForSeconds(0.01f);
        }
        enemyMovement.ToggleForceMove(false);

        base.ExecuteSkill(enemy, player);
    }

    private bool RushStopCriteraFufilled()
    {
        return enemyMovement.enemyToPlayerXDifferenceWithin(-xStopBounds, xStopBounds) &&
            enemyMovement.enemyToPlayerYDifferenceWithin(-yStopBounds, yStopBounds);
    }
}
