using UnityEngine;

public class MagicSpin : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject bossProjectile;  // Assign BossProjectile prefab
    public int projectileCount = 12;                     // Number of projectiles in circle
    public float projectileSpeed = 10f;                  // Speed of projectiles

    [Header("Shooting Settings")]
    public float cooldown = 5f;                          // Time between circular attacks
    private float timer = 0f;

    [Header("Optional Spawn Point")]
    [SerializeField] private Transform spawnPoint;       // If null, uses boss position

    private GAMEGLOBALMANAGEMENT gameManager;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager")?.GetComponent<GAMEGLOBALMANAGEMENT>();
        if (gameManager == null)
            Debug.LogError("MagicSpin: No GameManager found in the scene!");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cooldown)
        {
            ShootCircular();
            timer = 0f;
        }
    }

    private void ShootCircular()
    {
        if (bossProjectile == null || gameManager == null)
            return;

        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : transform.position;

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = i * (360f / projectileCount);
            float rad = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            // Instantiate projectile
            GameObject proj = Instantiate(bossProjectile, spawnPos, Quaternion.identity);
            BossProjectile bp = proj.GetComponent<BossProjectile>();
            if (bp != null)
            {
                bp.damage = (int)gameManager.enemyShooterProjectileDamage; // Use global damage
                bp.Shoot(direction, projectileSpeed);
            }
        }
    }
}