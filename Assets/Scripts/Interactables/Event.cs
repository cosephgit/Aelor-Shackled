using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Events do nothing on their own!
// these are data holders and should be added to an event sequence
// they will called in the order defined in the event sequence

public class Event : MonoBehaviour
{
    [SerializeField]private float delay = 1f; // the time delay before the next event in the sequence should be triggeded
    private EventSequence sequence;
    private float endTime;

    // start this event
    public virtual void Run(EventSequence setSequence)
    {
        sequence = setSequence;
    }

    // use the generic timed event delay
    protected void EndDelay()
    {
        endTime = delay;
    }

    // end the event if this event uses a timer
    private void Update()
    {
        if (endTime > 0)
        {
            endTime -= Time.deltaTime;
            if (endTime <= 0)
            {
                End();
            }
        }
    }

    // end this event and let the event sequence know
    public virtual void End()
    {
        sequence.EventComplete();
    }
}
