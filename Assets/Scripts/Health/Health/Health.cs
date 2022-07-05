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

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.entityMain = this.GetComponent<EntityMain>();
        maxHealth = entityMain.GetBaseMaxHealth();
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
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
        healthbar.SetHealth(currentHealth, maxHealth);
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

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public float GetMaxtHealth()
    {
        return maxHealth;
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public bool isHealthPercentageEqualOrBelow(float percentage)
    {
        return GetHealthPercentage() <= percentage;
    }
}
