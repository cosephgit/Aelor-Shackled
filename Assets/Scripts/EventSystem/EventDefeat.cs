using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event causes the player to lose the game!
// Created by: Seph 27/5
// Last edit by: Seph 28/5

public class EventDefeat : Event
{
    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        // TODO some sort of game over/load screen menu elemet
        #if UNITY_EDITOR
        Debug.Log("GAME OVER YOU LOSE");
        #endif

        UIControlInterface.instance.Defeat();

        finished = true;
    }
}
