using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySkillBasicStats", menuName = "ScriptableObjects/Enemy/EnemySkillBasicStats")]
public class EnemySkillBasicStats : ScriptableObject
{
    [SerializeField] public new string name;
    [SerializeField] public float DamageMultiplier;
    [SerializeField] public float DurationPerFrame;

    // How long the skill lasts
    [SerializeField] public float SkillDuration;
    [SerializeField] public float MinSkillCooldown;
    [SerializeField] public float MaxSkillCooldown;

    // How long afterwards which enemy cannot use any attacks.
    [SerializeField] public float minAttackLockoutDuration;
    [SerializeField] public float maxAttackLockoutDuration;
}
