using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    AudioSource audioSource;
    AudioListener audioListener;
    [SerializeField] AudioClip defaultBGM;

    float MUSIC_FADE_OUT_TIME = 1;
    float MUSIC_FADE_IN_TIME = 0.5f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        this.audioSource = this.GetComponent<AudioSource>();
        this.audioListener = this.GetComponent<AudioListener>();

        SceneManager.sceneLoaded += OnSceneLoaded;
        DontDestroyOnLoad(gameObject);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("test");
        if (scene.buildIndex != 1)
        {
            audioSource.Stop();
            return;
        } 

        if (audioSource.clip != defaultBGM)
        {
            Debug.Log("differentMusic");
            audioSource.clip = defaultBGM;
            
        }
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PlayClip(AudioClip audioClip)
    {
        StartCoroutine(fadeAndSwitch(audioClip));
    }

    public void RevertBackToNormalBGM()
    {
        StartCoroutine(fadeAndSwitch(defaultBGM));
    }

    IEnumerator fadeAndSwitch(AudioClip audioClip)
    {
        if (audioClip == audioSource.clip)
        {
            yield break;
        }

        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            Debug.Log(audioSource.volume);
            audioSource.volume -= startVolume * Time.deltaTime / MUSIC_FADE_OUT_TIME;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = audioClip;
        
        audioSource.Play();

        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / MUSIC_FADE_IN_TIME;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
