using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave8 : Wave
{
    [SerializeField] List<GameObject> sw2Enemies;

    protected override IEnumerator sw1()
    {
        spawn(EnemyList.Instance.PpeWzd, waveInfo.notableSpawnPos[0]);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        StartCoroutine(spawnRandom(EnemyList.Instance.PpeWzd, 2, 10, 10));
        StartCoroutine(spawnRandom(sw2Enemies, 5, 5, 5));

        yield return new WaitForSeconds(25);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        List<Vector2> sw3SpawnPos = waveInfo.notableSpawnPos.GetRange(1, 6);
        StartCoroutine(spawnRandom(EnemyList.Instance.RedWzd, sw3SpawnPos, 5, 3, 6));
        StartCoroutine(spawnRandom(EnemyList.Instance.Archer, 5, 5, 5));

        yield return new WaitForSeconds(25);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw4());
    }

    IEnumerator sw4()
    {
        spawn(EnemyList.Instance.RedWzd, pos(6));
        spawn(EnemyList.Instance.PpeWzd, pos(7));

        yield return null;
        StartCoroutine(concludeWaveOnKill());
    }
}
