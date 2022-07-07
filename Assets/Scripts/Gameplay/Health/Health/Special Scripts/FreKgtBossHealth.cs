using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreKgtBossHealth : BossHealth
{
    private EnemyCombat enemyCombat;
    [SerializeField] EnemySkill specialAttack;

    protected override void Start()
    {
        base.Start();
        this.enemyCombat = this.GetComponent<EnemyCombat>();
    }

    protected override void changeHealth(float amount)
    {
        if (enemyCombat.ManuallyExecuteSkill(specialAttack))
        {
            CombatMechanics.Instance.InstantiateParryText(this.transform.position);
            return;
        }
        base.changeHealth(amount);
    }
}
