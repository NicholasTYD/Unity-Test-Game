using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwdSkeBasicAttack : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        Debug.Log("Used skill");
        base.ExecuteSkill(enemy, player);
    }
}
