using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ItemSlot
{
    public Item item;
    public bool onStart;
    public bool owned;
}
[CreateAssetMenu(menuName="Data /Item Collection")]
public class ItemCollection : ScriptableObject
{
    public List<ItemSlot> itemSlots;
  
   public void Init()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            itemSlots[i].owned = itemSlots[i].onStart;
        }
    }

    public void Set(Item item, bool set)
    {
        ItemSlot itemSlot = itemSlots.Find((x) => x.item == item);
        if (itemSlot!=null)
        {
            itemSlot.owned = set;
        }
        else
        {
            Debug.Log("Item not found in collection" + item.itemName);
        }
      
    }
}
