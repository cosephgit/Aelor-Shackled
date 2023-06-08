using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event sets the parallax manager to lock on to the target transform (e.g. for puzzles or battles)
// Created by: Seph 28/5
// Last edit by: Seph 28/5

public class EventCameraLock : Event
{
    [SerializeField]private Transform lockPoint;
    [SerializeField]private bool lockOrElseNot;
    [SerializeField]private float setOrthoSize = 0f;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        if (lockOrElseNot)
            SceneManager.instance.parallaxManager.SetCameraLock(lockPoint.position, setOrthoSize);
        else
            SceneManager.instance.parallaxManager.EndCameraLock();
    }
}
