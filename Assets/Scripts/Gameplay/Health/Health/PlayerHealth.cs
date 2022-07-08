using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMain))]
public class PlayerHealth : Health, ISavable
{
    private Animator playerAnim;
    private PlayerCombat playerCombat;
    // 11 samples, 3 frames
    private float staggerDuration = 1f / 11 * 3;
    [SerializeField] private float hurtInvulnerabilityDuration;
    [SerializeField] private float blockInvulnerabilityDuration;

    protected override void Start()
    {
        base.Start();
        this.playerAnim = this.GetComponent<Animator>();
        this.playerCombat = this.GetComponent<PlayerCombat>();
    }

    public override bool TakeDamage(float amount)
    {
        // If something with parry breaks shift the parry code into the isInvul block. Shouldn't break tho
        if (playerCombat.Parried())
        {
            grantInvulnerability = blockInvulnerabilityDuration;
            return false;
        }
        else if (!isInvulnerable())
        {
            base.TakeDamage(amount);
            entityMain.lockoutDuration = staggerDuration;
            grantInvulnerability = hurtInvulnerabilityDuration;
            playerAnim.SetTrigger("Hurt");
            playerCombat.interruptCombat();
            return true;
        }
        return false;
    }

    public void IncreaseMaxHealth(float amount)
    {
        this.maxHealth += amount;
        changeHealth(amount);
    }

    public void SaveData(SaveData saveData)
    {
        saveData.MaxHealth = maxHealth;
        saveData.CurrentHealth = currentHealth;
    }

    public void LoadData(SaveData saveData)
    {
        setMaxHealth(saveData.MaxHealth);

        // Restores character to full health on death/save load.
        // Too punishing to just restore back to almost death levels of health.
        setCurrentHealth(saveData.MaxHealth);

        // setCurrentHealth(saveData.CurrentHealth);
    }
}
