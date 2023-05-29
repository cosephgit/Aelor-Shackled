using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this event opens a dialogue tree and lets the player choose a dialogue option
// it then triggers the next event depending on the dialogue selection

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
        // TODO
        finished = true;
    }
}
