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

    protected override void Start()
    {
        base.Start();
        this.playerCombat = this.GetComponent<PlayerCombat>();
        this.playerMovement = this.GetComponent<PlayerMovement>();
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
    }
}
