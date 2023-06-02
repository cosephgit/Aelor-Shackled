using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// show an inventory item moving to or from the inventory to a world point
// Created by: Seph 1/6
// Last edit by: Seph 1/6

public class UIInventoryItemMove : MonoBehaviour
{
    [SerializeField]private Image itemImage;
    [SerializeField]private RectTransform itemRect;
    [SerializeField]private float itemPause = 0.2f; // the delay at the start and end of the move
    [SerializeField]private float itemMoveTime = 0.6f;

    // specify the item to move, where it should start, and where it should end
    // start position should be a canvas space positions, endSlot is the index of the inventory slot it should move to
    // this section is admittedly pretty janky right now
    // I don't 100% understand the usage of RectTransform.anchoredPosition and vs transform.position, but this got the problem solved quickly
    public void SetItemPick(InventoryItem item, Vector2 start, int endSlot)
    {
        itemImage.sprite = item.itemImage.sprite;
        itemImage.color = item.itemImage.color;
        itemImage.transform.position = start;
        itemImage.enabled = true;
        StopAllCoroutines();
        StartCoroutine(ItemMovement(itemImage.transform.position, UIControlInterface.instance.inventory.GetSlotPosition(endSlot)));
    }

    public void SetItemDrop(InventoryItem item, int startSlot, Vector2 end)
    {
        itemImage.sprite = item.itemImage.sprite;
        itemImage.color = item.itemImage.color;
        itemImage.transform.position = UIControlInterface.instance.inventory.GetSlotPosition(startSlot);
        itemImage.enabled = true;
        StopAllCoroutines();
        StartCoroutine(ItemMovement(itemImage.transform.position, end));
    }

    private IEnumerator ItemMovement(Vector2 start, Vector2 end)
    {
        float progress = 0f;

        yield return new WaitForSeconds(itemPause);

        while (progress < itemMoveTime)
        {
            yield return new WaitForEndOfFrame();
            progress += Time.deltaTime;
            itemImage.transform.position = Vector2.Lerp(start, end, progress / itemMoveTime);
        }

        yield return new WaitForSeconds(itemPause);
        itemImage.enabled = false;
    }
}
