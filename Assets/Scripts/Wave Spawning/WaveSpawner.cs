using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] float waveTextAppearDuration;
    [SerializeField] List<Wave> waves;

    public bool waveCompleted { get; set; }
    public int currentWave { get; private set; }

    public int CurrentEnemyCount { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        waveCompleted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && waveCompleted)
        {
            initiateWave();
        } else if (Input.GetKeyDown(KeyCode.Space) && !waveCompleted)
        {
            Debug.Log("can't start!");
        }
    }

    void initiateWave()
    {
        if (currentWave > waves.Count)
        {
            Debug.Log("the end");
        }

        string text = waves[currentWave].GetWaveName();
        StartCoroutine(setWaveText(text));
        waves[currentWave].StartWave();
        waveCompleted = false;
    }

    public void ConcludeWave()
    {
        waveCompleted = true;
        string text = waves[currentWave].GetWaveName() + " Complete!";
        StartCoroutine(setWaveText(text));
        currentWave++;
    }

    public bool gotEnemiesRemaining()
    {
        return CurrentEnemyCount > 0;
    }

    IEnumerator setWaveText(string text)
    {
        waveText.text = text;
        waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(waveTextAppearDuration);
        waveText.gameObject.SetActive(false);
    }
}
