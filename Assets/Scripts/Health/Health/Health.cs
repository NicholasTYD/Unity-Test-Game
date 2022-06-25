using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    [SerializeField] Healthbar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
    }

    void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthbar.SetHealth(currentHealth, maxHealth);
    }

    public void DealDamage(float amount)
    {
        ChangeHealth(-amount);
    }

    public void Heal(float amount)
    {
        ChangeHealth(amount);
    }
}
