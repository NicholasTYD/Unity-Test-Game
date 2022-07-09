using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour, ISavable
{
    public static WaveSpawner Instance;
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] float waveTextAppearDuration;
    [SerializeField] List<Wave> waves;
    [SerializeField] UpgradeMenu upgradeMenu;

    float waveTextTimer;

    public bool WaveCompleted { get; set; }
    public bool UpgradesChosen { get; set; }
    public int CurrentWave { get; set; }

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

        WaveCompleted = true;
        UpgradesChosen = true;

        // CurrentWave = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (WaveCompleted && UpgradesChosen && !GameManager.Instance.isGamePaused)
            {
                initiateWave();
            }
            else if (!WaveCompleted)
            {
                setWaveText("You can't start a wave while its still in progress!");
            } 
            else if (!UpgradesChosen)
            {
                setWaveText("Choose an upgrade first!");
            }
        }

        if (waveTextTimer >= 0)
        {
            waveTextTimer -= Time.deltaTime;
        } 
        else if (waveText.gameObject.activeInHierarchy)
        {
            waveText.gameObject.SetActive(false);
        }
    }
    public bool gotEnemiesRemaining()
    {
        return CurrentEnemyCount > 0;
    }

    void initiateWave()
    {
        if (CurrentWave >= waves.Count)
        {
            Debug.Log("the end");
            return;
        }

        string text = waves[CurrentWave].GetWaveName();
        setWaveText(text);
        waves[CurrentWave].StartWave();
        WaveCompleted = false;
        UpgradesChosen = false;
    }

    public void ConcludeWave()
    {
        WaveCompleted = true;
        string text = waves[CurrentWave].GetWaveName() + " Complete!";
        setWaveText(text);
        StartCoroutine(PresentUpgrades());
    }

    public void ConcludeBossWave()
    {
        WaveCompleted = true;
        string text = "Boss wave" + " Complete!";
        setWaveText(text);
        StartCoroutine(PresentEnhancedUpgrades());
    }

    IEnumerator PresentUpgrades()
    {
        yield return new WaitForSeconds(3);
        upgradeMenu.PresentUpgrades();

        StartCoroutine(IncrementWaveAndSave());
    }

    IEnumerator PresentEnhancedUpgrades()
    {
        yield return new WaitForSeconds(3);
        upgradeMenu.PresentEnhancedUpgrades();

        StartCoroutine(IncrementWaveAndSave());
    }

    IEnumerator IncrementWaveAndSave()
    {
        yield return new WaitForSeconds(0.01f);
        CurrentWave++;
        GameManager.Instance.SaveGameData();
    }

    void setWaveText(string text)
    {
        waveText.text = text;
        waveText.gameObject.SetActive(true);
        waveTextTimer = waveTextAppearDuration;
    }

    public void SaveData(SaveData saveData)
    {
        saveData.CurrentWave = WaveSpawner.Instance.CurrentWave;
    }

    public void LoadData(SaveData saveData)
    {
        CurrentWave = saveData.CurrentWave;
        WaveCompleted = true;
        UpgradesChosen = true;
    }
}
