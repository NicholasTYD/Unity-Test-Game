using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] Healthbar healthbar;
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
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (isInvulnerable())
        {
            invulnerabilityTimeLeft -= Time.deltaTime;
        }
    }

    void changeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthbar.SetHealth(currentHealth, maxHealth);
    }

    protected bool isInvulnerable()
    {
        return invulnerabilityTimeLeft > 0;
    }

    public virtual void TakeDamage(float amount)
    {
        if (!isInvulnerable())
        {
            changeHealth(-amount);
        }
    }

    public void Heal(float amount)
    {
        changeHealth(amount);
    }
}
