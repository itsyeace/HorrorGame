using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemData> _items = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        _items.Add(item);
        Debug.Log($"Picked up {item.itemName}. Total items: {_items.Count}");
    }

    public bool HasItem(ItemData itemData)
    {
        foreach (ItemData item in _items)
        {
            if (item == itemData)
            {
                return true;
            }
        }
        return false;
    }
}
