using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private GameObject InventoryBar;
    public Transform InvBarTransform;

    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory { get; set; }

    public delegate void onInventoryChangedEvent();
    public onInventoryChangedEvent OnInvChange;
    public static InventorySystem current;

    public GameObject playerGO;

    private void Update()
    {
        InventoryBar = playerGO.GetComponent<PlayerMove>().invenBar;
        InvBarTransform = playerGO.GetComponent<PlayerMove>().invenBarTr;

        //Debug.Log(current);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

        playerGO = GameObject.FindGameObjectWithTag("Player");

        //current = this;
    }

    private void Start()
    {
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
            OnInvChange();
        }
        else
        { 
            InventoryItem newItem = new InventoryItem(refData);
            inventory.Add(newItem);
            m_itemDictionary.Add(refData, newItem);
            OnInvChange();
        }
    }

    public void Remove(InventoryItemData refData)
    {
        if (m_itemDictionary.TryGetValue(refData, out InventoryItem value))
        {
            value.RemoveFromStack();
            OnInvChange();

            if (value.stackSize == 0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(refData);
                OnInvChange();
            }
        }
    }

    public int ItemCount(string ItemName)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].data.displayName == ItemName)
            {
                return inventory[i].stackSize;
            }
            else
            {
                Debug.Log("Item name not found");

            }
        }
        return 0;
    }

    public InventoryItemData GetItemDataThrough_ID(int ID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].data.id == ID)
            {
                return inventory[i].data;
            }
        }
        return null;
    }
}
