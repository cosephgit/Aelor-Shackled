using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// event to play or stop sound effects
// Created by: Seph 8/6
// Last edit by: Seph 8/6

public class EventAudio : Event
{
    [Header("if sourceChange is set it will be enabled or disabled")]
    [SerializeField]private AudioSource sourceChange;
    [SerializeField]private bool sourceEnableElseDisable;
    [Header("playMusic will tell the sound system to play this clip unless stopMusic")]
    [SerializeField]private AudioClip playMusic;
    [SerializeField]private bool stopMusic;
    [Header("playClip will play once through the sound system manager")]
    [SerializeField]private AudioClip playClip;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        if (sourceChange)
        {
            if (sourceEnableElseDisable)
                sourceChange.Play();
            else
                sourceChange.Stop();
        }

        if (stopMusic)
            SoundSystemManager.instance.StopMusic();
        else if (playMusic)
            SoundSystemManager.instance.PlayMusic(playMusic);

        if (playClip)
        {
            SoundSystemManager.instance.PlaySFXStandard(playClip);
        }
    }
}
