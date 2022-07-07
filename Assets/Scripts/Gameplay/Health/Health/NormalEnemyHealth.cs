using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyHealth : EnemyHealth
{
    EnemyHealthbar enemyHealthbar;

    protected override void Start()
    {
        base.Start();
        enemyHealthbar = (EnemyHealthbar) base.healthbar;
    }

    public void AlignHealthbar()
    {
        enemyHealthbar.AlignHealthbar();
    }
}
