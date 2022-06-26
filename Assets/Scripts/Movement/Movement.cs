using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Movement : MonoBehaviour
{
    protected Rigidbody2D entityRb;
    [SerializeField] protected float speed;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.entityRb = this.GetComponent<Rigidbody2D>();
    }

    public abstract void move();

    protected abstract void flip();

    protected bool facingRight()
    {
        return this.transform.localScale.x > 0;
    }
}
