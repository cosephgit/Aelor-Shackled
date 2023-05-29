using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller for any player-specific actor behaviour
// Created by: Seph 27/5
// Last edit by: Seph 28/5

public class PlayerAdventureController : ActorBase
{
    private InventoryItem[] items = new InventoryItem[Global.INVENTORYSLOTS];

    // returns the index of the item in the player's inventory if the player has it
    // returns -1 if the player doesn't have it
    public int HasItem(InventoryItem type)
    {
        for (int i = 0; i < Global.INVENTORYSLOTS; i++)
        {
            if (items[i].itemName == type.itemName) return i;
        }

        return -1;
    }
}
