using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{
    public WaveInfoScriptableObject waveInfo;

    float bossWaveEndSlowdownTime = 1;
    float bossWaveEndTimeScale = 0.33f;

    public virtual void StartWave()
    {
        StartCoroutine(sw1());
    }

    protected abstract IEnumerator sw1();

    public string GetWaveName()
    {
        return waveInfo.waveName;
    }

    protected void endWave()
    {
        WaveSpawner.Instance.ConcludeWave();
    }

    protected void endBossWave()
    {
        WaveSpawner.Instance.ConcludeBossWave();
    }

    protected IEnumerator concludeWaveOnKill()
    {
        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        endWave();
    }

    protected IEnumerator concludeBossWaveOnKill()
    {
        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(0.1f);
        }

        Time.timeScale = bossWaveEndTimeScale;

        yield return new WaitForSeconds(bossWaveEndSlowdownTime);

        Time.timeScale = 1;

        endBossWave();
    }

    protected bool gotEnemiesRemaining()
    {
        return WaveSpawner.Instance.gotEnemiesRemaining();
    }

    protected Vector2 pos(int position)
    {
        return waveInfo.notableSpawnPos[position];
    }

    protected void spawn(GameObject enemy)
    {
        spawn(enemy, General.Instance.GetRandomPosition());
    }

    protected void spawn(GameObject enemy, Vector2 pos)
    {
        CombatMechanics.Instance.Spawn(enemy, pos);
    }

    // Spawns an enemy on all the positions inputted once.
    protected void spawnAllOnce(GameObject enemy, List<Vector2> posList)
    {
        foreach (Vector2 pos in posList)
        {
            spawn(enemy, pos);
        }
    }

    // Spawns an enemy on all the positions inputted a specified number of times at a specified interval.
    protected IEnumerator spawnAll(GameObject enemy, List<Vector2> posList, int amount, float minInterval, float maxInterval)
    {
        for (int i = 0; i < amount; i++)
        {
            foreach (Vector2 pos in posList)
            {
                spawn(enemy, pos);
            }
            float spawnInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Spawns a random enemy a specified number of times at a specified interval.
    protected IEnumerator spawnRandom(GameObject enemy, int amount, float minInterval, float maxInterval)
    {
        for (int i = 0; i < amount; i++)
        {
            spawn(enemy, General.Instance.GetRandomPosition());
            float spawnInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Spawns a enemy on a random pos in the posList for a specified number of times at a specified interval.
    protected IEnumerator spawnRandom(GameObject enemy, List<Vector2> posList, int amount, float minInterval, float maxInterval)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector2 selectedPos = posList[Random.Range(0, posList.Count)];
            spawn(enemy, selectedPos);
            float spawnInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Spawns a random enemy on a random pos in the posList for a specified number of times at a specified interval.
    protected IEnumerator spawnRandom(List<GameObject> enemies, List<Vector2> posList, int amount, float minInterval, float maxInterval)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject selectedEnemy = enemies[Random.Range(0, enemies.Count)];
            Vector2 selectedPos = posList[Random.Range(0, posList.Count)];
            spawn(selectedEnemy, selectedPos);
            float spawnInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Spawns a random enemy on a random pos in the map for a specified number of times at a specified interval.
    protected IEnumerator spawnRandom(List<GameObject> enemies, int amount, float minInterval, float maxInterval)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject selectedEnemy = enemies[Random.Range(0, enemies.Count)];
            Vector2 selectedPos = General.Instance.GetRandomPosition();
            spawn(selectedEnemy, selectedPos);
            float spawnInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    
}
