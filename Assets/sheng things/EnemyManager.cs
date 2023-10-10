using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyData[] all_enemyData;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < all_enemyData.Length; i++)
        {
            Debug.Log(all_enemyData[i].enemyVec3);
        }
    }
}
