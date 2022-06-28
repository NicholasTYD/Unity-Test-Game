using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    private PlayerMain playerMain;
    private EnemyMovementAI specificEnemyMovement;

    protected override void Start()
    {
        base.Start();
        this.playerMain = GameObject.FindWithTag("Player").GetComponent<PlayerMain>();
    }

    protected override void flip()
    {
        // To be implemented
    }

    public bool isFacingCorrectDirection(Vector2 target)
    {
        float directionVectorXVal = General.GetDirectionVector(this.transform.position, target).x;
        return ((Mathf.Approximately(directionVectorXVal, 0) && facingRight()) ||
            (directionVectorXVal > 0 && facingRight()) ||
            (directionVectorXVal < 0 && !facingRight()));
    }

    public override void Move()
    {
        // specificEnemyMovement.Move();
    }
}
