using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Health>().ChangeHealth(-1);
    }
}
