using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class manages each event sequence
// when an event sequence begins, normal gameplay is (normally) put in hold until the event sequence ends
// (some very simple event sequences may allow the player to still move around)
// each event sequence is a list

public class EventSequence : MonoBehaviour
{
    [SerializeField]private Event[] events; // the events which should be run in this event sequence
    [SerializeField]private bool pauseAdventure = true; // should normal adventure controls be put on pause until this event ends?
    private int eventCurrent; // the index of the current event in the events array
    private bool eventSequenceActive = false;

    public void Run()
    {
        Debug.Log("event sequence triggered");
        RunCurrentEvent();
        SceneManager.instance.SetAdventurePause(pauseAdventure);
        eventCurrent = 0;
        eventSequenceActive = true;
    }

    public void RunCurrentEvent()
    {
        if (eventCurrent < events.Length)
        {
            events[eventCurrent].Run(this);
        }
        else
        {
            // event sequence has finished
            SceneManager.instance.SetAdventurePause(false);
        }
    }

    // called by an Event when it is complete
    public void EventComplete()
    {
        eventCurrent++;
        RunCurrentEvent();
    }
}
