using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// manages save/load/config settings
// Created by: Seph 9/6
// Last edit by: Seph 9/6

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]private AudioMixer audioMixer;
    public float volumeMusic { get; private set; }
    public float volumeEffect { get; private set; }
    public float dialogueSpeed { get; private set; }
    public bool adventureMode { get; private set; }
    public int sceneReached { get; private set; }
    public float dialogueSlowdown { get; private set; } // this is the actual value that affects dialogue speed

    private void Awake()
    {
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

        SetVolumeMusic(PlayerPrefs.GetFloat(Global.SAVEVOLUMEMUSIC, 1f));
        SetVolumeEffect(PlayerPrefs.GetFloat(Global.SAVEVOLUMEEFFECT, 1f));
        SetDialogueSpeed(PlayerPrefs.GetFloat(Global.SAVEDIALOGUESPEED, 0.5f));
        SetAdventureMode((PlayerPrefs.GetInt(Global.SAVEADVENTUREMODE, 0) == 1));
        SetSceneReached(PlayerPrefs.GetInt(Global.SAVEPROGRESS, 0));
    }

    public void SetVolumeMusic(float value)
    {
        volumeMusic = value;
        audioMixer.SetFloat("volMusic", Global.VolToDecibels(volumeMusic));
        PlayerPrefs.SetFloat(Global.SAVEVOLUMEMUSIC, volumeMusic);
    }

    public void SetVolumeEffect(float value)
    {
        volumeEffect = value;
        audioMixer.SetFloat("volEffects", Global.VolToDecibels(volumeEffect));
        PlayerPrefs.SetFloat(Global.SAVEVOLUMEEFFECT, volumeEffect);
    }

    public void SetDialogueSpeed(float value)
    {
        dialogueSpeed = value;
        PlayerPrefs.SetFloat(Global.SAVEDIALOGUESPEED, dialogueSpeed);
        // when dialogue speed is 0, dialogue plays at 0.5 speed
        // when dialogue speed is 1, dialogue plays at 1.5 speed
        dialogueSlowdown = (0.5f - dialogueSpeed);
    }

    public void SetAdventureMode(bool active)
    {
        adventureMode = active;
        PlayerPrefs.SetInt(Global.SAVEADVENTUREMODE, adventureMode ? 1 : 0);
    }

    public void SetSceneReached(int scene)
    {
        sceneReached = scene;
        PlayerPrefs.SetInt(Global.SAVEPROGRESS, sceneReached);
    }
}
