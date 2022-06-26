using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMain))]
public class PlayerHealth : Health
{
    private PlayerMain playerMain;
    private Animator playerAnim;
    private PlayerCombat playerCombat;
    // 11 samples, 3 frames
    private float staggerDuration = 1f / 11 * 3;
    [SerializeField] private float hurtInvulnerabilityDuration;
    [SerializeField] private float blockInvulnerabilityDuration;

    protected override void Start()
    {
        base.Start();
        this.playerMain = this.GetComponent<PlayerMain>();
        this.playerAnim = this.GetComponent<Animator>();
        this.playerCombat = this.GetComponent<PlayerCombat>();
    }

    public override void TakeDamage(float amount)
    {
        if (!isInvulnerable())
        {
            if (playerCombat.Parried())
            {
                grantInvulnerability = blockInvulnerabilityDuration;
                return;
            }
            base.TakeDamage(amount);
            playerMain.lockoutDuration = staggerDuration;
            grantInvulnerability = hurtInvulnerabilityDuration;
            playerAnim.SetTrigger("Hurt");
            playerCombat.interruptCombat();
        }
    }
}
