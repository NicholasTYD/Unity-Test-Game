using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkillScriptableObject : ScriptableObject
{
    public float Cooldown;
    public float DamageMultiplier;

    public abstract bool CanUse();

    public abstract void ExecuteSkill(EnemyMain enemy, PlayerMain player);
}
