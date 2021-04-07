using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This is UI Inventory reference
public class InventoryPanel : MonoBehaviour
{
    [SerializeField] List<InventoryButton> inventoryButtons;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI description;

    public void Show(ItemSlot itemSlot)
    {
        //if (itemSlot.item.picture!=null)
        {
            image.sprite = itemSlot.item.picture;
        }     
        description.text= itemSlot.item.description;
    }
    public void Set(ItemCollection itemCollection)
    {
        for (int i = 0; i < inventoryButtons.Count; i++)
        {
            inventoryButtons[i].gameObject.SetActive(false);
        }
        int inventoryButtonCount = 0;
        for (int i = 0; i < itemCollection.itemSlots.Count; i++)
        {
            if (itemCollection.itemSlots[i].owned==true)
            {
                inventoryButtons[inventoryButtonCount].Set(itemCollection.itemSlots[i], this);
                inventoryButtonCount +=1;
            }
        }


        //for (int i = 0; i < inventoryButtons.Count; i++)
        //{
        //    if (i < itemCollection.itemSlots.Count && itemCollection.itemSlots[i].owned==true)
        //    {
        //        inventoryButtons[i].Set(itemCollection.itemSlots[i]);
        //    }
        //    else
        //    {
        //        inventoryButtons[i].gameObject.SetActive(false);
        //    }
           
        //}
    }
}
