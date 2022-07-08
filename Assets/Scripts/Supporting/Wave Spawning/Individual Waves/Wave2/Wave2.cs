using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave2 : Wave
{
    [SerializeField] List<GameObject> sw3Enemies;

    protected override IEnumerator sw1()
    {
        spawnAllOnce(EnemyList.Instance.SprSke, waveInfo.notableSpawnPos);
        
        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw2());
    }

    IEnumerator sw2()
    {
        spawn(EnemyList.Instance.SwdSke, pos(0));
        spawn(EnemyList.Instance.SwdSke, pos(1));

        yield return new WaitForSeconds(4);

        spawnAllOnce(EnemyList.Instance.SprSke, waveInfo.notableSpawnPos);

        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(sw3());
    }

    IEnumerator sw3()
    {
        StartCoroutine(spawnRandom(sw3Enemies, 10, 2, 2));

        yield return new WaitForSeconds(20);

        StartCoroutine(sw4());
    }

    IEnumerator sw4()
    {
        spawnAllOnce(EnemyList.Instance.SwdSke, waveInfo.notableSpawnPos);

        yield return null;
        StartCoroutine(concludeWaveOnKill());
    }
}
