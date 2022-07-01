using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreKgtSpecialAttack : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && anim.GetBool("Block");
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        anim.SetBool("Block", false);
        base.ExecuteSkill(enemy, player);
    }
}
