using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprSkeMovementAI : EnemyMovementAI
{
    protected override bool StopCriteraFufilled()
    {
        return distanceCheck() && yCheck();
    }

    public override void Move(float speed)
    {
        if (StopCriteraFufilled())
        {
            enemyAnim.SetFloat("MoveSpeed", 0);
            return;
        }
        base.Move(speed);
    }

    private bool distanceCheck()
    {
        return Vector2.Distance(player.transform.position, this.transform.position) < 1;
    }

    private bool yCheck()
    {
        float yDifference = this.transform.position.y - player.transform.position.y;
        return Mathf.Approximately(yDifference, 0) || 
            (yDifference < 0 && (yDifference > - playerBoxColliderHeight/2));
    }
}
