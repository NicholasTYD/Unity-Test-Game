using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreKgtBlock : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        enemy.lockoutDuration = enemySkillBasicStats.SkillDuration;
        enemy.AttackLockoutDuration = enemySkillBasicStats.SkillDuration +
        Random.Range(enemySkillBasicStats.minAttackLockoutDuration, enemySkillBasicStats.maxAttackLockoutDuration);
        skillCooldownTimer = enemySkillBasicStats.SkillDuration +
            Random.Range(enemySkillBasicStats.MinSkillCooldown, enemySkillBasicStats.MaxSkillCooldown);
        StartCoroutine(adjustDamageMultiplierDuringSkill());

        StartCoroutine(toggleBlock());
    }

    IEnumerator toggleBlock()
    {
        anim.SetBool(enemySkillBasicStats.name, true);
        yield return new WaitForSeconds(enemySkillBasicStats.SkillDuration);
        anim.SetBool(enemySkillBasicStats.name, false);
    }
}
