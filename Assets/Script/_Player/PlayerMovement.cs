using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
        public float speed = 5f;
        public float rotationSpeed = 720f; // Degrees per second

    float velX = 0f;

    private void Update()
    {
        velX = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        // Move the player forward
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);
        // Rotate the player based on horizontal input
        transform.Rotate(Vector3.forward, -velX * rotationSpeed * Time.fixedDeltaTime);
    }
}
