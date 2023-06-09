using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Events do nothing on their own!
// these are data holders and should be added to an event sequence
// they will called in the order defined in the event sequence
// on it's own this class will just leave a pause
// Created by: Seph 27/5
// Last edit by: Seph 28/5

public class Event : MonoBehaviour
{
    [SerializeField]protected float delay = 1f; // the minimum time delay before the next event in the sequence should be triggered
    private EventSequence sequence;
    protected float endTime;
    protected bool finished;
    private bool running = false;

    // start this event
    public virtual void Run(EventSequence setSequence)
    {
        running = true;
        sequence = setSequence;
        endTime = delay;
        finished = true;
        #if UNITY_EDITOR
        Debug.Log("starting event " + gameObject);
        #endif
    }

    // end the event if this event uses a timer
    protected virtual void Update()
    {
        if (endTime > 0)
        {
            //Debug.Log("endTime = " + endTime);
            endTime -= Time.deltaTime;
            if (endTime <= 0)
            {
                if (finished)
                    End();
                else
                    endTime = 0.01f; // wait for another frame
            }
        }
    }

    // end this event and let the event sequence know
    public virtual void End()
    {
        running = false;
        endTime = 0;
        //Debug.Log("endTime = " + endTime);
        sequence.EventComplete();
    }

    public virtual void EndEventRemote(int index)
    {
        End(); // need to end the event sequence NOW so that the adventure mode is unpaused (if needed)
    }
}
