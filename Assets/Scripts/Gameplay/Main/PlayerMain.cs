using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerCombat))]
public class PlayerMain : EntityMain
{
    Animator playerAnim;
    PlayerCombat playerCombat;
    PlayerMovement playerMovement;
    PlayerHealth playerHealth;
    float timeBeforeRespawn = 3;

    protected override void Start()
    {
        base.Start();
        this.playerCombat = this.GetComponent<PlayerCombat>();
        this.playerMovement = this.GetComponent<PlayerMovement>();
        this.playerHealth = this.GetComponent<PlayerHealth>();

        General.Instance.Player = this.gameObject;
    }

    protected override void Update()
    {
        base.Update();

        if (!canAct())
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            playerCombat.Attack();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Roll cooldowns governed by the playerMovement script.
            playerMovement.Roll();
        }

        if (Input.GetMouseButton(1))
        {
            playerCombat.Block();
        }
    }

    private void FixedUpdate()
    {
        if (canAct())
        {
            movement.Move();
        }
    }

    public override void Die()
    {
        base.Die();
        StartCoroutine(handleDeath());
    }

    public void IncreaseMaxHealth(float amount)
    {
        playerHealth.IncreaseMaxHealth(amount);
    }

    public void IncreaseAttack(float amount)
    {
        playerCombat.IncreaseAttack(amount);
    }

    public void IncreaseAttackSpeed(float amount)
    {
        playerCombat.IncreaseAttackSpeed(amount);
    }

    public void IncreaseParryDamageBonusDuration(float amount)
    {
        playerCombat.IncreaseParryDamageBonusDuration(amount);
    }

    public void IncreaseParryDamageBonusMultiplier(float amount)
    {
        playerCombat.IncreaseParryDamageBonusMultiplier(amount);
    }

    public void IncreaseMovementSpeed(float amount)
    {
        playerMovement.IncreaseMovementSpeed(amount);
    }

    public void IncreaseComboTime()
    {
        playerCombat.UpgradeComboTime();
    }

    public void UnlockParryStrike()
    {
        playerCombat.parryStrikeUnlocked = true;
    }

    protected override IEnumerator handleDeath()
    {
        yield return new WaitForSeconds(timeBeforeRespawn);
        GameManager.Instance.ResumeGame();
    }
}
