using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script uses an item from the player inventory
// Created by: Seph 30/5
// Last edit by: Seph 1/6

public class EventUseItem : Event
{
    [SerializeField]private InventoryItem item;
    [SerializeField]private Transform itemEndPos;

    public override void Run(EventSequence setSequence)
    {
        base.Run(setSequence);
        // TODO play animation moving item towards the action point

        // remove the item from the player's inventory
        int slotIndex = SceneManager.instance.playerAdventure.HasItem(item);

        // TODO play animation moving item towards inventory box
        if (slotIndex == -1)
        {
            End();
        }
        else
        {
            SceneManager.instance.playerAdventure.UseItem(item);

            if (itemEndPos)
            {
                // pop up the inventory box
                UIControlInterface.instance.inventory.UIMouseOver();
                // show the item moving towards the inventory box
                UIControlInterface.instance.inventoryItem.SetItemDrop(item, slotIndex, UIControlInterface.instance.WorldToScreenPos(itemEndPos.position));
            }
        }
    }
}
