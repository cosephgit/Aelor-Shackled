using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script checks if the player has an item and may branch into an eventsequence if the item is available
// Created by: Seph 30/5
// Last edit by: Seph 30/5

public class EventCheckItem : Event
{
    [SerializeField]private InventoryItem item;
    [SerializeField]private EventSequence eventPass;
    [SerializeField]private EventSequence eventFail;

    public override void End()
    {
        base.End();

        if (SceneManager.instance.playerAdventure.HasItem(item) >= 0)
        {
            if (eventPass)
                eventPass.Run();
        }
        else
        {
            if (eventFail)
                eventFail.Run();
        }
    }
}
