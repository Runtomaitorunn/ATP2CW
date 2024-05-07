using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Dictionary<ItemData, GameObject> collectedItems = new Dictionary<ItemData, GameObject>();

    [Header("Scenes")]
    public GameObject[] scenes;
    public static bool canReceive = false;
    private LightIncenseStick lightIncenseStick;
    private Monk monk;
    

    [Header("Equipment")]
    public GameObject equipmentCanvas;
    public Image[] equipmentSlots, equipmentImages;
    public Sprite emptyItemSlotSprite;
    public Color selectedItemColor;
    public int selectedCanvasSlotID = 0, selectedItemID = -1;
    public ItemData selectedItem;
    private GameObject dragObj;

    [Header("Audio")]
    public AudioManager audioManager;
    public ClickManager clickManager;



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
    
    public void CompareReceiveItem(ItemData item)
    {

        
        // get canreceive from what's on item data
        if(dragObj != null)
        {
            Debug.Log("this is drag object id" + dragObj.GetComponent<ItemData>().itemID);
            Debug.Log("this is requiredID" + item.requiredItemID);
            if (dragObj.GetComponent<ItemData>().itemID == item.requiredItemID)
            {
                canReceive = true;
                Debug.Log("this is canReceive" + canReceive);
                CheckSceneCondition(item);
                
                
            }
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
            case 5:
                audioManager.PlayAudioByName("IncenseBurner");
                SceneChange("2");
                DisableCurrentScene("1");
                lightIncenseStick.EndScene1();
                //go to scene 1 
                break;
            case 12:
                audioManager.PlayAudioByName("SlidingPic");
                foreach (GameObject i in item.objectToRemove)
                    Destroy(i);
                    break;
            case 10:
                Debug.Log("dad memories!");//hen situation
                break;
            case 11:
                Debug.Log("nainai memories!");// worm situation
                break;

            case 4:

                clickManager.ActivateProps(item);
                break;
            case 3:

                clickManager.ActivateProps(item);
                break;

        }
    }
    public void SceneChange(string sceneNo)
    {
        // scene transition

        //set scene aactive
        foreach (GameObject scene in scenes)
        {
            if (scene.name == sceneNo)
            {
                scene.SetActive(true);
                break;
            }
        }
    }
    public void DisableCurrentScene(string sceneNo)
    {
        // scene transition

        //set scene aactive
        foreach (GameObject scene in scenes)
        {
            if (scene.name == sceneNo)
            {
                scene.SetActive(false);
                break;
            }
        }
    }
    public void SceneTransition(ItemData item)
    {

        SceneManager.LoadScene(item.targetSceneName);
    }

}
