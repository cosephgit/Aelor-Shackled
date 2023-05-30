using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script uses an item from the player inventory
// Created by: Seph 30/5
// Last edit by: Seph 30/5

public class EventUseItem : Event
{
    [SerializeField]private InventoryItem item;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        // TODO play animation moving item towards the action point
    }

    public override void End()
    {
        base.End();

        // remove the item from the player's inventory
        SceneManager.instance.playerAdventure.UseItem(item);
    }
}
