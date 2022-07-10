using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave14 : Wave
{
    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.Shaman, pos(0));
        spawnAllOnce(EnemyList.Instance.Tengu, waveInfo.notableSpawnPos.GetRange(1, 2));
        spawnAllOnce(EnemyList.Instance.Sniper, waveInfo.notableSpawnPos.GetRange(3, 3));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        spawn(EnemyList.Instance.SprSke, pos(0));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(0.1f);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        spawnAllOnce(EnemyList.Instance.Shaman, waveInfo.notableSpawnPos.GetRange(6, 8));

        yield return new WaitForSeconds(5);

        StartCoroutine(spawnRandom(EnemyList.Instance.Shaman, 10, 1.5f, 2.5f));

        yield return new WaitForSeconds(2);

        spawn(EnemyList.Instance.Tengu);

        yield return new WaitForSeconds(5);

        spawn(EnemyList.Instance.Sniper);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw4());
    }

    IEnumerator sw4()
    {
        spawn(EnemyList.Instance.Witch, pos(14));

        yield return null;
        StartCoroutine(concludeWaveOnKill());
    }
}
