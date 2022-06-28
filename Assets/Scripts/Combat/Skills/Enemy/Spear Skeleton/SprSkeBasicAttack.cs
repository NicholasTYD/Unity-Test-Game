using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprSkeBasicAttack : EnemySkill
{
    public override bool CanUse()
    {
        return base.CanUse() && distanceCheck() && yCheck();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        Debug.Log("Used skill");
        base.ExecuteSkill(enemy, player);
    }

    private bool distanceCheck()
    {
        return Vector2.Distance(player.transform.position, this.transform.position) < 1;
    }

    private bool yCheck()
    {
        float yDifference = this.transform.position.y - player.transform.position.y;
        return Mathf.Approximately(yDifference, 0) ||
            (yDifference < 0 && (yDifference > -playerBoxColliderHeight / 2));
    }
}
