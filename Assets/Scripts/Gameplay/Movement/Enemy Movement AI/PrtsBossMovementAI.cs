using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrtsBossMovementAI : BossMovement
{
    private bool isGoingToArenaCenter;
    [SerializeField] float runSpeed;
    private float bonusSpeed;
    public override void Move()
    {
        if (!isGoingToArenaCenter)
        {
            base.Move();
        }
        else
        {
            MoveTo(General.Instance.StageCenter);
        }
    }

    public void ToggleMoveToCenter(bool input)
    {
        isGoingToArenaCenter = input;
        if (input)
        {
            bonusSpeed = runSpeed - speed;
            speed = runSpeed;
            ToggleForceMove(true);
        }
        else
        {
            ToggleForceMove(false);
            speed -= bonusSpeed;
        }
    }
}
