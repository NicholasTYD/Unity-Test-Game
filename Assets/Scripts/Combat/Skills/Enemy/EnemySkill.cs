using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkill : MonoBehaviour
{
    [SerializeField] protected EnemySkillBasicStats enemySkillBasicStats;
    [SerializeField] protected EnemySkill followupSkill;
    protected Animator anim;
    protected float skillCooldownTimer;

    protected EnemyCombat enemyCombat;
    protected EnemyMovement enemyMovement;
    protected GameObject player;

    protected virtual void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.enemyCombat = this.GetComponent<EnemyCombat>();

        this.enemyMovement = this.GetComponent<EnemyMovement>();
        this.player = GameObject.FindWithTag("Player");

        skillCooldownTimer = enemySkillBasicStats.StartSkillCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (skillCooldownTimer > 0)
        {
            skillCooldownTimer -= Time.deltaTime;
        }
    }

    public virtual bool CanUse()
    {
        return skillCooldownTimer <= 0;
    }

    protected bool withinIdleRange()
    {
        return enemyMovement.StopCriteraFufilled();
    }

    protected Vector2 getProjectileSpawnPoint()
    {
        return enemyCombat.EnemyProjectileSpawnPoint.position;
    }

    // Gets the direction from spawnpoint of the projectile to the center of the player's hitbox.
    protected Vector2 getRangedAimDirection()
    {
        Vector2 offset = General.Instance.PlayerHitboxOffset;
        return General.Instance.GetDirectionUnitVector(getProjectileSpawnPoint(),
            (Vector2) player.transform.position + offset);
    }

    protected float getRangedAimDistance()
    {
        Vector2 offset = General.Instance.PlayerHitboxOffset;
        return Vector2.Distance(getProjectileSpawnPoint(), (Vector2)player.transform.position + offset);
    }

    public virtual void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        enemy.lockoutDuration = enemySkillBasicStats.SkillDuration;
        enemy.AttackLockoutDuration = enemySkillBasicStats.SkillDuration +
        Random.Range(enemySkillBasicStats.minAttackLockoutDuration, enemySkillBasicStats.maxAttackLockoutDuration);
        skillCooldownTimer = enemySkillBasicStats.SkillDuration +
            Random.Range(enemySkillBasicStats.MinSkillCooldown, enemySkillBasicStats.MaxSkillCooldown);
        StartCoroutine(adjustDamageMultiplierDuringSkill());
        anim.SetTrigger(enemySkillBasicStats.name);

        if (followupSkill != null)
        {
            StartCoroutine(AttemptFollowupSkill(enemy, player));
        }
    }

    protected IEnumerator adjustDamageMultiplierDuringSkill()
    {
        float amountToAdd = enemyCombat.CurrentAbilityDamageMultiplier * (enemySkillBasicStats.DamageMultiplier - 1);
        enemyCombat.CurrentAbilityDamageMultiplier += amountToAdd;
        yield return new WaitForSeconds(enemySkillBasicStats.SkillDuration);
        enemyCombat.CurrentAbilityDamageMultiplier -= amountToAdd;
    }

    private IEnumerator AttemptFollowupSkill(EnemyMain enemy, PlayerMain player)
    {
        yield return new WaitForSeconds(enemySkillBasicStats.SkillDuration);
        if (followupSkill.CanUse())
        {
            followupSkill.ExecuteSkill(enemy, player);
        }
    }
}
