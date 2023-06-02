using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script adds an item to the player inventory
// Created by: Seph 30/5
// Last edit by: Seph 1/6

public class EventGetItem : Event
{
    [SerializeField]private InventoryItem item;
    [SerializeField]private Transform itemStartPos;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);

        int slotIndex = SceneManager.instance.playerAdventure.GetItemSlotEmpty();

        // TODO play animation moving item towards inventory box

        if (slotIndex == -1)
        {
            // no inventory space
            base.End();
        }
        else if (itemStartPos)
        {
            // pop up the inventory box
            UIControlInterface.instance.inventory.UIMouseOver();
            // show the item moving towards the inventory box
            UIControlInterface.instance.inventoryItem.SetItemPick(item, UIControlInterface.instance.WorldToScreenPos(itemStartPos.position), slotIndex);
        }
    }

    public override void End()
    {
        base.End();

        // add the item to the player's inventory
        SceneManager.instance.playerAdventure.AddItem(item);
    }
}
