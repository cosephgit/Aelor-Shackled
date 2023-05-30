using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script adds an item to the player inventory
// Created by: Seph 30/5
// Last edit by: Seph 30/5

public class EventGetItem : Event
{
    [SerializeField]private InventoryItem item;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        // TODO play animation moving item towards inventory box
    }

    public override void End()
    {
        base.End();

        // add the item to the player's inventory
        SceneManager.instance.playerAdventure.AddItem(item);
    }
}
