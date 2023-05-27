using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event tells the subject pawn to move to the target point
// this event only ends when the subject reaches the target

public class EventMove : Event
{
    [SerializeField]private ActorBase subject;
    [SerializeField]private Vector3 target;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        subject.SetMoveTarget(target, true);
        finished = false;
    }

    // this event only ends when the subject reaches the target point
    protected override void Update()
    {
        if (Mathf.Approximately((subject.transform.position - target).magnitude, 0f))
        {
            finished = true;
        }
        base.Update();
    }
}
