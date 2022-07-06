using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{
    public WaveInfoScriptableObject waveInfo;

    public abstract void StartWave();

    public string GetWaveName()
    {
        return waveInfo.waveName;
    }

    protected void endWave()
    {
        WaveSpawner.Instance.ConcludeWave();
    }

    protected IEnumerator concludeWaveOnKill()
    {
        while (gotEnemiesRemaining())
        {
            yield return new WaitForSeconds(1);
        }
        endWave();
    }

    protected bool gotEnemiesRemaining()
    {
        return WaveSpawner.Instance.gotEnemiesRemaining();
    }

    protected Vector2 pos(int position)
    {
        return waveInfo.notableSpawnPos[position];
    }

    protected void Spawn(GameObject enemy, Vector2 pos)
    {
        CombatMechanics.Instance.Spawn(enemy, pos);
    }

    // Spawns an enemy on all the positions inputted once.
    protected IEnumerator spawnAllOnce(GameObject enemy, List<Vector2> posList)
    {
        foreach (Vector2 pos in posList)
        {
            Spawn(enemy, pos);
        }
        yield return null;
    }

    // Spawns an enemy on all the positions inputted a specified number of times at a specified interval.
    protected IEnumerator spawnAll(GameObject enemy, List<Vector2> posList, int amount, float minInterval, float maxInterval)
    {
        for (int i = 0; i < amount; i++)
        {
            foreach (Vector2 pos in posList)
            {
                Spawn(enemy, pos);
            }
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
            Spawn(selectedEnemy, selectedPos);
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
            Spawn(selectedEnemy, selectedPos);
            float spawnInterval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    
}
