using UnityEngine;

public class MagicWall : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed = 20f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if(rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    // Call this when you spawn the wall
    public void Initialize(Vector2 direction, float speed)
    {
        moveDirection = direction.normalized;
        moveSpeed = speed;

        // Rotate wall to face movement direction
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        // Apply velocity immediately
        if(rb != null)
            rb.velocity = moveDirection * moveSpeed;
    }

    private void Update()
    {
        // Optional: destroy after leaving screen or after some time
        // Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            // Deal damage to player
            GAMEGLOBALMANAGEMENT gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
            if(gameManager != null)
                gameManager.PlayerGetDamage(20); // or configurable

            Destroy(gameObject);
        }

        if(collision.CompareTag("walls") || collision.CompareTag("Clutter") || collision.CompareTag("Chest"))
        {
            Destroy(gameObject);
        }
    }
}
