using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
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
