using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this type of event plays a dialogue line on the indicated pawn

public class EventDialogue : Event
{
    [SerializeField]private TalkableBase subject;
    [SerializeField]private string line;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        subject.ShowLine(line);

        EndDelay();
    }

    public override void End()
    {
        subject.HideLine();
        base.End();
    }
}
