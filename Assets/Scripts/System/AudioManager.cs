using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : PersistentSingleton<AudioManager>
{
    [SerializeField] AudioSource sFXPlayer;

    // Used for UI SFX
    public void PlaySFX(AudioClip audioClip, float volume)
    {
        sFXPlayer.PlayOneShot(audioClip, volume);
    }
}