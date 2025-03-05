using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float maxSpeed = 10f;
    [SerializeField] private float acceleration = 32f;
    [SerializeField] private float decelaration = 4f;

    private bool tooHighSpeed;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(rb.linearVelocity.magnitude > maxSpeed)
        {
            tooHighSpeed = true;
        }
        else
        {
            tooHighSpeed = false;
        }
    }
    private void FixedUpdate()
    {
        if(InputManager.Movement != Vector2.zero)
        {
            if (tooHighSpeed)
            {
                rb.AddForce(rb.linearVelocity * -decelaration, ForceMode2D.Force);
            } 
            else
            {
                rb.AddForce(InputManager.Movement * acceleration, ForceMode2D.Force);
            }
        } 
        else
        {
            rb.AddForce(rb.linearVelocity * -decelaration * 4, ForceMode2D.Force);
        }
    }
}
