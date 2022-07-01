using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreKgtBasicAttack : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && withinIdleRange();
    }
}
