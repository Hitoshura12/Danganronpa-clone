using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is Player Inventory
public class Inventory : MonoBehaviour
{
    [SerializeField] ItemCollection itemCollection;
    [SerializeField] InventoryPanel inventoryPanel;

    private void Start()
    {
        itemCollection.Init();
        inventoryPanel.Set(itemCollection);
    }
    public void Set(Item item, bool set=true)
    {
        itemCollection.Set(item, set);
    }
}
