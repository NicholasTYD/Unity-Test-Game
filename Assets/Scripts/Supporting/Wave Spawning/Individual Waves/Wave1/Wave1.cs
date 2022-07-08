using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave1 : Wave
{
    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.SprSke, pos(0));
        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        spawn(EnemyList.Instance.SprSke, pos(0));
        spawn(EnemyList.Instance.SprSke, pos(1));
        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        spawn(EnemyList.Instance.SprSke, pos(0));
        spawn(EnemyList.Instance.SprSke, pos(1));
        spawn(EnemyList.Instance.SprSke, pos(2));

        yield return null;
        StartCoroutine(concludeWaveOnKill());
    }
}
