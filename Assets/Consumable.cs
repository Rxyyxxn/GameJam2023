using UnityEngine;

public class Consumable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("consumable"))
        {
            // The player touched a consumable object, make it disappear.
            other.GetComponent<ItemObject>().OnHandlePickupItem();
        }
    }
}
