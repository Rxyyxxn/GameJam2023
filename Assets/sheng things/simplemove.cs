using UnityEngine;

public class simplemove : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Adjust the movement speed as needed.
    public float jumpForce = 7.0f; // Adjust the jump force as needed.
    private bool isGrounded;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the player is grounded.
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f);

        // Horizontal movement (WASD keys).
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        movement = transform.TransformDirection(movement);
        movement *= moveSpeed * Time.deltaTime;

        // Apply movement.
        rb.MovePosition(transform.position + movement);

        // Jumping (Space key).
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
