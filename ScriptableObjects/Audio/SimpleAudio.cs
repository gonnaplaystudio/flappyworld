using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "AudioEvent/Simple")]
public class SimpleAudio : AudioEvent {

    public AudioClip[] clips;

    public float volumeMin;
    public float volumeMax;

    public float pitchMin;
    public float pitchMax;

    public bool music;
    public bool fx;

    public override void Play(AudioSource audioSource)
    {
        if (clips == null || !CheckVolume())
            return;

        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.volume = Random.Range(volumeMin, volumeMax);
        audioSource.pitch = Random.Range(pitchMin, pitchMax);
        audioSource.Play();
    }

    public override void Play(AudioSource audioSource, float _pitch)
    {
        if (clips == null || !CheckVolume())
            return;

        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.volume = Random.Range(volumeMin, volumeMax);
        audioSource.pitch = _pitch;
        audioSource.Play();
    }

    public override void ChangeVolume(float newValue)
    {
        volumeMax = newValue;
        volumeMin = newValue;
    }

    public override float GetVolume()
    {
        return volumeMin;
    }

    public bool CheckVolume()
    {
        if (music)
        {
            return Convert.ToBoolean(PlayerPrefs.GetString("Music", "true"));
        }
        else
        {
            return Convert.ToBoolean(PlayerPrefs.GetString("Sound", "true"));
        }
    }
}
