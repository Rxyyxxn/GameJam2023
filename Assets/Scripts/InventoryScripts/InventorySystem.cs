using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory { get; set; }

    public static InventorySystem current;

    private void Update()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            Debug.Log(inventory[i].data.displayName + " " + inventory[i].stackSize);
        }
    }

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        current = this;
    }

    public InventoryItem Get(InventoryItemData refData)
    {
        if (m_itemDictionary.TryGetValue(refData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    public void Add(InventoryItemData refData)
    {
        if (m_itemDictionary.TryGetValue(refData, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        { 
            InventoryItem newItem = new InventoryItem(refData);
            inventory.Add(newItem);
            m_itemDictionary.Add(refData, newItem);
        }
    }

    public void Remove(InventoryItemData refData)
    {
        if (m_itemDictionary.TryGetValue(refData, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(refData);
            }
        }
    }
}