using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WzdMovementAI : EnemyMovement
{
    [SerializeField] float xOffset;

    public override void Move()
    {
        if (StopCriteraFufilled() && !_forceMove)
        {
            idle();
            return;
        }

        Vector2 playerPosition = playerMain.transform.position;
        Vector2 targetPosition = playerPosition + new Vector2(xOffset, 0);
        this.transform.position =
            Vector2.MoveTowards(this.transform.position, targetPosition, Time.deltaTime * speed);
        FaceTowards(playerPosition);
    }
}
