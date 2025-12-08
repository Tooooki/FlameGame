using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int damage = 15;                        // Damage dealt to the player
    public ParticleSystem hitEffect;              // Optional particle effect on hit

    private Rigidbody2D rb;
    private GAMEGLOBALMANAGEMENT gameManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if(rb == null)
            Debug.LogError("BossProjectile requires a Rigidbody2D component!");

        // Find the global game manager
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GAMEGLOBALMANAGEMENT>();
        if(gameManager == null)
            Debug.LogError("BossProjectile: No GameManager found in the scene!");
    }

    // Call this method immediately after instantiating the projectile
    public void Shoot(Vector2 direction, float speed)
    {
        if(rb != null)
        {
            rb.linearVelocity = direction.normalized * speed;
        }

        // Rotate projectile to face direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Hit player
        if(collision.CompareTag("Player") && gameManager != null)
        {
            gameManager.PlayerGetDamage(damage);
            Explode();
        }

        // Hit walls, clutter, chest
        if(collision.CompareTag("walls") || collision.CompareTag("Clutter") || collision.CompareTag("Chest"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        // Stop movement
        if(rb != null)
            rb.linearVelocity = Vector2.zero;

        // Play particle effect if assigned
        if(hitEffect != null)
            Instantiate(hitEffect, transform.position, Quaternion.identity);

        // Destroy projectile
        Destroy(gameObject);
    }
}
