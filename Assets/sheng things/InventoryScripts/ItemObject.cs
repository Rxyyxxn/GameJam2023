using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData refItem;

    public Tilemap tmap;
    public Vector3Int itemVec3;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        itemVec3 = tmap.WorldToCell(transform.position);
        transform.position = tmap.CellToWorld(itemVec3);
        tmap = GameObject.FindGameObjectWithTag("GroundTile").GetComponent<Tilemap>();
    }

    public void OnHandlePickupItem()
    {
        InventorySystem.current.Add(refItem);
        Destroy(gameObject);
    }
    private void Update()
    {
        tmap = GameObject.FindGameObjectWithTag("GroundTile").GetComponent<Tilemap>();

    }
}
