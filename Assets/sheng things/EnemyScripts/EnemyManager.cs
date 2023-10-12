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

        //Debug.Log(all_ItemData[1].itemVec3);
    }
}
