using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Movement : MonoBehaviour
{
    protected EntityMain entityMain;
    protected Rigidbody2D entityRb;
    [SerializeField] protected float speed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.entityMain = this.GetComponent<EntityMain>();
        this.entityRb = this.GetComponent<Rigidbody2D>();

        this.speed = entityMain.GetBaseMovementSpeed();
    }

    public abstract void Move();

    protected virtual void flip()
    {
        Vector3 currentScale = this.transform.localScale;
        currentScale.x *= -1;
        this.transform.localScale = currentScale;
    }

    protected bool facingRight()
    {
        return this.transform.localScale.x > 0;
    }
}
