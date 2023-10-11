using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData refItem;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void OnHandlePickupItem()
    {
        InventorySystem.current.Add(refItem);
        Destroy(gameObject);
    }
}
