using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    ItemSlot item;
    InventoryPanel panel;

    public void Set(ItemSlot itemSlot, InventoryPanel panel)
    {
        this.panel = panel;
        gameObject.SetActive(true);
        item = itemSlot;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.item.itemName;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        panel.Show(item);

    }
}
