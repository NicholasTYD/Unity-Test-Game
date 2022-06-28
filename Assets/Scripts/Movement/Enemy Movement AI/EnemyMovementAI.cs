using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovementAI : MonoBehaviour
{
    protected GameObject player;
    protected float playerBoxColliderWidth;
    protected float playerBoxColliderHeight;

    private void Start()
    {
        this.player = GameObject.FindWithTag("Player");
        playerBoxColliderWidth = this.player.GetComponent<BoxCollider2D>().size.x;
        playerBoxColliderHeight = this.player.GetComponent<BoxCollider2D>().size.y;
    }

    protected abstract bool StopCriteraFufilled();

    public virtual void Move(float speed)
    {
        this.transform.position = 
            Vector2.MoveTowards(this.transform.position, player.transform.position, Time.deltaTime * speed);
    }
}
