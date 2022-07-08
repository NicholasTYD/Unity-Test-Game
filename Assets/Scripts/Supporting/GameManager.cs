using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool isGamePaused { get; private set; }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.json";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = General.Instance.Player.GetComponent<PlayerMain>().GenerateSaveData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public SaveData LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.Log("Error, save file not found!");
            return null;
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
        SceneManager.LoadScene(1);
        General.Instance.Player.GetComponent<PlayerMain>().LoadSaveData(LoadGame());
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
}
