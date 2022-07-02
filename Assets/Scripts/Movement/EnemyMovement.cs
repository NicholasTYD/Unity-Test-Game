using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    protected bool _forceMove;
    protected BaseStatsScriptableObject baseStats;
    protected Animator enemyAnim;
    protected PlayerMain playerMain;
    protected Vector2 prevPos;

    protected override void Start()
    {
        base.Start();
        this.baseStats = entityMain.GetBaseStats();
        this.enemyAnim = this.GetComponent<Animator>();
        this.playerMain = GameObject.FindWithTag("Player").GetComponent<PlayerMain>();
    }

    private void FixedUpdate()
    {
        updateCurrentMoveSpeed();

        if (_forceMove)
        {
            Move();
        }
    }

    public override void Move()
    {
        if (StopCriteraFufilled() && !_forceMove)
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

    public void FaceAway(Vector2 target)
    {
        if (isFacingCorrectDirection(target))
        {
            flip();
        }
    }

    public virtual bool StopCriteraFufilled()
    {
        return playerDistanceWithin(baseStats.maxAllowableDistance) &&
            enemyToPlayerXDifferenceWithin(baseStats.minXDifference, baseStats.maxXDifference) &&
            enemyToPlayerYDifferenceWithin(baseStats.minYDifference, baseStats.maxYDifference);
    }

    public void ToggleForceMove(bool input)
    {
        if (input)
        {
            entityMain.ForceLockout = true;
            _forceMove = true;
        }
        else
        {
            entityMain.ForceLockout = false;
            _forceMove = false;
        }
    }

    protected void idle()
    {
        FaceTowards(playerMain.transform.position);
    }

    protected bool isFacingCorrectDirection(Vector2 target)
    {
        float directionVectorXVal = General.Instance.GetDirectionVector(this.transform.position, target).x;
        return ((Mathf.Approximately(directionVectorXVal, 0) && facingRight()) ||
            (directionVectorXVal > 0 && facingRight()) ||
            (directionVectorXVal < 0 && !facingRight()));
    }

    protected void updateCurrentMoveSpeed()
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
