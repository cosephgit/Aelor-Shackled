using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this class shows the current player inventory
// the inventory box slides down out of sight while empty or not in use, and pops back up on mouse over
// Created by: Seph 28/5
// Last edit by: Seph 1/6

public class UIInventorySlot : MonoBehaviour
{
    [SerializeField]private Button slotButton;
    [SerializeField]private Image slotItem; // the image of the item in this inventory slot
    public Vector2 slotBasePos { get; private set; }

    public void SetInitialPos()
    {
        slotBasePos = transform.position;
    }

    public void SetEmpty()
    {
        slotButton.interactable = false;

        slotItem.enabled = false;
    }

    public void SetFilled(InventoryItem item)
    {
        slotButton.interactable = true;

        slotItem.sprite = item.itemImage.sprite;
        slotItem.color = item.itemImage.color;

        slotItem.enabled = true;
    }
}
