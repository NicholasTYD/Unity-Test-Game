using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiBossMovementAI : BossMovement
{
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
}
