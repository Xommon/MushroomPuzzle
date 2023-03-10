using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    [Range(0f, 1.0f)]
    public float volume;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;
    public bool loop;
    public bool music;
}
