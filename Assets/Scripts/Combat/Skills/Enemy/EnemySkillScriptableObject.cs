using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkillScriptableObject : ScriptableObject
{
    public float SkillCooldown;
    public float DamageMultiplier;
    public float AttackLockoutDuration;

    public abstract bool CanUse();

    public virtual void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        enemy.AttackLockoutDuration = AttackLockoutDuration;
    }
}
