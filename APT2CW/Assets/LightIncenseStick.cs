using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LightIncenseStick : MonoBehaviour
{

    public GameManager gameManager;
    public ItemData thisItemData;
    public GameObject winPic;

    
    public void ReceivedItem()
    {
        int requiredItemID = thisItemData.requiredItemID;
        gameManager.CompareReceiveItem(requiredItemID);
    }

    public void EndScene1()
    {
        winPic.SetActive(true);
        
    }

}
