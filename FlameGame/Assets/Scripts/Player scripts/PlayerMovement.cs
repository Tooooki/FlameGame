using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 10f;

    private Rigidbody2D rb;

    public bool movementActive = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (movementActive)
        {
            rb.linearVelocity = InputManager.Movement * maxSpeed;
        }
    }
}
