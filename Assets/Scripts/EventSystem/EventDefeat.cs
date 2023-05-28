using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event causes the player to lose the game!

public class EventDefeat : Event
{
    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        // TODO some sort of game over/load screen menu elemet
        finished = true;
    }
}
