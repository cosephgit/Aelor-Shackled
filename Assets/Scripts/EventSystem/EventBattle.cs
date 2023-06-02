using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event starts a battle sequence
// Created by: Seph 28/5
// Last edit by: Seph 28/5


public class EventBattle : Event
{
    [SerializeField]private BattleManager battle;
    [SerializeField]private EventSequence sequenceVictory;
    [SerializeField]private EventSequence sequenceDefeat;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        // TODO camera transition to battle view
        finished = false;

        //battle.BeginBattleEvent(this);

        // TEMP FOR TESTING
        UIControlInterface.instance.dialogueTree.OpenDialogue(transform.position, new string[2] { "WIN", "LOSE" }, this);
    }

    public void BattleEnd(bool victory)
    {
        finished = true;
        base.End();
        if (victory)
            sequenceVictory.Run();
        else
            sequenceDefeat.Run();
    }

    // TEMP FOR TESTING
    public override void EndEventRemote(int index)
    {
        BattleEnd(index == 0);
    }
}
