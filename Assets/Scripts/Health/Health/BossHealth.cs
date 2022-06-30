using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    BossHealthbar enemyHealthbar;

    protected override void Start()
    {
        base.Start();
        enemyHealthbar = (BossHealthbar) base.healthbar;
    }
}
