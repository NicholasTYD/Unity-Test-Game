using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    public Transform EnemyProjectileSpawnPoint;
    private PlayerMain playerMain;
    private EnemyMain enemyMain;
    [SerializeField] List<EnemySkill> enemySkills;

    float currentAttack;
    public float CurrentAbilityDamageMultiplier { get; set; }
    
    protected override void Start()
    {
        base.Start();
        this.playerMain = GameObject.FindWithTag("Player").GetComponent<PlayerMain>();
        this.enemyMain = this.GetComponent<EnemyMain>();
        currentAttack = baseAttack;
        CurrentAbilityDamageMultiplier = 1;
    }
    public override void Attack()
    {
        foreach (EnemySkill skill in enemySkills)
        {
            if (skill.CanUse())
            {
                skill.ExecuteSkill(enemyMain, playerMain);
                break;
            }
        }
    }

    public bool ManuallyExecuteSkill(EnemySkill skill)
    {
        if (skill.CanUse())
        {
            skill.ExecuteSkill(enemyMain, playerMain);
            return true;
        }
        return false;
    }

    public void Damage(GameObject entity)
    {
        CombatMechanics.Instance.DealDamageTo(entity, GetAbilityDamage());
    }

    public float GetAbilityDamage()
    {
        return currentAttack * CurrentAbilityDamageMultiplier;
    }
}
