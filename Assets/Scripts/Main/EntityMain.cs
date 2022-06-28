using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Movement))]
public abstract class EntityMain : MonoBehaviour
{
    public BaseStatsScriptableObject BasicStats;
    protected Health health;
    protected Movement movement;
    private Animator anim;
    private Rigidbody2D rb;
    public float lockoutDuration { get; set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.health = this.GetComponent<Health>();
        this.movement = this.GetComponent<Movement>();
        this.anim = this.GetComponent<Animator>();
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!canAct())
        {
            lockoutDuration -= Time.deltaTime;
            return;
        }
    }

    void FixedUpdate()
    {
        if (canAct())
        {
            movement.Move();
        }
    }

    public void Die()
    {
        lockoutDuration = 999;
        rb.simulated = false;
        anim.SetTrigger("Die");
    }

    protected bool canAct()
    {
        return lockoutDuration <= 0;
    }

    public virtual void TakeDamage(float amount)
    {
        health.TakeDamage(amount);
    }

    public void HealDamage(float amount)
    {
        health.Heal(amount);
    }

    public float GetBaseMaxHealth()
    {
        return BasicStats.BaseMaxHealth;
    }

    public float GetBaseAttack()
    {
        return BasicStats.BaseAttack;
    }

    public float GetBaseMovementSpeed()
    {
        return BasicStats.BaseMovementSpeed;
    }
}
