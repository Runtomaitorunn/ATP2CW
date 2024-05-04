using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static List<ItemData> collectedItems = new List<ItemData>();

    [Header("Local Scene")]
    int activeLocalScene = 1;

    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots, equipmentImages;
    public Sprite emptyItemSlotSprite;
    public Color selectedItemColor;
    public int selectedCanvasSlotID = 0, selectedItemID = -1;



    public void SelectItem(int equipmentCanvasID)
    {
        Color c = Color.white;
        c.a = 0;
        equipmentSlots[selectedCanvasSlotID].color = c;

        if(equipmentCanvasID>= collectedItems.Count || equipmentCanvasID < 0)
        {
            selectedItemID = -1;
            selectedCanvasSlotID = 0;
            return;
        }
        equipmentSlots[equipmentCanvasID].color = selectedItemColor;
        selectedCanvasSlotID = equipmentCanvasID;
        selectedItemID = collectedItems[selectedCanvasSlotID].itemID;


    }

    public void ShowItemName(int quipmentCnavasID)
    {

    }

    public void UpdateEquipmentCanvas()
    {
        int itemAmount = collectedItems.Count; 
        int itemSlotAmount = equipmentImages.Length;
        for (int i = 0; i < itemSlotAmount; i++)
        {
            if (i < itemAmount)           
                equipmentImages[i].sprite = collectedItems[i].itemSlotSprite;
            else
                equipmentImages[i].sprite = emptyItemSlotSprite;
                            
        }

        if(itemAmount == 0)
        
            SelectItem(-1);
        else if (itemAmount == 1)
            SelectItem(0);

    }

}
