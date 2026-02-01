using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] music, sfx;
    public AudioSource sfxSource, musicSource1, musicSource2;

    public void StartMusic(string name, string name2)
    {
        musicSource1.clip = Array.Find(music, x => x.name == name).clip;
        musicSource2.clip = Array.Find(music, x => x.name == name2).clip;
        musicSource1.Play();
        musicSource2.Play();
        musicSource1.volume = 1;
        musicSource2.volume = 0;
    }

    public void StartMusic(string name)
    {
        musicSource1.clip = Array.Find(music, x => x.name == name).clip;
        musicSource1.Play();
        musicSource1.volume = 1;
        musicSource2.volume = 0;
    }

    public void StopMusic()
    {
        musicSource1.Stop();
        musicSource2.Stop();
    }

    public void PlaySfx(string name)
    {
        sfxSource.PlayOneShot(Array.Find(sfx, x => x.name == name).clip);
    }

    public void SwitchMusicSource(bool masked)
    {
        if(masked)
        {
            musicSource1.volume = 0;
            musicSource2.volume = 1;
        }
        else
        {
            musicSource1.volume = 1;
            musicSource2.volume = 0;
        }
    }

}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}