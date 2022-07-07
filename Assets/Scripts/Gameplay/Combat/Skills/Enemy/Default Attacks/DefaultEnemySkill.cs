using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultEnemySkill : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }
}
