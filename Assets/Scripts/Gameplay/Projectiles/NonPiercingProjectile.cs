using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPiercingProjectile : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        destroySelfIfPlayerNotRolling(collision);
    }

    void destroySelfIfPlayerNotRolling(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && 
            !collision.gameObject.GetComponent<PlayerMovement>().inRollState)
        {
            destroySelf();
        }
    }
}
