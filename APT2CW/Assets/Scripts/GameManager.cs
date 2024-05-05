using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static Dictionary<ItemData, GameObject> collectedItems = new Dictionary<ItemData, GameObject>();

    [Header("Local Scene")]
    int activeLocalScene = 1;

    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots, equipmentImages;
    public Sprite emptyItemSlotSprite;
    public Color selectedItemColor;
    public int selectedCanvasSlotID = 0, selectedItemID = -1;
    public ItemData selectedItem;
    private GameObject dragObj;



    public void SelectItem(int equipmentCanvasID)
    {
        Color c = Color.white;
        c.a = 0;
        equipmentSlots[selectedCanvasSlotID].color = c;

        if (equipmentCanvasID >= collectedItems.Count || equipmentCanvasID < 0)
        {
            selectedItemID = -1;
            selectedCanvasSlotID = 0;
            return;
        }
        equipmentSlots[equipmentCanvasID].color = selectedItemColor;
        selectedCanvasSlotID = equipmentCanvasID;
        selectedItemID = collectedItems.ElementAt(selectedCanvasSlotID).Key.itemID;
        selectedItem = collectedItems.ElementAt(selectedCanvasSlotID).Key;
        // collectedItems[selectedCanvasSlotID].itemID;
        //DragStart(selectedItemID);

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
            {
                equipmentImages[i].sprite = collectedItems.ElementAt(i).Key.itemSlotSprite;
            }

            else
                equipmentImages[i].sprite = emptyItemSlotSprite;

        }

        if (itemAmount == 0)

            SelectItem(-1);
        else if (itemAmount == 1)
            SelectItem(0);

    }

    public void CursorPicture()
    {
        if (collectedItems != null)
        {
            // activate the object
            collectedItems.TryGetValue(selectedItem, out dragObj);
            if (dragObj)
                dragObj.SetActive(true);

            // gameobject follow cursor
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            dragObj.transform.position = cursorPosition;
        }
        
    }
    public void DropCursorPicture()
    {
        if (collectedItems != null)
        {
            // get canreceive from what's on item data
            
            if (canReceive)
            {
                //destroy the selected object sprite

                //delete it from the dictionary
                RemoveItems();
                // destroy the dragobj
            }
            //also disable the game object, if already removed then cannot
            dragObj.SetActive(false);
        }
           
    }

    public void RemoveItems()
    {
        var itemToRemove = collectedItems.FirstOrDefault(x => x.Key == selectedItem);

        // Check if the item was found
        if (itemToRemove.Key != null)
        {
            // Remove the item from the dictionary
            collectedItems.Remove(itemToRemove.Key);
            UpdateEquipmentCanvas();
        }
    }

}
