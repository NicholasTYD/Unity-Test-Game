using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
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
            }
        }
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
