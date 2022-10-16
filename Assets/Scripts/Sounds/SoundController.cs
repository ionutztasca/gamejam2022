using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    #region ---------------------------------------- Fields ----------------------------------------

    public static SoundController SharedInstance;

    // Button Sounds
    public AudioSource buttonSoundSource;

    // Enemy Sounds
    public List<AudioSource> enemyAudioSource;

    // Background Music
    public AudioSource backgroundAudioSource;

    // Volume
    public float backgroundVolume = 1.0f;
    public float audioSourceVolume = 1.0f;
    public bool audioActive = true;

    #endregion ---------------------------------------- Fields ----------------------------------------

    #region ---------------------------------------- Methods ----------------------------------------

    private void Awake()
    {
        Singleton();
    }

    public void Singleton()   // Singleton class, only one instance
    {
        if (SharedInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);   // Singleton Class, preserve the game object
                                             // between scenes because there can be only one
        }
    }

    public void PlayButtonSound()
    {
        buttonSoundSource.Play();
    }

    public void MuteSound()
    {
        backgroundVolume = 0f;
        audioSourceVolume = 0f;

        backgroundAudioSource.volume = backgroundVolume;
        for (int i = 0; i < enemyAudioSource.Count; i++)
        {
            enemyAudioSource[i].volume = 0;
        }
        buttonSoundSource.volume = audioSourceVolume;
    }

    public void UnMuteSound()
    {
        backgroundVolume = 0.5f;
        audioSourceVolume = 1.0f;

        for (int i = 0; i < enemyAudioSource.Count; i++)
        {
            enemyAudioSource[i].volume = audioSourceVolume;
        }
        buttonSoundSource.volume = audioSourceVolume;
    }

    #endregion ---------------------------------------- Methods ----------------------------------------

}