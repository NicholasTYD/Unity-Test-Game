using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : Healthbar
{
    Vector3 healthbarCurrentScale;

    private void Start()
    {
        healthbarCurrentScale = this.transform.localScale;
        low = high;
    }

    public override void SetHealth(float health, float maxHealth)
    {
        base.SetHealth(health, maxHealth);
        AlignHealthbar();
    }

    public void AlignHealthbar()
    {
        if (!isHealthbarAlignedCorrectly())
        {
            healthbarCurrentScale.x *= -1;
            this.transform.localScale = healthbarCurrentScale;
        }
    }

    bool isHealthbarAlignedCorrectly()
    {
        return (healthbarCurrentScale.x >= 0 && transform.parent.localScale.x >= 0) ||
            (healthbarCurrentScale.x < 0 && transform.parent.localScale.x < 0);
    }
}
