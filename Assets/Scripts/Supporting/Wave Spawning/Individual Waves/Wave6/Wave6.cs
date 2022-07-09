using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave6 : Wave
{
    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.Witch, pos(0));

        yield return new WaitForSeconds(5);

        StartCoroutine(spawnRandom(EnemyList.Instance.SprSke, 5, 0, 0));

        yield return new WaitForSeconds(10);

        StartCoroutine(spawnRandom(EnemyList.Instance.SwdSke, 5, 0, 0));

        yield return new WaitForSeconds(5);

        StartCoroutine(spawnRandom(EnemyList.Instance.Pile, 2, 0, 0));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        StartCoroutine(spawnRandom(EnemyList.Instance.Pile, 3, 2, 2));
        StartCoroutine(spawnRandom(EnemyList.Instance.Archer, 2, 3, 3));
        StartCoroutine(spawnRandom(EnemyList.Instance.SwdSke, 5, 1, 2));

        yield return new WaitForSeconds(10);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        spawnAllOnce(EnemyList.Instance.Witch, waveInfo.notableSpawnPos.GetRange(1, 2));

        yield return new WaitForSeconds(2);

        spawnAllOnce(EnemyList.Instance.Archer, waveInfo.notableSpawnPos);

        StartCoroutine(concludeWaveOnKill());
    }
}
