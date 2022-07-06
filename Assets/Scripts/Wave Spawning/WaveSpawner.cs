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

    public static bool waveCompleted { get; set; }
    private static int currentWave;

    public static int CurrentEnemyCount { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && waveCompleted)
        {
            initiateWave();
        }
    }

    void initiateWave()
    {
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
