using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShamanHealth : NormalEnemyHealth
{
    [SerializeField] float percentageHealthDecayPerSecond;

    protected override void Update()
    {
        changeHealth(-(percentageHealthDecayPerSecond * maxHealth) * Time.deltaTime);
        base.Update();
    }
}
