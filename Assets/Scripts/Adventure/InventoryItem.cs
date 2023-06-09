using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// a class for inventory items
// only contains a name, sound and image
// any actual player inspection of the item will be handled in the event system introducing it
// Created by: Seph 28/5
// Last edit by: Seph 29/5

public class InventoryItem : MonoBehaviour
{
    [field:SerializeField]public string itemName { get; private set; }
    [field:SerializeField]public SpriteRenderer itemImage { get; private set; }
    [field:SerializeField]public AudioClip itemSound { get; private set; }
}
