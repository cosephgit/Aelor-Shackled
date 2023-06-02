using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// this class shows the current player inventory
// the inventory box slides down out of sight while empty or not in use, and pops back up on mouse over
// Created by: Seph 28/5
// Last edit by: Seph 30/5

public class UIInventory : MonoBehaviour
{
    [SerializeField]private Transform inventoryBox; // the actual inventory box
    [SerializeField]private Transform inventoryBoxOffscreen; // the position indicator for the inventory box while not in use
    [SerializeField]private UIInventorySlot[] inventorySlots = new UIInventorySlot[Global.INVENTORYSLOTS]; // the individual inventory slots to fill with items
    [SerializeField]private float popUpDuration = 2f; // how long the inventory pops up for
    [SerializeField]private float popUpMoveTime = 0.5f; // how long the inventory takes to go from top position to bottom position
    private Vector3 posOriginal;
    private Vector3 posOffscreen;
    private float popUpEndTime; // how long before the inventory pops down
    private float popUpProgress; // fraction of movement from bottom to top position (0f...1f)

    private void Start()
    {
        posOriginal = inventoryBox.position;
        posOffscreen = inventoryBoxOffscreen.position;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].SetEmpty();
            inventorySlots[i].SetInitialPos();
        }
        // move the box off-screen at the start of the scene
        inventoryBox.position = posOffscreen;
        popUpProgress = 0f;
        popUpEndTime = 0f;
    }

    // updates the inventory position for the current popUpProgress
    private void UpdateInventoryPosition()
    {
        inventoryBox.transform.position = Vector2.Lerp(posOffscreen, posOriginal, (1f - Mathf.Cos(popUpProgress * Mathf.PI)) * 0.5f);
    }

    private void Update()
    {
        if (popUpEndTime > 0)
        {
            popUpEndTime -= Time.deltaTime;
            if (popUpProgress < 1f)
            {
                // move a bit towards top position
                popUpProgress = Mathf.Min(1f, popUpProgress + Time.deltaTime / popUpMoveTime);
                UpdateInventoryPosition();
            }
        }
        else
        {
            if (popUpProgress > 0f)
            {
                // move a bit towards bottom position
                popUpProgress = Mathf.Max(0f, popUpProgress - Time.deltaTime / popUpMoveTime);
                UpdateInventoryPosition();
            }
        }
    }

    public void UIMouseOver()
    {
        popUpEndTime = popUpDuration;
    }

    // called by the UI buttons when an inventory slot button is pressed
    public void ButtonUse(int index)
    {

    }

    public void SetSlotContent(int index, InventoryItem type)
    {
        if (type)
            inventorySlots[index].SetFilled(type);
        else
            inventorySlots[index].SetEmpty();
    }

    public Vector2 GetSlotPosition(int index)
    {
        return (inventorySlots[index].slotBasePos);
    }
}
