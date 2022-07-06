using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiMeteor : MonoBehaviour
{
    [SerializeField] GameObject warningCircle;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject shockwavePrefab;
    [SerializeField] List<Vector2> shockwaveDirections;

    CircleCollider2D damageHurtbox;
    
    float warningTime = 1;
    float fireballTravelTime = 0.5f;
    float circleColliderActiveTime = 0.5f;
    float destroyAfterSecondsPastExplosion = 2;

    float meteorDamage;
    float shockwaveDamage;
    float shockwaveSpeed;
    float shockwaveLifetime;

    private void Start()
    {
        this.damageHurtbox = this.GetComponent<CircleCollider2D>();
        StartCoroutine(this.executeAttack());
    }

    public void SetStats(float meteorDamage, float shockwaveDamage, float shockwaveSpeed, float shockwaveLifetime)
    {
        this.meteorDamage = meteorDamage;
        this.shockwaveDamage = shockwaveDamage;
        this.shockwaveSpeed = shockwaveSpeed;
        this.shockwaveLifetime = shockwaveLifetime;
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CombatMechanics.Instance.DealDamageTo(collision.gameObject, meteorDamage);
        }
    }

    protected void destroySelf()
    {
        Destroy(gameObject);
    }

    IEnumerator executeAttack()
    {
        yield return new WaitForSeconds(warningTime);

        fireball.SetActive(true);

        yield return new WaitForSeconds(fireballTravelTime);
        warningCircle.SetActive(false);
        fireball.SetActive(false);
        explosion.SetActive(true);
        damageHurtbox.enabled = true;
        foreach (Vector2 dir in shockwaveDirections)
        {
            CombatMechanics.Instance.InstantiateProjectile(shockwavePrefab, this.transform.position, dir,
                shockwaveDamage, shockwaveSpeed, shockwaveLifetime);
        }

        yield return new WaitForSeconds(circleColliderActiveTime);
        damageHurtbox.enabled = false;
        explosion.SetActive(false);

        yield return new WaitForSeconds(destroyAfterSecondsPastExplosion - circleColliderActiveTime);
        destroySelf();
    }
}
