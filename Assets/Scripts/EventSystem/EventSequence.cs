using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class manages each event sequence
// when an event sequence begins, normal gameplay is (normally) put in hold until the event sequence ends
// (some very simple event sequences may allow the player to still move around)
// each event sequence is a list
// Created by: Seph 27/5
// Last edit by: Seph 28/5

public class EventSequence : MonoBehaviour
{
    [SerializeField]private Event[] events; // the events which should be run in this event sequence
    [SerializeField]private bool pauseAdventure = true; // should normal adventure controls be put on pause until this event ends?
    [SerializeField]private EventSequence eventSequenceNext; // if non-null, this event sequence is automatically started after this one ends
    private int eventCurrent; // the index of the current event in the events array

    public void Run()
    {
        Debug.Log("Event sequence triggered " + gameObject);

        SceneManager.instance.SetAdventurePause(pauseAdventure);
        eventCurrent = 0;
        RunCurrentEvent();
    }

    private void RunCurrentEvent()
    {
        if (eventCurrent < events.Length)
        {
            events[eventCurrent].Run(this);
        }
        else
        {
            // event sequence has finished
            SceneManager.instance.SetAdventurePause(false);
            if (eventSequenceNext)
            {
                eventSequenceNext.Run();
            }
        }
    }

    // called by an Event when it is complete
    public void EventComplete()
    {
        eventCurrent++;
        RunCurrentEvent();
    }
}
