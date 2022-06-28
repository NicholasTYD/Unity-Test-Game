using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Movement : MonoBehaviour
{
    private EntityMain entityMain;
    protected Rigidbody2D entityRb;
    [SerializeField] protected float speed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.entityMain = this.GetComponent<EntityMain>();
        this.entityRb = this.GetComponent<Rigidbody2D>();

        this.speed = entityMain.GetBaseMovementSpeed();
    }

    public abstract void move();

    protected abstract void flip();

    protected bool facingRight()
    {
        return this.transform.localScale.x > 0;
    }
}
