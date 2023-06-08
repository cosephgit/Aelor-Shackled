using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event tells the desired actors to fade out and go to sleep
// it uses the event delay as the fade out duration to sync the fade with the event timing
// Created by: Seph 8/5
// Last edit by: Seph 8/6

public class EventSleepActor : Event
{
    [SerializeField]private ActorBase[] actors;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        if (actors.Length > 0)
        {
            for (int i = 0; i < actors.Length; i++)
            {
                actors[i].FadeOut(delay);
            }
        }
        else
        {
            finished = true;
            End();
        }
    }
}
