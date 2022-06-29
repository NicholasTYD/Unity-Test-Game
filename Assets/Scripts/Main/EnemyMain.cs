using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : EntityMain
{
    public float AttackLockoutDuration;
    protected PlayerMain playerMain;
    private EnemyCombat enemyCombat;

    protected override void Start()
    {
        base.Start();
        this.playerMain = GameObject.FindWithTag("Player").GetComponent<PlayerMain>();
        this.enemyCombat = this.GetComponent<EnemyCombat>();
    }

    protected override void Update()
    {
        base.Update();

        if (!canAct())
        {
            return;
        }

        if (!canAttack())
        {
            AttackLockoutDuration -= Time.deltaTime;
        }
        else
        {
            enemyCombat.Attack();
        }
    }

    void FixedUpdate()
    {
        if (canAct())
        {
            movement.Move();
        } 
    }

    protected bool canAttack()
    {
        return AttackLockoutDuration <= 0 && canAct();
    }
}
