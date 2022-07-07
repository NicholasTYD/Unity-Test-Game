using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreKgtSpecialAttack : EnemySkill
{
    public bool isBlocking;

    public override bool CanUse()
    {
        return base.CanUse() && isBlocking;
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        base.ExecuteSkill(enemy, player);
    }
}
