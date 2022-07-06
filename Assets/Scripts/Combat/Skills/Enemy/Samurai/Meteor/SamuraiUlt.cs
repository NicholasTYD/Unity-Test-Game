using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiUlt : EnemySkill
{
    // Others
    [SerializeField] SamuraiBossMovementAI samuraiBossMovementAI;
    [SerializeField] float healthPercentageThreshold;
    [SerializeField] ParticleSystem glowingEyes;

    // Jump
    [SerializeField] string jump;
    [SerializeField] float jumpDuration;

    // Blink phase
    [SerializeField] string blink;
    [SerializeField] float blinkTeleportTime;
    [SerializeField] float blinkDuration;

    // Ult
    Vector2 afkArea = new Vector2(60, 60);
    [SerializeField] GameObject plusMeteor;
    [SerializeField] GameObject crossMeteor;
    [SerializeField] float ultDuration;
    [SerializeField] int waveCount;
    [SerializeField] float waveInterval;
    [SerializeField] int meteorsPerWave;
    [SerializeField] float shockwaveSpeed;
    [SerializeField] float shockwaveLifetime;

    Vector2 prevPos;

    public override bool CanUse()
    {
        return base.CanUse() && enemyHealth.isHealthPercentageEqualOrBelow(healthPercentageThreshold);
    }

    // Complete override
    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        StartCoroutine(jumpUp(enemy, player));

        enemy.lockoutDuration = enemySkillBasicStats.SkillDuration;
        enemy.AttackLockoutDuration = enemySkillBasicStats.SkillDuration +
        Random.Range(enemySkillBasicStats.minAttackLockoutDuration, enemySkillBasicStats.maxAttackLockoutDuration);
        skillCooldownTimer = enemySkillBasicStats.SkillDuration +
            Random.Range(enemySkillBasicStats.MinSkillCooldown, enemySkillBasicStats.MaxSkillCooldown);
        StartCoroutine(adjustDamageMultiplierDuringSkill());
    }

    IEnumerator jumpUp(EnemyMain enemy, PlayerMain player)
    {
        prevPos = this.transform.position;
        anim.SetTrigger(jump);
        samuraiBossMovementAI.Jump();
        yield return new WaitForSeconds(jumpDuration);
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

        StartCoroutine(blinkBack(enemy, player));
    }

    IEnumerator blinkBack(EnemyMain enemy, PlayerMain player)
    {
        glowingEyes.Play();
        samuraiBossMovementAI.SetEnragedSpeed();
        anim.SetTrigger(blink);
        yield return new WaitForSeconds(blinkTeleportTime);
        this.transform.position = prevPos;
        yield return new WaitForSeconds(blinkDuration - blinkTeleportTime);

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
            shockwaveSpeed, shockwaveLifetime);
    }
}
