using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public GameObject playerGO;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("consumable"))
        {
            // The player touched a consumable object, make it disappear.
            playerGO.GetComponent<PlayerMove>().currentSound.clip = playerGO.GetComponent<PlayerMove>().playerCollectItem;
            playerGO.GetComponent<PlayerMove>().currentSound.Play();
            other.gameObject.GetComponent<ItemObject>().OnHandlePickupItem();
        }

        //if (other.gameObject.CompareTag("enemy"))
        //{
        //    other.gameObject.GetComponent<EnemyData>().EnemyHP--;
        //}
    }

    //private void OnTriggerStay2D(Collider2D other)
    //{
    //    if (other.gameObject.CompareTag("enemy"))
    //    {
    //        other.gameObject.GetComponent<EnemyData>().EnemyHP--;
    //    }
    //}
}

