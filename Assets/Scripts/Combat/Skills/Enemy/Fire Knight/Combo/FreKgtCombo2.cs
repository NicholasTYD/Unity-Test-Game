using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreKgtCombo2 : EnemySkill
{
    [SerializeField] float minXBound;
    [SerializeField] float maxXBound;
    [SerializeField] float minYBound;
    [SerializeField] float maxYBound;

    public override bool CanUse()
    {
        return base.CanUse() && enemyMovement.enemyToPlayerXDifferenceWithin(minXBound, maxXBound)
            && enemyMovement.enemyToPlayerYDifferenceWithin(minYBound, maxYBound);
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
    }
}
