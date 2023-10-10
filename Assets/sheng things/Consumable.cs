using UnityEngine;

public class Consumable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("consumable"))
        {
            // The player touched a consumable object, make it disappear.
            other.gameObject.GetComponent<ItemObject>().OnHandlePickupItem();
        }
    }
}
