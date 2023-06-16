using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event tells the subject pawn to move to the target point
// this event only ends when the subject reaches the target
// Created by: Seph 27/5
// Last edit by: Seph 9/6

public enum MoveFacing
{
    Normal, // no change, just use whatever movement results in
    Right, // face right at end of move
    Left // face left at end of move
}

public class EventMove : Event
{
    [SerializeField]private ActorBase subject;
    [SerializeField]private Transform target;
    [SerializeField]private bool moveForced = false; // should this move be forced to this location, regardless of moveable areas?
    [SerializeField]private MoveFacing moveFacing = MoveFacing.Normal;
    Vector3 subjectTarget;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        subjectTarget = target.position; // this is needed in case the subject is the parent of this event, else the subject will chase the target endlessly
        subject.TryMove(subjectTarget, true, moveForced);
        if (moveFacing != MoveFacing.Normal)
            subject.SetMoveFacing(moveFacing);
        finished = false;
    }

    // this event only ends when the subject reaches the target point
    protected override void Update()
    {
        if (endTime > 0 && !subject.IsMoving())
        {
            finished = true;
        }
        base.Update();
    }
}
