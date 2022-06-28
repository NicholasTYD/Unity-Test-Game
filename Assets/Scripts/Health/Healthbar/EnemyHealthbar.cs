using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : Healthbar
{
    private void Start()
    {
        low = high;
    }
}
