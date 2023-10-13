using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyData[] all_enemyData;
    public ItemObject[] all_ItemData;

    // Update is called once per frame
    void Update()
    {
        all_enemyData = FindObjectsOfType<EnemyData>();
        all_ItemData = FindObjectsOfType<ItemObject>();

        for (int i = 0; i < all_enemyData.Length; i++)
        {
            Debug.Log("enemy " + all_enemyData[0].enemyVec3);
        }
        
    }
}
