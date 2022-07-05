using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalHazard : MonoBehaviour
{
    private float damage;
    private float lifetime;
    private float currentLifetime;

    // Update is called once per frame
    void Update()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime >= lifetime)
        {
            destroySelf();
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CombatMechanics.Instance.DealDamageTo(collision.gameObject, damage);
        }
    }

    public void SetStats(float damage, float lifetime)
    {
        this.damage = damage;
        this.lifetime = lifetime;
    }

    protected void destroySelf()
    {
        Destroy(gameObject);
    }
}
