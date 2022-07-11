using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyHealth : Health
{
    float HEALTH_SCALING_PER_WAVE = 1.1f;
    EnemyHealthbar enemyHealthbar;

    void Start()
    {
        // base.Start();
        this.entityMain = this.GetComponent<EntityMain>();
        maxHealth = entityMain.GetBaseMaxHealth() * Mathf.Pow(HEALTH_SCALING_PER_WAVE, WaveSpawner.Instance.CurrentWave);
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
        enemyHealthbar = (EnemyHealthbar) base.healthbar;
    }

    public void AlignHealthbar()
    {
        enemyHealthbar.AlignHealthbar();
    }
}
