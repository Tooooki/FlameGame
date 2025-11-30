using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject bossProjectile;   // Assign BossProjectile prefab
    public float projectileSpeed = 10f;                   // Speed of the projectile

    [Header("Shooting Settings")]
    public bool canShoot = true;
    public float shootInterval = 0.5f;                    // How often TryShoot is called
    [Range(0f, 1f)]
    public float shootChance = 0.5f;                      // Chance to fire each interval (0 = never, 1 = always)

    [Header("Optional Shoot Point")]
    [SerializeField] private Transform shootPoint;        // Optional child transform for projectile spawn

    private Transform player;

    private void Awake()
    {
        // Find the player in the scene
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("BasicAttack: No GameObject with tag 'Player' found in the scene!");
        }

        // Start repeating shooting
        InvokeRepeating(nameof(TryShoot), 0f, shootInterval);
    }

    private void TryShoot()
    {
        if (!canShoot || player == null)
            return;

        // Random chance to shoot
        if (Random.value > shootChance)
            return;

        // Determine spawn position
        Vector3 spawnPos = shootPoint != null ? shootPoint.position : transform.position;

        // Instantiate projectile
        GameObject projectile = Instantiate(bossProjectile, spawnPos, Quaternion.identity);

        // Aim at player
        Vector2 direction = player.position - spawnPos;

        // Assign velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction.normalized * projectileSpeed;
        }
        else
        {
            Debug.LogWarning("BasicAttack: BossProjectile prefab has no Rigidbody2D!");
        }

        // Rotate projectile to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}