using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMechanics : MonoBehaviour
{
    public static CombatMechanics Instance;
    public GameObject DamageText;
    public GameObject HealText;
    public GameObject ParryText;
    public BossHealthbar Bosshealthbar;
    public ParticleSystem spawnVfx;

    private void Awake()
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
        foreach (Collider2D entity in entities)
        {
            DealDamageTo(entity.gameObject, damage);
        }
    }

    // All damage dealing instances should call this method to instantiate the damage values.
    public void DealDamageTo(GameObject entity, float value)
    {
        Vector2 offset = new Vector2(0, 1);
        EntityMain temp = entity.GetComponent<EntityMain>();
        if(temp.TakeDamage(value))
        {
            InstantiateDamageText(value, (Vector2)entity.transform.position + offset);
        }
    }

    public void Heal(GameObject entity, float value)
    {
        Vector2 offset = new Vector2(0, 1);
        EntityMain temp = entity.GetComponent<EntityMain>();
        entity.GetComponent<EntityMain>().HealDamage(value);
        InstantiateHealText(value, (Vector2)entity.transform.position + offset);
    }

    public void InstantiateDamageText(float value, Vector2 position)
    {
        TextPopup indicator = Instantiate(DamageText, position, Quaternion.identity).GetComponent<TextPopup>();
        indicator.setText(value);
    }

    public void InstantiateHealText(float value, Vector2 position)
    {
        TextPopup indicator = Instantiate(HealText, position, Quaternion.identity).GetComponent<TextPopup>();
        indicator.setText(value);
    }

    public void InstantiateParryText(Vector2 position)
    {
        Vector2 offset = new Vector3(0, 1);
        Vector2 newPosition = position + offset;
        TextPopup indicator = Instantiate(ParryText, newPosition, Quaternion.identity).GetComponent<TextPopup>();
        indicator.setText("Parried!");
    }

    public void InstantiateProjectile(GameObject projectile, Vector2 position, Vector2 rotation,
        float damage, float speed, float lifetime)
    {
        if (projectile.GetComponent<Projectile>() == null)
        {
            Debug.Log("You're trying to instantiate something that isn't a projectile!");
            return;
        }

        float angle = Vector2.SignedAngle(Vector2.right, rotation);
        Quaternion quaternion = Quaternion.Euler(0, 0, angle);
        GameObject projectile1 = Instantiate(projectile, position, quaternion);
        projectile1.GetComponent<Projectile>().SetStats(damage, speed, lifetime);
    }

    public void InstantiateEnvHazard(GameObject envHazard, Vector2 position, float damage, float lifetime)
    {
        if (envHazard.GetComponent<EnvironmentalHazard>() == null)
        {
            Debug.Log("You're trying to instantiate something that isn't a Environmental Hazard!");
            return;
        }

        GameObject hazard = Instantiate(envHazard, position, Quaternion.identity);
        hazard.GetComponent<EnvironmentalHazard>().SetStats(damage, lifetime);
    }

    public void Spawn(GameObject enemy, Vector3 position, Quaternion quaternion)
    {
        Instantiate(enemy, position, quaternion);
        Instantiate(spawnVfx, position, quaternion);
        WaveSpawner.Instance.CurrentEnemyCount++;
    }

    public void Spawn(GameObject enemy, Vector3 position)
    {
        Instantiate(enemy, position, Quaternion.identity);
        Instantiate(spawnVfx, position, Quaternion.identity);
        WaveSpawner.Instance.CurrentEnemyCount++;
    }
}
