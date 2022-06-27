using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMechanics : MonoBehaviour
{
    public static CombatMechanics Instance;
    public GameObject damageText;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // OverlapBoxAll but also does dmg to specified entities in it.
    public void DamageBoxAll(Vector2 point, Vector2 size, float angle, int layerMask, float damage)
    {
        Collider2D[] entities = Physics2D.OverlapBoxAll(point, size, angle, layerMask);
        foreach (Collider2D entity in entities)
        {
            EntityMain temp = entity.GetComponent<EntityMain>();
            temp.TakeDamage(damage);
        }
    }

    public void DamageCircleAll(Vector2 point, float radius, int layerMask, float damage)
    {
        Collider2D[] entities = Physics2D.OverlapCircleAll(point, radius, layerMask);
        Debug.Log(point + " " + radius + " " + layerMask);
        foreach (Collider2D entity in entities)
        {
            DealDamageTo(entity.gameObject, damage);
            Debug.Log("Dealt " + damage + " damage to "+ entity);;
        }
    }

    public void DealDamageTo(GameObject entity, float value)
    {
        EntityMain temp = entity.GetComponent<EntityMain>();
        // temp.TakeDamage(damage); (Make it return a boolean)
        InstantiateDamageText(value, entity.transform.position);
    }

    public void InstantiateDamageText(float value, Vector2 position)
    {
        DamageIndicator indicator = Instantiate(damageText, position, Quaternion.identity).GetComponent<DamageIndicator>();
        indicator.setDamageText(value);
    }
}
