using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float speed;
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
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * Time.deltaTime * speed, this.transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CombatMechanics.Instance.DealDamageTo(collision.gameObject, damage);
        destroySelf();
    }

    public void SetStats(float damage, float speed, float lifetime)
    {
        this.damage = damage;
        this.speed = speed;
        this.lifetime = lifetime;
    }

    private void destroySelf()
    {
        Destroy(gameObject);
    }
}
