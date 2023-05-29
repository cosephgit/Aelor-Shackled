using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event opens a dialogue tree and lets the player choose a dialogue option
// it then triggers the next event depending on the dialogue selection
// Created by: Seph 27/5
// Last edit by: Seph 28/5

// note this should ALWAYS be the last event of an event sequence
// errors will happen if used incorrectly

public class EventDialogueTree : Event
{
    [Header("Array sizes must match - use a null dialogueEvent entry for 'no event'")]
    [SerializeField]private string[] dialogueOptions; // the options which the player may choose
    [SerializeField]private EventSequence[] dialogueEvents; // the event sequences which will be triggered by each choice, must match the array size of dialogueOptions
    private int options;

    private void Awake()
    {
        // validate the arrays
        options = Mathf.Min(dialogueOptions.Length, dialogueEvents.Length);
        if (options != Mathf.Max(dialogueOptions.Length, dialogueEvents.Length))
        {
            Debug.LogError("EventDialogueTree " + gameObject + " set up error: mismatched array sizes, using only " + options + " entries");
        }
    }

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        UIControlInterface.instance.dialogueTree.OpenDialogue(transform.position, dialogueOptions, this);

        finished = false;
    }

    // takes the index of the selected dialogue option from the UIDialogueTree
    public override void EndEventRemote(int index)
    {
        End(); // need to end the event sequence NOW so that the adventure mode is unpaused (if needed)
        dialogueEvents[index].Run(); // and with the start of the new event sequence adventure adventure mode will be paused again (if needed)
    }
}
