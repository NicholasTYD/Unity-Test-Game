using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave7 : Wave
{
    [SerializeField] List<GameObject> sw1Enemies;

    protected override IEnumerator sw1()
    {
        StartCoroutine(spawnRandom(sw1Enemies, 10, 3, 4));

        yield return new WaitForSeconds(40);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        StartCoroutine(spawnRandom(EnemyList.Instance.RedWzd, 2, 10, 10));
        StartCoroutine(spawnRandom(sw1Enemies, 5, 5, 5));

        yield return new WaitForSeconds(25);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        StartCoroutine(spawnRandom(EnemyList.Instance.RedWzd, waveInfo.notableSpawnPos, 5, 3, 6));
        StartCoroutine(spawnRandom(EnemyList.Instance.Archer, 5, 5, 5));

        yield return new WaitForSeconds(25);

        StartCoroutine(concludeWaveOnKill());
    }
}
