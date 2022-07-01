using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemySkill : MonoBehaviour
{
    [SerializeField] EnemySkillBasicStats enemySkillBasicStats;
    private Animator anim;
    protected float skillCooldownTimer;

    protected EnemyCombat enemyCombat;
    protected EnemyMovement enemyMovement;
    protected GameObject player;

    private void Start()
    {
        this.anim = this.GetComponent<Animator>();
        this.enemyCombat = this.GetComponent<EnemyCombat>();

        this.enemyMovement = this.GetComponent<EnemyMovement>();
        this.player = GameObject.FindWithTag("Player");
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

    public virtual void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        enemy.lockoutDuration = enemySkillBasicStats.SkillDuration;
        enemy.AttackLockoutDuration = enemySkillBasicStats.SkillDuration +
        Random.Range(enemySkillBasicStats.minAttackLockoutDuration, enemySkillBasicStats.maxAttackLockoutDuration);
        skillCooldownTimer = enemySkillBasicStats.SkillDuration +
            Random.Range(enemySkillBasicStats.MinSkillCooldown, enemySkillBasicStats.MaxSkillCooldown);
        StartCoroutine(adjustDamageMultiplierDuringSkill());
        anim.SetTrigger(enemySkillBasicStats.name);
    }

    protected IEnumerator adjustDamageMultiplierDuringSkill()
    {
        float amountToAdd = enemyCombat.CurrentAbilityDamageMultiplier * (enemySkillBasicStats.DamageMultiplier - 1);
        enemyCombat.CurrentAbilityDamageMultiplier += amountToAdd;
        yield return new WaitForSeconds(enemySkillBasicStats.SkillDuration);
        enemyCombat.CurrentAbilityDamageMultiplier -= amountToAdd;
    }
}
