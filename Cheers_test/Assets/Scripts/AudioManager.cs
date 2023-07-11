using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
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

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " was not found!");
            return;
        }
        s.source.Play();
    }
    public void PlayRandomDeathSound()
    {
        int i = UnityEngine.Random.Range(0, 4);
        switch (i)
        {
            case 0:
                FindObjectOfType<AudioManager>().Play("death2");
                break;
            case 1:
                FindObjectOfType<AudioManager>().Play("death3");
                break;
            case 2:
                FindObjectOfType<AudioManager>().Play("death4");
                break;
            case 3:
                FindObjectOfType<AudioManager>().Play("death5");
                break;
            default:
                FindObjectOfType<AudioManager>().Play("death1");
                break;
        }
    }
}