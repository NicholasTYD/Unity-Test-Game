using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave4 : Wave
{
    [SerializeField] List<GameObject> sw1Enemies;
    [SerializeField] List<GameObject> sw2Enemies;

    protected override IEnumerator sw1()
    {
        StartCoroutine(spawnRandom(sw1Enemies, 15, 1, 2));

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        spawn(EnemyList.Instance.Pile, pos(0));

        yield return new WaitForSeconds(5);

        StartCoroutine(spawnRandom(sw2Enemies, 15, 2, 2));

        yield return new WaitForSeconds(32);

        spawnAllOnce(EnemyList.Instance.Pile, waveInfo.notableSpawnPos);

        StartCoroutine(concludeWaveOnKill());
    }
}
