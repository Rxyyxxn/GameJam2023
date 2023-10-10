using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public EnemyData[] all_enemyData;

    // Update is called once per frame
    void Update()
    {
        all_enemyData = FindObjectsOfType<EnemyData>();

        //for (int i = 0; i < all_enemyData.Length; i++)
        //{
        //    Debug.Log(all_enemyData[i]);
        //}
    }
}
