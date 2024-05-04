using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public GameManager gamemanager;


    public void CollectItem(ItemData item)
    {
        TryGettingItem(item);
    }

    public void TryGettingItem(ItemData item)
    {
        if (item.requiredItemID == 4 || GameManager.collectedItems.Contains(item.requiredItemID))
        {
            GameManager.collectedItems.Add(item.itemID);
            foreach(GameObject i in item.objectToRemove)
            Destroy(i);
            Debug.Log("Contained" + name);
        }
        if (item.requiredItemID == 0 || GameManager.collectedItems.Contains(item.requiredItemID))
        {
            GameManager.collectedItems.Add(item.itemID);
            foreach (GameObject i in item.objectToRemove)
                Destroy(i);
            Debug.Log("Contained" + name);
        }
    }

    public void StartSlidingPuzzle(ItemData item)
    {
        if (item.requiredItemID == 6)
        {
            GameManager.collectedItems.Add(item.itemID);
            foreach (GameObject i in item.objectToActivate)
                i.SetActive(true);
            Debug.Log("Contained" + name);
        }
    }
}
