using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkillBasicStats : ScriptableObject
{
    public new string name;
    public float DamageMultiplier;
    public float DurationPerFrame;

    // How long the skill lasts
    public float SkillDuration;
    public float SkillCooldown;

    // How long afterwards which enemy cannot use any attacks.
    public float minAttackLockoutDuration;
    public float maxAttackLockoutDuration;
}
