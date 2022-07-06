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

    protected void Spawn(GameObject enemy, Vector2 pos)
    {
        CombatMechanics.Instance.Spawn(EnemyList.Instance.SprSke, pos);
    }

    protected Vector2 pos(int position)
    {
        return waveInfo.notableSpawnPos[position];
    }
}
