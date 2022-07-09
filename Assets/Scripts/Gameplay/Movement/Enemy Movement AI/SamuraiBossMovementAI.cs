using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiBossMovementAI : BossMovement
{
    [SerializeField] float enragedSpeed;
    private BoxCollider2D enemyBoxCollider;

    protected override void Start()
    {
        base.Start();
        this.enemyBoxCollider = this.GetComponent<BoxCollider2D>();
    }

    public void initiateDashes(EnemyMain enemy, PlayerMain player, float dashForce, List<float> attackTimings)
    {
        StartCoroutine(ExecuteDashes(enemy, player, dashForce, attackTimings));
    }

    IEnumerator ExecuteDashes(EnemyMain enemy, PlayerMain player, float dashForce, List<float> attackTimings)
    {
        float prevAttackTime = 0.0f;
        foreach (float time in attackTimings)
        {
            yield return new WaitForSeconds(time - prevAttackTime);

            FaceTowards(player.transform.position);
            Vector2 dashDirection = General.Instance.GetDirectionUnitVector(this.transform.position, General.Instance.Player.transform.position);
            entityRb.AddForce(dashDirection * dashForce, ForceMode2D.Impulse);
            prevAttackTime = time;
        }
    }

    public void SetEnragedSpeed()
    {
        speed = enragedSpeed;
    }

    public void Jump()
    {
        StartCoroutine(temporaryDisableCollision());
        entityRb.AddForce(Vector2.up * 100, ForceMode2D.Impulse);
    }

    IEnumerator temporaryDisableCollision()
    {
        enemyBoxCollider.enabled = false;
        yield return new WaitForSeconds(1);
        enemyBoxCollider.enabled = true;
    }

    public void StopDashes()
    {
        entityRb.simulated = false;
    }
}
