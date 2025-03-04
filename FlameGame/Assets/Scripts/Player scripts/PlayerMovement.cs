using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 20f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float decelaratiom = 5f;
    [SerializeField] private bool dashActive = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (InputManager.Movement != Vector2.zero)
        {
            rb.AddForce(InputManager.Movement * acceleration, ForceMode2D.Force);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        } else
        {
            rb.AddForce(rb.linearVelocity * -decelaratiom, ForceMode2D.Force);
        }
    }
}
