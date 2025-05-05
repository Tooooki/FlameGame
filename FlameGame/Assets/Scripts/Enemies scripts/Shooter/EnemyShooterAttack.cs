using UnityEngine;

public class EnemyShooterAttack : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;
    public bool canShoot = true;
    [SerializeField] private GameObject projectilePrefab;
    public float projectileSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        InvokeRepeating("RandomizeShooting", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void RandomizeShooting()
    {
        if (Random.Range(0, 3) == 0 && canShoot)
        {
            GameObject projectile;
            projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            projectile.SetActive(true);
            Rigidbody2D projectileRb;
            projectileRb = projectile.GetComponent<Rigidbody2D>();
            Vector3 direction = (GAME.Player.transform.position - projectile.transform.position);
            projectileRb.linearVelocity = direction.normalized * projectileSpeed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
        }
    }
}
