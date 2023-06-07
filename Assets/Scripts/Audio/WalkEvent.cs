using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// simple class to be attached alongside a character animator for animation events
// Created by: Seph 7/6
// Last edit by: Seph 7/6

public class WalkEvent : MonoBehaviour
{
    public void Footstep()
    {
        SoundSystemManager.instance.footstepSource.Play();
    }
}
