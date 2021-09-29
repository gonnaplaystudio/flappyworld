using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AudioEvent : ScriptableObject {

    public abstract void Play(AudioSource audioSource);
    public abstract void Play(AudioSource audioSource, float _pitch);
    public abstract void ChangeVolume(float newValue);
    public abstract float GetVolume();
}
