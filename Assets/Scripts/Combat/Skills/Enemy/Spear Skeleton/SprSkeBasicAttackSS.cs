using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprSkeBasicAttackSS : EnemySkill
{
    public override bool CanUse()
    {
        return true;
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        Debug.Log("Skill used");
    }
}
