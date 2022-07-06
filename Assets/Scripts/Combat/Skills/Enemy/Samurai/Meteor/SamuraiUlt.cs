using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiUlt : EnemySkill
{
    // Others
    [SerializeField] SamuraiBossMovementAI samuraiBossMovementAI;
    [SerializeField] float healthPercentageThreshold;
    [SerializeField] GameObject glowingEyes;

    // Blink phase
    [SerializeField] string blink;
    [SerializeField] float blinkDuration;
    [SerializeField] float blinkTeleportTime;

    // Jump
    [SerializeField] string jump;
    [SerializeField] float jumpDuration;

    // Ult
    Vector2 afkArea = new Vector2(60, 60);
    [SerializeField] GameObject plusMeteor;
    [SerializeField] GameObject crossMeteor;
    [SerializeField] float ultDuration;
    [SerializeField] int waveCount;
    [SerializeField] float waveInterval;
    [SerializeField] int meteorsPerWave;
    [SerializeField] float shockwaveDamageMultiplier;
    [SerializeField] float shockwaveSpeed;
    [SerializeField] float shockwaveLifetime;

    // Fall
    [SerializeField] string fall;
    [SerializeField] float fallDuration;

    public override bool CanUse()
    {
        return base.CanUse() && enemyHealth.isHealthPercentageEqualOrBelow(healthPercentageThreshold);
    }

    // Complete override
    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        StartCoroutine(blinkToArenaCenter(enemy, player));

        enemy.lockoutDuration = enemySkillBasicStats.SkillDuration;
        enemy.AttackLockoutDuration = enemySkillBasicStats.SkillDuration +
        Random.Range(enemySkillBasicStats.minAttackLockoutDuration, enemySkillBasicStats.maxAttackLockoutDuration);
        skillCooldownTimer = enemySkillBasicStats.SkillDuration +
            Random.Range(enemySkillBasicStats.MinSkillCooldown, enemySkillBasicStats.MaxSkillCooldown);
        StartCoroutine(adjustDamageMultiplierDuringSkill());
    }

    IEnumerator blinkToArenaCenter(EnemyMain enemy, PlayerMain player)
    {
        anim.SetTrigger(blink);
        yield return new WaitForSeconds(blinkTeleportTime);
        this.transform.position = General.Instance.StageCenter;
        yield return new WaitForSeconds(blinkDuration - blinkTeleportTime);

        StartCoroutine(executeUlt(enemy, player));
    }

    IEnumerator jumpUp(EnemyMain enemy, PlayerMain player)
    {
        anim.applyRootMotion = true;
        anim.SetTrigger(jump);
        yield return new WaitForSeconds(jumpDuration);
        anim.applyRootMotion = false;
        this.transform.position = afkArea;

        StartCoroutine(executeUlt(enemy, player));
    }

    IEnumerator executeUlt(EnemyMain enemy, PlayerMain player)
    {
        anim.SetTrigger(enemySkillBasicStats.name);
        int directHit = Random.Range(0, meteorsPerWave * waveCount);
        int meteorCount = 0;

        for (int i = 0; i < waveCount; i++)
        {
            for (int j = 0; j < meteorsPerWave; j++)
            {
                spawnMeteor(meteorCount == directHit);
                meteorCount++;
            }

            yield return new WaitForSeconds(waveInterval);
        }

        yield return new WaitForSeconds(ultDuration - (waveCount * waveInterval));

        StartCoroutine(fallDown(enemy, player));
    }

    IEnumerator fallDown(EnemyMain enemy, PlayerMain player)
    {
        glowingEyes.SetActive(true);
        samuraiBossMovementAI.enrage();

        anim.applyRootMotion = true;
        anim.SetTrigger(fall);
        yield return new WaitForSeconds(fallDuration);
        anim.applyRootMotion = false;

        // Gotta keep subsequent attacks working.
        if (followupSkill != null)
        {
            StartCoroutine(AttemptFollowupSkill(enemy, player));
        }
    }

    private void spawnMeteor(bool atPlayerPos)
    {
        Vector2 spawnPos;
        if (atPlayerPos)
        {
            spawnPos = player.transform.position;
        }
        else
        {
            spawnPos = General.Instance.GetRandomPosition();
        }

        GameObject meteor;
        if (General.Instance.RandomBool())
        {
             meteor = Instantiate(plusMeteor, spawnPos, Quaternion.identity);
        } else
        {
            meteor = Instantiate(crossMeteor, spawnPos, Quaternion.identity);
        }
        meteor.GetComponent<SamuraiMeteor>().SetStats(enemyCombat.GetAbilityDamage(),
            enemyCombat.GetAbilityDamage() * shockwaveDamageMultiplier, shockwaveSpeed, shockwaveLifetime);
    }
}
