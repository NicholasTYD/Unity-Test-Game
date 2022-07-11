using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyHealth : Health
{
    float HEALTH_SCALING_PER_WAVE = 1.1f;
    EnemyHealthbar enemyHealthbar;

    protected override void Start()
    {
        enemyHealthbar = (EnemyHealthbar) base.healthbar;
        base.Start();
    }

    public void AlignHealthbar()
    {
        enemyHealthbar.AlignHealthbar();
    }

    protected override float GetStartingMaxHealth()
    {
        return base.GetStartingMaxHealth() * Mathf.Pow(HEALTH_SCALING_PER_WAVE, WaveSpawner.Instance.CurrentWave);
    }
}
