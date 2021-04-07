using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicAudioSource;

    public void Play(AudioClip clip)
    {
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
    internal void Pause()
    {
        musicAudioSource.Pause();
    }

    internal void UnPause()
    {
        musicAudioSource.UnPause();
    }
}
