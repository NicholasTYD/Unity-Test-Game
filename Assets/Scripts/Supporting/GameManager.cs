using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGamePaused { get; private set; }

    private List<ISavable> objectsToSave = new List<ISavable>();
    private bool isNewSave = true;
    private float prePauseTimeScale;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(saveDataOnSceneChange(scene, mode));
    }

    public void SaveGameData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.json";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData saveData = new SaveData();

        foreach (ISavable savable in objectsToSave)
        {
            savable.SaveData(saveData);
        }

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public void LoadGameData()
    {
        Debug.Log("test2");
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData saveData = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            foreach (ISavable savable in objectsToSave)
            {
                savable.LoadData(saveData);
            }
        }
        else
        {
            Debug.Log("Error, save file not found!");
        }
    }

    public void StartNewGame()
    {
        isNewSave = true;
        SceneManager.LoadScene(1);
    }

    public void ResumeGame()
    {
        SceneManager.LoadScene(1);
    }

    IEnumerator saveDataOnSceneChange(Scene scene, LoadSceneMode mode)
    {
        yield return new WaitForSeconds(0.01f);

        if (scene.buildIndex != 1)
        {
            yield break;
        }

        IEnumerable<ISavable> savableObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISavable>();
        foreach (ISavable saveableObject in savableObjects)
        {
            objectsToSave.Add(saveableObject);
        }

        if (isNewSave)
        {
            isNewSave = false;
            SaveGameData();
        }
        else
        {
            LoadGameData();
        }
    }

    public void Pause()
    {
        prePauseTimeScale = Time.timeScale;
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = prePauseTimeScale;
        isGamePaused = false;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
