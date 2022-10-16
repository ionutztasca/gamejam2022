using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    #region ---------------------------------------- Fields ----------------------------------------

    // Fruit Sounds
    [SerializeField] private List<AudioSource> playerRandomAudioSource;

    #endregion ------------------------------------- Fields ----------------------------------------

    #region ---------------------------------------- Fields ----------------------------------------

    public void PlayPlayerSound()
    {
        playerRandomAudioSource[Random.Range(0, playerRandomAudioSource.Count)].Play();
    }

    #endregion ------------------------------------- Fields ----------------------------------------

}
