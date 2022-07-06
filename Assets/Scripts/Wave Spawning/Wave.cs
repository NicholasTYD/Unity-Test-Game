using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave : MonoBehaviour
{
    public WaveInfoScriptableObject waveInfo;

    public abstract void StartWave();

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

    public string GetWaveName()
    {
        return waveInfo.waveName;
    }
}
