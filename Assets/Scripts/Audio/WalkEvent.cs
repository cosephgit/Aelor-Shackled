using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// simple class to be attached alongside a character animator for animation events
// Created by: Seph 7/6
// Last edit by: Seph 8/6

public class WalkEvent : MonoBehaviour
{
    [SerializeField]private AudioClip[] footstepSounds;

    public void Footstep()
    {
        if (footstepSounds.Length > 0)
            SoundSystemManager.instance.PlaySFXStandard(footstepSounds[Random.Range(0, footstepSounds.Length)]);
    }
}
