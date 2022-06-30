using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    BossHealthbar enemyHealthbar;

    protected override void Start()
    {
        base.Start();
        enemyHealthbar = (BossHealthbar)healthbar;
        // enemyHealthbar = GameObject.FindWithTag("Boss Health UI").GetComponent<BossHealthbar>();
        toggleBossHealthbar(true);
        
    }

    protected override void changeHealth(float amount)
    {
        base.changeHealth(amount);
        if (currentHealth == 0)
        {
            toggleBossHealthbar(false);
        }
    }

    private void toggleBossHealthbar(bool input)
    {
        enemyHealthbar.gameObject.SetActive(input);
    }

    /*
    BossHealthbar enemyHealthbar;

    protected override void Start()
    {
        base.Start();
        enemyHealthbar = (BossHealthbar) base.healthbar;
    }
    */
}
