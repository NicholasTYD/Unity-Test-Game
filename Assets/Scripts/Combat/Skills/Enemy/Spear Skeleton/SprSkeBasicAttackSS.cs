using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SprSkeBasicAttack", menuName = "ScriptableObjects/Enemy/SpearSkeleton/BasicAttack")]
public class SprSkeBasicAttackSS : EnemySkillScriptableObject
{
    public override bool CanUse()
    {
        return true;
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {

    }
}
