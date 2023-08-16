using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{


    public Sound[] sounds;
    // Start is called before the first frame update
    
        public static AudioManager instance;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); 
    }
    void Start() {
        Play("Theme");
    }
    public void Play(string name) { 
   
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }

        var o = gameObject.AddComponent<AudioSource>();
        o.clip = s.clip;
        o.volume = s.volume;
        o.pitch = s.pitch;
        o.loop = s.loop;
        o.Play();
        if (!s.loop)
        {
            Destroy(o, s.clip.length);
        }
    }    
}
