using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] lanes; // Reference to the lane positions.

    public float laneChangeSpeed = 5f; // Speed at which the player moves between lanes.

    private int currentLane = 1; // The player's current lane (0 for left, 1 for center, 2 for right).

    public float moveSpeed = 5f; // Speed at which the player moves forward.

    Vector3 initialPosition;

    private void Update()
    {
        // Move the player character forward.
        //transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        initialPosition = transform.position;

        // Handle player input for lane changes using arrow keys.
        if (Input.GetKeyDown(KeyCode.A))
        {
            //MoveToLane(currentLane - 1); // Move left.
            StartCoroutine(MoveToLane(currentLane - 1));

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            //MoveToLane(currentLane + 1); // Move right.
            StartCoroutine(MoveToLane(currentLane + 1));

        }
    }

    private IEnumerator MoveToLane(int targetLane)
    {
        // Clamp the target lane within the valid range (0 to 2).
        targetLane = Mathf.Clamp(targetLane, 0, lanes.Length - 1);

        // Calculate the target position based on the lane index.
        Vector3 targetPosition = lanes[targetLane].position;

        // Update the current lane.
        currentLane = targetLane;

        float duration = 0.5f; // Adjust the dash duration as needed.

        float startTime = Time.time;
        initialPosition = transform.position;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            //transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            transform.position = new Vector3(Mathf.Lerp(initialPosition.x, targetPosition.x, t), transform.position.y, transform.position.z);

            yield return null;
        }

        // Ensure that the final position is exactly the target position.
        transform.position = targetPosition;
    }

}
