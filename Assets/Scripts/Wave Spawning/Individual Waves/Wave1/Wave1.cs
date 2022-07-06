using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave1 : Wave
{
    public override void StartWave()
    {
        StartCoroutine(sw1());
    }

    IEnumerator sw1()
    {
        Spawn(EnemyList.Instance.SprSke, pos(0));
        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        Spawn(EnemyList.Instance.SprSke, pos(0));
        Spawn(EnemyList.Instance.SprSke, pos(1));
        Debug.Log(pos(0));
        Debug.Log(pos(1));
        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        Spawn(EnemyList.Instance.SprSke, pos(0));
        Spawn(EnemyList.Instance.SprSke, pos(1));
        Spawn(EnemyList.Instance.SprSke, pos(2));

        yield return null;
        StartCoroutine(concludeWaveOnKill());
    }
}
