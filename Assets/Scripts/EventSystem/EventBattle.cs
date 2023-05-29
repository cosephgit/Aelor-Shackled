using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event starts a battle sequence
// Created by: Seph 28/5
// Last edit by: Seph 28/5


public class EventBattle : Event
{
    // TODO reference to battle class placeholder name: BATTLECLASS
    //[SerializeField]private BATTLECLASS battle;
    [SerializeField]private EventSequence sequenceVictory;
    [SerializeField]private EventSequence sequenceDefeat;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        // TODO camera transition to battle view
        finished = false;

        // TODO call battle class to start battle with reference to player pawn, enemy pawn, and this event, placeholder method name STARTBATTLE
        // battle.STARTBATTLE(this);

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
