using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TenguMovementAI : NormalEnemyMovement
{
    private bool isCharging;
    private Vector2 ChargeDirection;
    private float bonusSpeed;
    public override void Move()
    {
        if (!isCharging)
        {
            base.Move();
        }
        else
        {
            MoveTowards(ChargeDirection);
        }
    }

    public void ToggleCharge(bool input, float chargeSpeed)
    {
        isCharging = input;
        if (input)
        {
            bonusSpeed = chargeSpeed - speed;
            speed = chargeSpeed;
            ChargeDirection = General.Instance.GetDirectionVector(this.transform.position, General.Instance.Player.transform.position);
            ToggleForceMove(true);
        } else
        {
            ToggleForceMove(false);
            speed -= bonusSpeed;
        }
    }
}
