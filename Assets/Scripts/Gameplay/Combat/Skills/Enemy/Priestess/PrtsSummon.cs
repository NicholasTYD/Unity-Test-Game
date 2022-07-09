using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrtsSummon : EnemySkill
{
    PrtsBossMovementAI prtsBossMovementAI;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] float healthPercentageThreshold;
    [SerializeField] float spawnXOffset;
    [SerializeField] float spawnTime;

    protected override void Start()
    {
        this.prtsBossMovementAI = this.GetComponent<PrtsBossMovementAI>();
        base.Start();
    }

    public override bool CanUse()
    {
        return base.CanUse() && 
            (enemyHealth.isHealthPercentageEqualOrBelow(healthPercentageThreshold));
    }

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

        base.ExecuteSkill(enemy, player);
        yield return new WaitForSeconds(spawnTime);
        Vector2 spawnOffset = new Vector2(spawnXOffset, 0);
        CombatMechanics.Instance.Spawn(enemy1, (Vector2)this.transform.position - spawnOffset, Quaternion.identity);
        CombatMechanics.Instance.Spawn(enemy2, (Vector2)this.transform.position + spawnOffset, Quaternion.identity);
    }
}
