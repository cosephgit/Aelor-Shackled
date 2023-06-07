using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// * Date edited:     7/6/2023 by Seph
 
public class SoundSystemManager : MonoBehaviour {
    
    public static SoundSystemManager instance;

    [Header("AUDIO")]
    public AudioClip[] sfxClips;
    public AudioClip[] musicClips;
    private AudioSource[] sfxPool;
    public AudioSource musicSource;
    public AudioSource footstepSource;
    public int maxSFXSources = 10;
    private int currentSFX;
    public AudioSource multi;
    public static float multPitch = .85f;
    public float basePitch = .85f;
    public bool pitchInc = false;

    void Awake() {
        // Seph we don't want DoNotDestroyOnLoad here it does nothing useful and causes problems
        // rewritten to avoid some potential problems
        if (instance)
        {
            if (instance != this)
            {
                Destroy(gameObject);
                return;
            }
        }
        else
            instance = this;
    }

    void Start() {
        AudioSource multi = gameObject.AddComponent<AudioSource>();
        sfxPool = new AudioSource[maxSFXSources];
        for (int i = 0; i < maxSFXSources; i++) {
            GameObject g = new GameObject("sfx" + i);
            AudioSource sfx = g.AddComponent<AudioSource>();
            sfx.gameObject.transform.SetParent(transform);
            sfx.playOnAwake = false;
            sfxPool[i] = sfx;
        }
    }

    public void PlayVariedSFX(string clipName) {
        PlaySFX(clipName, Random.Range(0.95f, 1.25f), 1);
    }

    public void PlaySFXStandard(string clipName) {
        PlaySFX(clipName, 1, 3);
    }

    public void PlaySFXLouder(string clipName) {
        PlaySFX(clipName, 1, 10);
    }

    public void PlaySFX(string clipName, float pitch, float volume) {
        for (int i = 0; i < sfxClips.Length; i++) {
            if (clipName == sfxClips[i].name) {
                AudioSource sfx = sfxPool[currentSFX];
                sfx.clip = sfxClips[i];
                sfx.pitch = pitch;
                sfx.volume = volume;
                sfx.Play();
                currentSFX++;
                currentSFX %= maxSFXSources;
                break;
            }
        }
    }

    public void PlayMusic(string clipName) {
        for (int i = 0; i < musicClips.Length; i++) {
            if (clipName == musicClips[i].name) {
                musicSource.clip = musicClips[i];
                musicSource.Play();
                break;
            }
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
