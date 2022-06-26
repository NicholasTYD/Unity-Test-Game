using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerMain : EntityMain
{
    Animator playerAnim;
    PlayerCombat playerCombat;

    protected override void Start()
    {
        base.Start();
        this.playerCombat = this.GetComponent<PlayerCombat>();
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

        if (Input.GetMouseButtonDown(1))
        {
            playerCombat.Roll();
        }
    }
}
