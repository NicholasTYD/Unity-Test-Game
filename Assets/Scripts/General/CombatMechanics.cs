using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMechanics : MonoBehaviour
{
    // OverlapBoxAll but also does dmg to specified entities in it.
    public static void DamageBoxAll(Vector2 point, Vector2 size, float angle, int layerMask, float damage)
    {
        Collider2D[] entities = Physics2D.OverlapBoxAll(point, size, angle, layerMask);
        foreach (Collider2D entity in entities)
        {
            EntityMain temp = entity.GetComponent<EntityMain>();
            temp.TakeDamage(damage);
        }
    }

    public static void DamageCircleAll(Vector2 point, float radius, int layerMask, float damage)
    {
        Collider2D[] entities = Physics2D.OverlapCircleAll(point, radius, layerMask);
        Debug.Log(point + " " + radius + " " + layerMask);
        foreach (Collider2D entity in entities)
        {
            Debug.Log("HIT: " + entity);
            // EntityMain temp = entity.GetComponent<EntityMain>();
            // temp.TakeDamage(damage);
        }
    }
}
