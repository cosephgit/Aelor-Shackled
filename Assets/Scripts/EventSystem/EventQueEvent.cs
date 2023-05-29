using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event tells an interactable to que up a new event
// Created by: Seph 28/5
// Last edit by: Seph 28/5

public class EventQueEvent : Event
{
    [SerializeField]private InteractableBase subject;
    [SerializeField]private EventSequence eventSequence;
    [SerializeField]private InteractionType typeOverride = InteractionType.Look;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        // set the target object to have the new event type
        subject.SetEvent(typeOverride, eventSequence);
    }
}
