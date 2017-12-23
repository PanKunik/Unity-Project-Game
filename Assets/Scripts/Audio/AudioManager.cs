using UnityEngine.Audio;
using System.Collections;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    float vol = 0f;

    public static AudioManager instance;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    void Start()
    {
        FadeIn("MenuBackground");        
    }

    public void FadeIn(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        StartCoroutine(IncreaseVolume(s.source.volume, name, 5f));
    }

    public void FadeOut(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        StartCoroutine(DecreaseVolume(s.source.volume, name, 1f));
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void ChangeClip(string from, string to)
    {
        FadeOut(from);
        FadeIn(to);
    }


    protected IEnumerator IncreaseVolume(float vol, string name, float time)
    {
        float step = 1 / time / 10;

        Play(name);

        float volume = 0;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        while (volume < vol)
        {
            volume += step;
            s.source.volume = volume;
            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    protected IEnumerator DecreaseVolume(float vol, string name, float time)
    {
        float step = 1 / time / 10;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        while (vol > 0)
        {
            vol -= step;
            s.source.volume = vol;
            yield return new WaitForSeconds(0.1f);
        }

        Stop(name);

        yield return null;
    }
    // FindObjectOfType<AudioManager>().Play("PlayerDeath"); */
}
