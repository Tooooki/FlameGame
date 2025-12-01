using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject bossProjectile;
    public float projectileSpeed = 10f;

    [Header("Shooting Settings")]
    public bool canShoot = true;
    public float shootInterval = 0.5f;
    [Range(0f, 1f)]
    public float shootChance = 0.5f;

    [Header("Optional Shoot Point")]
    [SerializeField] private Transform shootPoint;

    private Transform player;

    private void Awake()
    {
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
        // Stop shooting if the script is disabled or canShoot is false
        if (!enabled || !canShoot || player == null)
            return;

        if (Random.value > shootChance)
            return;

        Vector3 spawnPos = shootPoint != null ? shootPoint.position : transform.position;
        GameObject projectile = Instantiate(bossProjectile, spawnPos, Quaternion.identity);

        Vector2 direction = player.position - spawnPos;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = direction.normalized * projectileSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    /// <summary>
    /// Call this to completely stop the BasicAttack (used when switching phases)
    /// </summary>
    public void StopAttack()
    {
        canShoot = false;
        CancelInvoke(nameof(TryShoot)); // stops InvokeRepeating immediately
        StopAllCoroutines();           // in case you ever use coroutines in the future
    }
}
