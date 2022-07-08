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
        Debug.Log("scene loaded");
        if (scene.buildIndex != 1)
        {
            return;
        }

        IEnumerable<ISavable> savableObjects = FindObjectsOfType<MonoBehaviour>().OfType<ISavable>();
        foreach (ISavable saveableObject in savableObjects)
        {
            objectsToSave.Add(saveableObject);
        }

        if (isNewSave)
        {
            SaveGame();
        }
        else
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.json";
        FileStream stream = new FileStream(path, FileMode.Create);

        // SaveData data = General.Instance.Player.GetComponent<PlayerMain>().GenerateSaveData();
        SaveData saveData = new SaveData();

        foreach (ISavable savable in objectsToSave)
        {
            savable.SaveData(saveData);
        }

        formatter.Serialize(stream, saveData);
        stream.Close();
    }

    public void LoadGame()
    {
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
            return;
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
        // StartCoroutine(saveDataOnSceneChange());
    }

    IEnumerator saveDataOnSceneChange()
    {
        yield return new WaitForSeconds(0.1f);
        SaveGame();
    }

    public void ResumeGame()
    {
        isNewSave = false;
        SceneManager.LoadScene(1);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        isGamePaused = false;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
