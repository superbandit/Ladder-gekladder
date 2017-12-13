using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public static SoundHandler Instance;

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
		
	}
	
	void Update ()
    {
		
	}

    void PlaySound(AudioClip clip)
    {
        foreach (AudioSource i in sources)
        {
            if(!i.isPlaying)
            {
                i.clip = clip;
                i.Play();
            }
        }
    }
}
