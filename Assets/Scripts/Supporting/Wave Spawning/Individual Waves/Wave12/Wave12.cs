using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave12 : Wave
{
    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.Shaman, pos(0));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        StartCoroutine(spawnRandom(EnemyList.Instance.Shaman, 3, 20, 20));
        StartCoroutine(spawnRandom(EnemyList.Instance.Archer, 15, 2, 3));

        yield return new WaitForSeconds(40);

        spawn(EnemyList.Instance.Tengu, pos(0));
        spawn(EnemyList.Instance.Witch, pos(1));
        spawn(EnemyList.Instance.Witch, pos(2));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        spawn(EnemyList.Instance.RedWzd);
        spawn(EnemyList.Instance.PpeWzd);
        spawn(EnemyList.Instance.Witch);
        spawn(EnemyList.Instance.Hntrss);

        yield return new WaitForSeconds(5);

        spawnAllOnce(EnemyList.Instance.Tengu, waveInfo.notableSpawnPos);

        yield return new WaitForSeconds(10);

        StartCoroutine(spawnRandom(EnemyList.Instance.Shaman, 2, 20, 20));

        yield return new WaitForSeconds(20);

        StartCoroutine(concludeWaveOnKill());
    }
}
