using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    BossHealthbar enemyHealthbar;

    protected override void Start()
    {
        enemyHealthbar = GameObject.FindWithTag("Boss Health UI").GetComponent<BossHealthbar>();
        toggleBossHealthbar(true);

        this.healthbar = enemyHealthbar;
        this.entityMain = this.GetComponent<EntityMain>();
        maxHealth = entityMain.GetBaseMaxHealth();
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
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
