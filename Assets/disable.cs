using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disable : MonoBehaviour
{
    private GameObject invUI;

    // Start is called before the first frame update
    void Start()
    {
        invUI = GameObject.FindGameObjectWithTag("InventoryUI");
        invUI.SetActive(false);
    }
}
