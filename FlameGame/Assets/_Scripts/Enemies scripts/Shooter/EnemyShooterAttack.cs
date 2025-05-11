using UnityEngine;

public class EnemyShooterAttack : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;


    public bool canShoot = true;
    [SerializeField] private GameObject projectilePrefab;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        InvokeRepeating("RandomizeShooting", 0f, 1f);
    }

    private void RandomizeShooting()
    {
        if (Random.Range(0, 3) == 0 && canShoot)
        {
            GameObject projectile;
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.SetActive(true);
            GAME.audioManager.PlaySFX(GAME.audioManager.enemyShootingBow);

            Vector3 direction = (GAME.Player.transform.position - projectile.transform.position);
            projectile.GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * GAME.enemyShooterProjectileVelocity;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        }
    }
}
