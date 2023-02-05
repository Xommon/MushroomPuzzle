using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public Slider musicSlider;
    public Slider soundSlider;
    public float musicVolume;
    public float soundVolume;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            if (s.music)
            {
                s.source.volume = musicVolume;
            }
            else
            {
                s.source.volume = soundVolume;
            }
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //Play("Music");
    }

    private void Update()
    {
        musicVolume = musicSlider.value;
        soundVolume = soundSlider.value;
        foreach (Sound s in sounds)
        {
            if (s.music)
            {
                s.source.volume = musicVolume;
            }
            else
            {
                s.source.volume = soundVolume;
            }
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound not found.");
            return;
        }
        s.source.Play();
    }
}
