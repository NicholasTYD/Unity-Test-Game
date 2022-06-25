using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;
    // [SerializeField] GameObject healthContainer;
    [SerializeField] Healthbar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        // healthbar = healthContainer.GetComponent<Healthbar>();
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth, maxHealth);
    }

    public void ChangeHealth(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        healthbar.SetHealth(currentHealth, maxHealth);
    }
}
