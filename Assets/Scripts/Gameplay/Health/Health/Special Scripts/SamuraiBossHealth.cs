using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiBossHealth : BossHealth
{
    [SerializeField] ParticleSystem lowHealthVfx;
    [SerializeField] float lowHealthThreshold;

    protected override void changeHealth(float amount)
    {
        base.changeHealth(amount);
        if (isHealthPercentageEqualOrBelow(lowHealthThreshold))
        {
            lowHealthVfx.Play();
        }
    }
}
