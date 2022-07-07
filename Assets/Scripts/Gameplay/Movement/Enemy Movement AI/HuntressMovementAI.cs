using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntressMovementAI : NormalEnemyMovement
{
    public bool RunningAway { get; set; }
    public override void Move()
    {
        if (!RunningAway)
        {
            base.Move();
        } else
        {
            Vector2 playerPosition = playerMain.transform.position;
            this.transform.position =
                Vector2.MoveTowards(this.transform.position, playerPosition, Time.deltaTime * -speed);
            FaceAway(playerPosition);
        }
    }
}
