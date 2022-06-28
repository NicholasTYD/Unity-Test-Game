using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : Combat
{
    float currentAttack;
    float currentAbilityDamageMultiplier;

    protected override void Start()
    {
        base.Start();
        currentAttack = baseAttack;
        currentAbilityDamageMultiplier = 1;
    }
    public override void Attack()
    {
        // To be implemented
    }

    public void Damage(GameObject entity)
    {
        CombatMechanics.Instance.DealDamageTo(entity, currentAttack * currentAbilityDamageMultiplier);
    }
}
