using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Reference to the player's Transform.

    private void LateUpdate()
    {
        if (player != null)
        {
            // Get the current camera position.
            Vector3 currentPosition = transform.position;

            // Update the camera's Z position to match the player's Z position.
            currentPosition.z = player.position.z-5;

            // Set the camera's position.
            transform.position = currentPosition;
        }
    }
}