using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave11 : Wave
{
    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.Hntrss, pos(0));
        spawnAllOnce(EnemyList.Instance.Tengu, waveInfo.notableSpawnPos.GetRange(1, 2));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        spawn(EnemyList.Instance.Tengu, pos(2));
        StartCoroutine(spawnRandom(EnemyList.Instance.RedWzd, 3, 15, 15));
        StartCoroutine(spawnRandom(EnemyList.Instance.PpeWzd, 3, 15, 15));

        yield return new WaitForSeconds(45);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        spawnAllOnce(EnemyList.Instance.Tengu, waveInfo.notableSpawnPos.GetRange(0, 4));
        StartCoroutine(spawnRandom(EnemyList.Instance.Witch, waveInfo.notableSpawnPos.GetRange(4, 1), 3, 15, 20));

        yield return new WaitForSeconds(40);

        spawn(EnemyList.Instance.Hntrss);

        yield return new WaitForSeconds(10);

        spawn(EnemyList.Instance.Tengu);

        StartCoroutine(concludeWaveOnKill());
    }
}
