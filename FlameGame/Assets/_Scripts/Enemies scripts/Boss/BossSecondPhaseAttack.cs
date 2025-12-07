using UnityEngine;

public class BossSecondPhaseAttack : MonoBehaviour
{
    [Header("PLAYER REFERENCE")]
    private Transform player;

    [Header("CIRCULAR ATTACK SETTINGS")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileDamage = 15f;
    [SerializeField] private int bulletsPerCircle = 12;
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float angleStep = 15f;
    private float fireTimer = 0f;
    private float currentAngle = 0f;

    [Header("MAGIC WALL SETTINGS")]
    [SerializeField] private GameObject magicWallPrefab;
    [SerializeField] private float wallCooldown = 5f;
    [SerializeField] private float magicWallSpeed = 20f;
    private float wallTimer = 0f;

    [Header("ENEMY SPAWN SETTINGS")]
    [SerializeField] private GameObject enemyPrefab;   // Assign enemy prefab in Inspector
    [SerializeField] private float enemySpawnCooldown = 8f; // Seconds between spawns
    [SerializeField] private int maxEnemiesPerSpawn = 3;
    private float enemySpawnTimer = 0f;

    private void Awake()
    {
        enabled = false; // Activate only in Phase 2
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (!player)
            Debug.LogError("BossSecondPhaseAttack: Player not found!");
    }

    private void Update()
    {
        if (!player) return;

        // Update timers
        fireTimer -= Time.deltaTime;
        wallTimer -= Time.deltaTime;
        enemySpawnTimer -= Time.deltaTime;

        // --------------------------
        // Circular Magic Spin
        // --------------------------
        if (fireTimer <= 0f)
        {
            ShootCircularPattern();
            fireTimer = fireRate;
        }

        // --------------------------
        // Magic Wall Attack
        // --------------------------
        if (wallTimer <= 0f && magicWallPrefab != null)
        {
            FireMagicWall();
            wallTimer = wallCooldown;
        }

        // --------------------------
        // Spawn Enemies
        // --------------------------
        if (enemySpawnTimer <= 0f && enemyPrefab != null)
        {
            SpawnEnemies();
            enemySpawnTimer = enemySpawnCooldown;
        }
    }

    // --------------------------
    // Circular Spin Attack
    // --------------------------
    private void ShootCircularPattern()
    {
        if (!projectilePrefab) return;

        for (int i = 0; i < bulletsPerCircle; i++)
        {
            float angle = currentAngle + (i * (360f / bulletsPerCircle));
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.up;

            GameObject projObj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            BossProjectile proj = projObj.GetComponent<BossProjectile>();
            if (proj != null)
            {
                proj.damage = (int)projectileDamage;
                proj.Shoot(direction, projectileSpeed);
            }

            projObj.tag = "BossProjectile";
        }

        currentAngle += angleStep;
    }

    // --------------------------
    // Magic Wall Attack
    // --------------------------
    private void FireMagicWall()
    {
        if (!magicWallPrefab || !player) return;

        Vector2 direction = (player.position - transform.position).normalized;
        GameObject wallObj = Instantiate(magicWallPrefab, transform.position, Quaternion.identity);

        MagicWall wallScript = wallObj.GetComponent<MagicWall>();
        if (wallScript != null)
        {
            wallScript.Initialize(direction, magicWallSpeed);
        }

        wallObj.tag = "BossProjectile";
    }

    // --------------------------
    // Spawn Enemies
    // --------------------------
    private void SpawnEnemies()
    {
        int spawnCount = Random.Range(1, maxEnemiesPerSpawn + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            // Spawn around the boss with a random offset
            Vector2 spawnOffset = Random.insideUnitCircle * 3f;
            Vector3 spawnPos = transform.position + (Vector3)spawnOffset;

            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemy.tag = "Enemy"; // Ensure enemy tag is set
        }
    }

    // --------------------------
    // Public editor-accessible settings
    // --------------------------
    public void SetFireRate(float newRate) => fireRate = newRate;
    public void SetBulletCount(int newCount) => bulletsPerCircle = newCount;
    public void SetProjectileSpeed(float newSpeed) => projectileSpeed = newSpeed;
    public void SetProjectileDamage(float newDamage) => projectileDamage = newDamage;
    public void SetMagicWallSpeed(float newSpeed) => magicWallSpeed = newSpeed;
    public void SetEnemySpawnCooldown(float newCooldown) => enemySpawnCooldown = newCooldown;
    public void SetMaxEnemiesPerSpawn(int newMax) => maxEnemiesPerSpawn = newMax;
}
