using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    #region ---------------------------------------- Fields ----------------------------------------

    // Fruit Sounds
    [SerializeField] private List<AudioSource> playerRandomAudioSource;
    public AudioSource poopSound;
    public AudioSource foodSound;
    public AudioSource hehe;
    public AudioSource haa;
    public AudioSource hungry;
    public AudioSource mefood;
    public AudioSource miky;

    #endregion ------------------------------------- Fields ----------------------------------------

    #region ---------------------------------------- Fields ----------------------------------------
    public void PlayPoopSound()
    {
        poopSound.Play();
    }
    public void PlayfoodSound()
    {
        foodSound.Play();

    }
    public void Playhehe()
    {
        hehe.Play();
    }
    public void Playhaa()
    {
        haa.Play();
    }
    public void Playhungry()
    {
        hungry.Play();
    }
    public void Playmefood()
    {
        mefood.Play();
    }
    public void Playmiky()
    {
        miky.Play();
    }
    public void PlayPlayerSound()
    {
       // playerRandomAudioSource[Random.Range(0, playerRandomAudioSource.Count)].Play();
    }

    #endregion ------------------------------------- Fields ----------------------------------------

}
