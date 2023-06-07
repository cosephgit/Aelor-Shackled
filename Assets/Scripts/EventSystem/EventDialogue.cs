using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this type of event plays a dialogue line on the indicated pawn
// Created by: Seph 27/5
// Last edit by: Seph 7/6

public class EventDialogue : Event
{
    [SerializeField]private ActorBase subject;
    [SerializeField]private string line;
    [SerializeField]private AnimSingle anim = AnimSingle.None; // if not None then the subject will play the matching anim (if present)
    [SerializeField]private bool oneShot = false; // this event will only ever trigger once, every consecutive call to this event will be skipped
    private bool usedOnce = false;


    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        if (oneShot)
        {
            if (usedOnce)
            {
                End();
                return;
            }
            usedOnce = true;
        }

        subject.ShowLine(line, anim);
    }

    public override void End()
    {
        subject.HideLine();
        base.End();
    }
}
