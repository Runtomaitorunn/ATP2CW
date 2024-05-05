using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static Dictionary<ItemData, GameObject> collectedItems = new Dictionary<ItemData, GameObject>();

    [Header("Scenes")]
    int activeLocalScene = 1;
    public static bool canReceive = false;

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
            Debug.Log("dragged z"+dragObj.transform.position.z);
        }
        
    }
    
    public void CompareReceiveItem(int requiredID)
    {

        Debug.Log("this is drag object id" + dragObj.GetComponent<ItemData>().itemID);
        Debug.Log("this is requiredID" + requiredID);
        // get canreceive from what's on item data
        if (dragObj.GetComponent<ItemData>().itemID == requiredID)
        {
            canReceive = true;
            Debug.Log("this is canReceive" + canReceive);
        }
    }
    public void DropCursorPicture()
    {
        if (collectedItems != null)
        {
            

            if (canReceive)
            {
                // remove items
                var itemToRemove = collectedItems.FirstOrDefault(x => x.Key == selectedItem);

                  // Check if the item was found
                if (itemToRemove.Key != null)
                {
                    // Remove the item from the dictionary
                    collectedItems.Remove(itemToRemove.Key);
                    UpdateEquipmentCanvas();
                }
                // set canReceive to dalse
                canReceive = false;
            }
            //also disable the game object, if already removed then cannot
            dragObj.SetActive(false);
        }
           
    }
  

    public void CheckSceneCondition(ItemData item)
    {
        switch (item.itemID)
        {
            case 0:
                //go to scene 1 
                break;

        }
    }
}
