using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyMovement : EnemyMovement
{
    private EnemyHealth enemyHealth;

    protected override void Start()
    {
        base.Start();
        this.enemyHealth = this.GetComponent<EnemyHealth>();
    }

    protected override void flip()
    {
        base.flip();
        enemyHealth.AlignHealthbar();
    }
}
