using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event adjusts the fade out levels of the scene
// it's used when the player triggers the mushroom patch in scene 2 to fade out the view for the transition to the return point
// Created by: Seph 4/6
// Last edit by: Seph 4/6

public class EventFade : Event
{
    [SerializeField]private float fadeNew;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        UIControlInterface.instance.fadeManager.SetFadeTarget(fadeNew);
    }

    public override void End()
    {
        base.End();
    }
}
