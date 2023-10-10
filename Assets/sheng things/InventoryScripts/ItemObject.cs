using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData refItem;

    private void Update()
    {
        Debug.Log(refItem);
    }

    public void OnHandlePickupItem()
    {
        
        InventorySystem.current.Add(refItem);
        Destroy(gameObject);
    }
}
