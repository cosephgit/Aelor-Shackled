using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this type of event plays a dialogue line on the indicated pawn
// Created by: Seph 27/5
// Last edit by: Seph 28/5

public class EventDialogue : Event
{
    [SerializeField]private ActorBase subject;
    [SerializeField]private string line;
    [SerializeField]private AnimSingle anim = AnimSingle.None; // if not None then the subject will play the matching anim (if present)

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        subject.ShowLine(line, anim);
    }

    public override void End()
    {
        subject.HideLine();
        base.End();
    }
}
