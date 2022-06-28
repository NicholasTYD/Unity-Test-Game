using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtbox : MonoBehaviour
{
    private EnemyCombat enemyCombat;
    private LayerMask playerLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        enemyCombat = this.GetComponentInParent<EnemyCombat>();
        playerLayerMask = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collidedWithPlayer(collision)) {
            enemyCombat.Damage(collision.gameObject);
        }
    }

    private bool collidedWithPlayer(Collider2D collision)
    {
        return collision.gameObject.layer == playerLayerMask;
    }
}
