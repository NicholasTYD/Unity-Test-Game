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
    protected Animator anim;
    public float lockoutDuration { get; set; }
    public bool ForceLockout { get; set; }
    public bool isDead { get; protected set; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.health = this.GetComponent<Health>();
        this.movement = this.GetComponent<Movement>();
        this.anim = this.GetComponent<Animator>();
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

    public virtual void Die()
    {
        isDead = true;
        lockoutDuration = 999;
        ForceLockout = true;
        health.grantInvulnerability = 999;
        anim.SetBool("IsDead", true);
        StartCoroutine(handleDeath());
    }

    public void SafeSetLockoutDuration(float value)
    {
        lockoutDuration = Mathf.Max(lockoutDuration, value);
    }

    protected bool canAct()
    {
        return lockoutDuration <= 0 && !isDead && !ForceLockout;
    }

    public virtual bool TakeDamage(float amount)
    {
        return health.TakeDamage(amount);
    }

    public void HealDamage(float amount)
    {
        health.Heal(amount);
    }

    public BaseStatsScriptableObject GetBaseStats()
    {
        return BasicStats;
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

    protected abstract IEnumerator handleDeath();
}
