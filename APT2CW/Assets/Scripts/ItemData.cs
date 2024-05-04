using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int itemID, requiredItemID;
    public GameObject[] objectToRemove;
    public GameObject[] objectToActivate;

    [Header("Hint")]
    public string hintMessage;
    public Canvas hintCanvas;
    
}
