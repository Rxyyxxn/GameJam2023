using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_slotPrefab;

    public Transform InventoryBar;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {
        InventorySystem.current.OnInvChange += OnUpdateInventory;
    }

    private void Update()
    {
        InventoryBar = InventorySystem.current.InvBarTransform;
    }

    private void OnUpdateInventory()
    {
        if (InventoryBar!=null)
        {
            foreach (Transform t in InventoryBar)
            {
                Destroy(t.gameObject);
            }

            DrawInventory();
        }
        else
        {

        }
    }

    public void DrawInventory()
    {
        foreach (InventoryItem item in InventorySystem.current.inventory)
        {
            AddInventorySlot(item);
        }
    }

    public void AddInventorySlot(InventoryItem item)
    {
        GameObject obj = Instantiate(m_slotPrefab);
        obj.transform.SetParent(InventoryBar.transform, false);

        SlotScript slot = obj.GetComponent<SlotScript>();
        slot.Set(item);
    }
}
