using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave9 : Wave
{
    [SerializeField] List<GameObject> sw3Enemies;

    protected override IEnumerator sw1()
    {
        StartCoroutine(spawnRandom(EnemyList.Instance.RedWzd, 2, 15, 15));
        StartCoroutine(spawnRandom(EnemyList.Instance.PpeWzd, 2, 15, 15));
        StartCoroutine(spawnRandom(EnemyList.Instance.Witch, 3, 9, 9));

        yield return new WaitForSeconds(18);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        spawnAllOnce(EnemyList.Instance.Hntrss, waveInfo.notableSpawnPos);

        yield return new WaitForSeconds(5);

        StartCoroutine(spawnRandom(EnemyList.Instance.Archer, 5, 0, 4));
        StartCoroutine(spawnRandom(EnemyList.Instance.Pile, 2, 10, 10));

        yield return new WaitForSeconds(16);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        StartCoroutine(spawnRandom(sw3Enemies, 10, 7, 8));

        yield return new WaitForSeconds(80);

        StartCoroutine(concludeWaveOnKill());
    }
}
