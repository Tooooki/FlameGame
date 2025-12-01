using UnityEngine;

public class BossSecondPhaseAttack : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private GameObject projectilePrefab;

    [Header("Adjustable in Inspector")]
    [SerializeField] private float projectileSpeed = 10f;      // How fast the bullets move
    [SerializeField] private int bulletsPerCircle = 12;        // Number of bullets in each circular shot
    [SerializeField] private float fireRate = 1.5f;            // Seconds between each circular shot
    [SerializeField] private float angleStep = 30f;            // Rotation of circle each shot

    private float fireTimer = 0f;
    private float currentAngle = 0f;

    private void Awake()
    {
        enabled = false; // Disabled by default; enabled during phase two
    }

    private void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f)
        {
            ShootCircularPattern();
            fireTimer = fireRate;
        }
    }

    private void ShootCircularPattern()
    {
        for (int i = 0; i < bulletsPerCircle; i++)
        {
            float angle = currentAngle + i * (360f / bulletsPerCircle);
            Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;

            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity = dir * projectileSpeed;

            // Ensure cleanup on boss death
            proj.tag = "BossProjectile";
        }

        // Rotate the circle slightly for next shot
        currentAngle += angleStep;
    }

    // Optional: public functions to modify values at runtime
    public void SetFireRate(float newRate)
    {
        fireRate = newRate;
    }

    public void SetBulletsPerCircle(int newCount)
    {
        bulletsPerCircle = newCount;
    }
}
