using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    float healthScalingPerWave = 1.1f;

    // Total override
    protected override void Start()
    {
        this.entityMain = this.GetComponent<EntityMain>();
        maxHealth = entityMain.GetBaseMaxHealth() * Mathf.Pow(healthScalingPerWave, WaveSpawner.Instance.currentWave);
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
    }
}
