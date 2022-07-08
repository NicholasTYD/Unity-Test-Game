using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave3 : Wave
{
    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.Archer, pos(0));
        spawn(EnemyList.Instance.Archer, pos(1));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        spawn(EnemyList.Instance.Archer, pos(0));
        spawn(EnemyList.Instance.Archer, pos(1));

        yield return new WaitForSeconds(5);

        spawnAllOnce(EnemyList.Instance.SwdSke, waveInfo.notableSpawnPos);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        StartCoroutine(spawnRandom(EnemyList.Instance.Archer, 5, 5, 10));
        StartCoroutine(spawnRandom(EnemyList.Instance.SwdSke, 15, 2, 3));

        yield return new WaitForSeconds(10);

        StartCoroutine(concludeWaveOnKill());
    }
}
