using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : Healthbar
{ 
    private void Start()
    {
        low = high;
    }

    public override void SetHealth(float health, float maxHealth)
    {
        base.SetHealth(health, maxHealth);
        alignHealthbar();
    }

    void alignHealthbar()
    {
        if (transform.parent.localScale.x < 0)
        {
            Vector3 healthbarCurrentScale = this.transform.localScale;
            healthbarCurrentScale.x *= -1;
            this.transform.localScale = healthbarCurrentScale;
        }
    }
}
