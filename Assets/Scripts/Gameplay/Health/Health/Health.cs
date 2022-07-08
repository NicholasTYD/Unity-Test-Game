using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    protected EntityMain entityMain;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currentHealth;
    [SerializeField] protected Healthbar healthbar;
    protected float invulnerabilityTimeLeft = 0;
    // Safe way for external methods to grant invul but not decrease the current invul time.
    public float grantInvulnerability
    {
        get { return invulnerabilityTimeLeft; }
        set { invulnerabilityTimeLeft = Mathf.Max(invulnerabilityTimeLeft, value); }
    }

    protected virtual void Update()
    {
        if (isInvulnerable())
        {
            invulnerabilityTimeLeft -= Time.deltaTime;
        }
    }

    protected virtual void changeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        updateHealthbar();
        if (currentHealth == 0)
        {
            entityMain.Die();
        }
    }

    protected bool isInvulnerable()
    {
        return invulnerabilityTimeLeft > 0;
    }

    public virtual bool TakeDamage(float amount)
    {
        if (!isInvulnerable())
        {
            changeHealth(-amount);
            return true;
        }
        return false;
    }

    public void Heal(float amount)
    {
        changeHealth(amount);
    }


    protected void setCurrentHealth(float currentHealth)
    {
        this.currentHealth = currentHealth;
        updateHealthbar();
    }

    protected void setMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        updateHealthbar();
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public bool isHealthPercentageEqualOrBelow(float percentage)
    {
        return GetHealthPercentage() <= percentage;
    }

    protected void updateHealthbar()
    {
        healthbar.SetHealth(currentHealth, maxHealth);
    }
}
