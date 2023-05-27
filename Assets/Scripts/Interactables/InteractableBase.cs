using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// the base class for all objects which can be interacted with

public class InteractableBase : ActorBase
{
    // each of the these indicate the event sequences that should be triggered when the indicated interaction type is selected
    // these CAN BE CHANGED by event sequences
    [SerializeField]private EventSequence eventLook;
    [SerializeField]private EventSequence eventTalk;
    [SerializeField]private EventSequence eventUse;
    [SerializeField]private EventSequence eventSpecial;

    // check if there is an event sequence of the given type
    public bool HasLook() { return (eventLook != null); }
    public bool HasTalk() { return (eventTalk != null); }
    public bool HasUse() { return (eventUse != null); }
    public bool HasSpecial() { return (eventSpecial != null); }

    // run the event sequence of the given type
    public void DoLook()
    {
        if (eventLook)
        {
            eventLook.Run();
            eventLook = null;
        }
    }
    public void DoTalk()
    {
        if (eventTalk)
        {
            eventTalk.Run();
            eventTalk = null;
        }
    }
    public void DoUse()
    {
        if (eventUse)
        {
            eventUse.Run();
            eventUse = null;
        }
    }
    public void DoSpecial()
    {
        if (eventSpecial)
        {
            eventSpecial.Run();
            eventSpecial = null;
        }
    }

    public void SetLook(EventSequence eventsNew)
    {
        eventLook = eventsNew;
    }
    public void SetTalk(EventSequence eventsNew)
    {
        eventTalk = eventsNew;
    }
    public void SetUse(EventSequence eventsNew)
    {
        eventUse = eventsNew;
    }
    public void SetSpecial(EventSequence eventsNew)
    {
        eventSpecial = eventsNew;
    }
}
