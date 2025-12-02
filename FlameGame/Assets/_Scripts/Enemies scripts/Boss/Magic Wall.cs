using UnityEngine;

public class MagicWall : MonoBehaviour
{
    [Header("Wall Settings")]
    [SerializeField] private float speed = 5f;          // Movement speed
    [SerializeField] private float lifetime = 8f;       // How long the wall lasts before disappearing
    [SerializeField] private int damage = 10;           // Damage to the player

    private Vector2 direction;                           // Direction of movement

    private void Start()
    {
        // Destroy after lifetime expires
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the wall
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }

    // Call this from the boss script after instantiating
    public void Initialize(Vector2 dir)
    {
        direction = dir.normalized;
    }

    // Optional: allow boss to override speed at runtime
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Damage the player
            GAMEGLOBALMANAGEMENT gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GAMEGLOBALMANAGEMENT>();
            if (gameManager != null)
            {
                gameManager.PlayerGetDamage(damage);
            }

            Destroy(gameObject);
        }

        // Destroy when hitting walls or obstacles
        if (collision.CompareTag("walls") || collision.CompareTag("Clutter") || collision.CompareTag("Chest"))
        {
            Destroy(gameObject);
        }
    }
}