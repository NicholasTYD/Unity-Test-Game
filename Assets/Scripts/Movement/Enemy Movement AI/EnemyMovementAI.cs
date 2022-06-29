using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementAI : MonoBehaviour
{
    protected EnemyMovement enemyMovement;
    protected GameObject player;
    protected float playerBoxColliderWidth;
    protected float playerBoxColliderHeight;

    private void Start()
    {
        this.enemyMovement = this.GetComponent<EnemyMovement>();
        this.player = GameObject.FindWithTag("Player");
        playerBoxColliderWidth = this.player.GetComponent<BoxCollider2D>().size.x;
        playerBoxColliderHeight = this.player.GetComponent<BoxCollider2D>().size.y;
    }

    protected abstract bool StopCriteraFufilled();

    public virtual void Move(float speed)
    { 
        if (StopCriteraFufilled())
        {
            idle();
            return;
        }

        Vector2 playerPosition = player.transform.position;
        this.transform.position = 
            Vector2.MoveTowards(this.transform.position, playerPosition, Time.deltaTime * speed);
        enemyMovement.FaceTowards(playerPosition);
    }

    protected void idle()
    {
        enemyMovement.FaceTowards(player.transform.position);
    }
}
