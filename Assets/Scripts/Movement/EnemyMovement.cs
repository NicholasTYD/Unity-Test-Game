using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    private Animator enemyAnim;
    private PlayerMain playerMain;
    [SerializeField] private EnemyMovementAI enemyMovementAI;
    private Vector2 prevPos;

    protected override void Start()
    {
        base.Start();
        this.enemyAnim = this.GetComponent<Animator>();
        this.playerMain = GameObject.FindWithTag("Player").GetComponent<PlayerMain>();
    }

    private void FixedUpdate()
    {
        updateCurrentMoveSpeed();
    }

    public override void Move()
    {
        enemyMovementAI.Move(speed);
    }

    public void flipIfNeeded(Vector2 target)
    {
        if (!isFacingCorrectDirection(target))
        {
            flip();
        }
    }

    private bool isFacingCorrectDirection(Vector2 target)
    {
        float directionVectorXVal = General.GetDirectionVector(this.transform.position, target).x;
        return ((Mathf.Approximately(directionVectorXVal, 0) && facingRight()) ||
            (directionVectorXVal > 0 && facingRight()) ||
            (directionVectorXVal < 0 && !facingRight()));
    }

    private void updateCurrentMoveSpeed()
    {
        Vector2 currentPos = this.transform.position;
        float speed = Vector2.Distance(currentPos, prevPos) / Time.deltaTime;
        enemyAnim.SetFloat("CurrentMoveSpeed", speed);
        prevPos = currentPos;
    }
}
