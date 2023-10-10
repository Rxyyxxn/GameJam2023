using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithItem : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided GameObject has the "Obstacle" tag.
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Destroy the collided GameObject.
            Destroy(collision.gameObject);
        }
    }
}
