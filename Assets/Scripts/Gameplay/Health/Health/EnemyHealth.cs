using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    float HEALTH_SCALING_PER_WAVE = 1.1f;

    protected virtual void Start()
    {
        this.entityMain = this.GetComponent<EntityMain>();
        maxHealth = entityMain.GetBaseMaxHealth() * Mathf.Pow(HEALTH_SCALING_PER_WAVE, WaveSpawner.Instance.CurrentWave);
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
    }
}
