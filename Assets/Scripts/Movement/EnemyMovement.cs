using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    private BaseStatsScriptableObject baseStats;
    private Animator enemyAnim;
    private PlayerMain playerMain;
    private EnemyHealth enemyHealth;
    // [SerializeField] private EnemyMovementAI enemyMovementAI;
    private Vector2 prevPos;

    protected override void Start()
    {
        base.Start();
        this.baseStats = entityMain.GetBaseStats();
        this.enemyAnim = this.GetComponent<Animator>();
        this.enemyHealth = this.GetComponent<EnemyHealth>();
        this.playerMain = GameObject.FindWithTag("Player").GetComponent<PlayerMain>();
    }

    private void FixedUpdate()
    {
        updateCurrentMoveSpeed();
    }

    public override void Move()
    {
        /*
        enemyMovementAI.Move(speed);
        */
        if (StopCriteraFufilled())
        {
            idle();
            return;
        }

        Vector2 playerPosition = playerMain.transform.position;
        this.transform.position =
            Vector2.MoveTowards(this.transform.position, playerPosition, Time.deltaTime * speed);
        FaceTowards(playerPosition);
    }

    public void FaceTowards(Vector2 target)
    {
        if (!isFacingCorrectDirection(target))
        {
            flip();
        }
    }

    public bool StopCriteraFufilled()
    {
        return playerDistanceWithin(baseStats.maxAllowableDistance) &&
            enemyToPlayerXDifferenceWithin(baseStats.minXDifference, baseStats.maxXDifference) &&
            enemyToPlayerYDifferenceWithin(baseStats.minYDifference, baseStats.maxYDifference);
    }

    protected void idle()
    {
        FaceTowards(playerMain.transform.position);
    }

    protected override void flip()
    {
        base.flip();
        enemyHealth.AlignHealthbar();
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

    public bool playerDistanceWithin(float value)
    {
        return Vector2.Distance(playerMain.transform.position, this.transform.position) < value;
    }

    public bool enemyToPlayerXDifferenceWithin(float min, float max)
    {
        float xDifference = this.transform.position.x - playerMain.transform.position.x;
        if (min == 0 || max == 0)
        {
            return Mathf.Approximately(xDifference, 0) ||
            (min <= xDifference && xDifference <= max);
        }
        else
        {
            return min <= xDifference && xDifference <= max;
        }
    }

    public bool enemyToPlayerYDifferenceWithin(float min, float max)
    {
        float yDifference = this.transform.position.y - playerMain.transform.position.y;
        if (min == 0 || max == 0)
        {
            return Mathf.Approximately(yDifference, 0) ||
            (min <= yDifference && yDifference <= max);
        }
        else
        {
            return min <= yDifference && yDifference <= max;
        }
    }
}
