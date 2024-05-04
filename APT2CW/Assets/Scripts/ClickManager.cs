using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public GameManager gameManager;
    public AudioManager audioManager;


    public void CollectItem(ItemData item)
    {
        TryGettingItem(item);
    }

    public void PlayAudio(ItemData item)
    {
        if(item.musicID == 0)
        {
            Debug.Log("I will play audio 0");
        }
    }
    public void TryGettingItem(ItemData item)
    {
        if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item))
        {
            GameManager.collectedItems.Add(item);
            
            foreach (GameObject i in item.objectToRemove)
            Destroy(i);
            gameManager.UpdateEquipmentCanvas();
            Debug.Log("Contained" + name);

        }
        
    }

    public void StartSlidingPuzzle(ItemData item)
    {
        if (item.itemID == 6)
        {
            GameManager.collectedItems.Add(item);
            foreach (GameObject i in item.objectToActivate)
                i.SetActive(true);
            Debug.Log("Contained" + name);
        }
    }
    
}
