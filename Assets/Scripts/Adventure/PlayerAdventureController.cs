using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller for any player-specific actor behaviour
// Created by: Seph 27/5
// Last edit by: Seph 29/5

public class PlayerAdventureController : ActorBase
{
    private InventoryItem[] items = new InventoryItem[Global.INVENTORYSLOTS];

    // go through the slots and sort them to remove gaps
    private void SortSlots()
    {
        /*
        go through all entries
        if the entry is empty, check all subsequent entries and move them back
        if there are no full subsequent entries, we're done
        */
        for (int i = 0; i < Global.INVENTORYSLOTS; i++)
        {
            bool done = false;

            if (items[i])
                UIControlInterface.instance.inventory.SetSlotContent(items[i]);
            else
            {
                // this is a blank slot, check all subsequent slots for an item to bring back
                for (int j = i+1; j < Global.INVENTORYSLOTS; j++)
                {
                    if (items[j])
                    {
                        items[i] = items[j];
                        items[j] = null;
                        UIControlInterface.instance.inventory.SetSlotContent(items[i]);
                        break;
                    }
                    else if (j == Global.INVENTORYSLOTS - 1)
                    {
                        // reached the end of the inventory array so it must be empty
                        done = true;
                    }
                }
            }

            if (done) break;
        }
    }

    // returns the index of the item in the player's inventory if the player has it
    // returns -1 if the player doesn't have it
    public int HasItem(InventoryItem type)
    {
        for (int i = 0; i < Global.INVENTORYSLOTS; i++)
        {
            if (items[i] && items[i].itemName == type.itemName) return i;
        }

        return -1;
    }

    // returns the index of the slot that the item is added to, or -1 if it fails (out of slots, or already in inventory)
    public int AddItem(InventoryItem type)
    {
        for (int i = 0; i < Global.INVENTORYSLOTS; i++)
        {
            // check if item is already in inventory
            if (items[i] && items[i].itemName == type.itemName) return -1;
        }
        for (int i = 0; i < Global.INVENTORYSLOTS; i++)
        {
            if (!items[i])
            {
                // add the item to this slot
                items[i] = type;
                SortSlots();
                return i;
            }
        }
        return -1;
    }

    // remove the indicated item type from the inventory, and re-sort the inventory
    // returns true if the item was successfully removed, false otherwise
    public bool UseItem(InventoryItem type)
    {
        for (int i = 0; i < Global.INVENTORYSLOTS; i++)
        {
            if (items[i] && items[i].itemName == type.itemName)
            {
                items[i] = null;
                SortSlots();
                return true;
            }
        }
        return false;
    }
}
