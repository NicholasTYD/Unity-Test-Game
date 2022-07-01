using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySkillBasicStats", menuName = "ScriptableObjects/Enemy/EnemySkillBasicStats")]
public class EnemySkillBasicStats : ScriptableObject
{
    public new string name;
    public float DamageMultiplier;
    public float DurationPerFrame;

    public float StartSkillCooldown;

    // How long the skill lasts
    public float SkillDuration;
    public float MinSkillCooldown;
    public float MaxSkillCooldown;

    // How long afterwards which enemy cannot use any attacks.
    public float minAttackLockoutDuration;
    public float maxAttackLockoutDuration;
}
