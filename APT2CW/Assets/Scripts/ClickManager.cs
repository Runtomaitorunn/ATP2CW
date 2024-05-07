using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ClickManager : MonoBehaviour
{
    public GameManager gameManager;
    public AudioManager audioManager;


    

    
    public void TryGettingItem(ItemData item)
    {
        if (item.requiredItemID == -1 || GameManager.collectedItems.ContainsKey(item))
        {
            GameManager.collectedItems.Add(item,item.gameObject);
            
            foreach (GameObject i in item.objectToRemove)
            //Destroy(i);
            i.SetActive(false);
            gameManager.UpdateEquipmentCanvas();
            audioManager.PlayAudioByName("collectable");
            Debug.Log("Contained" + name);

            
        }
        
    }

    public void StartSlidingPuzzle(ItemData item)
    {
        if (item.itemID == 6)
        {
            GameManager.collectedItems.Add(item, item.gameObject);
            foreach (GameObject i in item.objectToActivate)
                i.SetActive(true);
            Debug.Log("Contained" + name);
        }
    }
    
    public void ActivateProps(ItemData item)
    {
        foreach (GameObject obj in item.objectToActivate)
        {
            obj.SetActive(true);
        }
    }
    
}
