using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Movement))]
public abstract class EntityMain : MonoBehaviour
{
    protected Health health;
    protected Movement movement;
    public float lockoutDuration { private get; set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.health = this.GetComponent<Health>();
        this.movement = this.GetComponent<Movement>();
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
            movement.move();
        }
    }

    protected bool canAct()
    {
        return lockoutDuration <= 0;
    }

    public void TakeDamage(float amount)
    {
        health.TakeDamage(amount);
    }

    public void HealDamage(float amount)
    {
        health.Heal(amount);
    }
}
