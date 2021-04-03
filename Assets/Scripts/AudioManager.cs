using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource voiceAudioSource;

    public void Play(AudioClip clip)
    {
        voiceAudioSource.clip = clip;
        voiceAudioSource.Play();
    }

    internal void Pause()
    {
        voiceAudioSource.Pause();
    }

    internal void UnPause()
    {
        voiceAudioSource.UnPause();
    }
}
