using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int itemID, requiredItemID,musicID;
    public GameObject[] objectToRemove;
    public GameObject[] objectToActivate;

    [Header("Hint")]
    public string hintMessage;
    public Canvas hintCanvas;

    [Header("EquipmentBox")]
    public Sprite itemSlotSprite;
    
}
