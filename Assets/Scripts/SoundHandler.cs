using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public static SoundHandler Instance;

    public AudioClip music;

    public AudioSource[] sources;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start ()
    {

        if (!IsPlayingMusic())
        {
            PlayMusic(music);
        }
    }
	
	void Update ()
    {
		
	}

    public void PlaySound(AudioClip clip)
    {
        foreach (AudioSource i in sources)
        {
            if(!i.isPlaying)
            {
                i.clip = clip;
                i.Play();
                return;
            }
        }
    }
    public void PlayMusic(AudioClip clip)
    {
        foreach (AudioSource i in sources)
        {
            if (!i.isPlaying)
            {
                i.clip = clip;
                i.loop = true;
                i.Play();
                return;
            }
        }
    }
    public void StopMusic()
    {
        foreach(AudioSource i in sources)
        {
            if(i.loop)
            {
                i.loop = false;
                i.Stop();
            }
        }
    }

    public bool IsPlayingMusic()
    {
        foreach (AudioSource i in sources)
        {
            if (i.loop)
            {
                return true;
            }
        }
        return false;
    }
}
