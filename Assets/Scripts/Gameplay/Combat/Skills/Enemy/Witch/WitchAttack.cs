using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAttack : EnemySkill
{
    [SerializeField] float offset = 1;

    public override bool CanUse()
    {
        return base.CanUse();
    }

    public override void ExecuteSkill(EnemyMain enemy, PlayerMain player)
    {
        Vector2 playerPos = player.transform.position;
        Vector2 teleportPos;
        if (player.transform.position.x - offset <= -General.Instance.StageXBound)
        {
            teleportPos = (playerPos + new Vector2(offset, 0));
        }
        else if (player.transform.position.x + offset >= General.Instance.StageXBound)
        {
            teleportPos = (playerPos - new Vector2(offset, 0));
        }
        else if (General.Instance.RandomBool())
        {
            teleportPos = (playerPos + new Vector2(offset, 0));
        } else
        {
            teleportPos = (playerPos - new Vector2(offset, 0));
        }

        this.transform.position = teleportPos;
        enemyMovement.FaceTowards(playerPos);
        base.ExecuteSkill(enemy, player);
    }
}
