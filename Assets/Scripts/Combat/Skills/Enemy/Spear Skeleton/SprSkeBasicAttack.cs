using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprSkeBasicAttack : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }
}
