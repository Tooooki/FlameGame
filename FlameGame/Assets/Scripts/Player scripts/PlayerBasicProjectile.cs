using UnityEngine;
using UnityEngine.Events;
using Unity.VisualScripting;
public class PlayerBasicProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject self;
    [SerializeField] private CircleCollider2D projectileCollider;

    private GameObject enemyShoot;
    private EnemyCrosshair enemyScript;

    // Bullet damage variable
    public float BulletDamage { get; private set; }


    public UnityEvent OnWallHit;
    public UnityEvent OnEnemyHit;

    GAMEGLOBALMANAGEMENT GAME;

    void Start()
    {
        // Set bullet damage from player stats
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                BulletDamage = playerStats.BulletDamage;  // Get bullet damage from player stats
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    void Update()
    {
        // Update logic for the bullet (if any)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("walls"))
        {
            OnWallHit?.Invoke();
            rb.linearVelocity = Vector2.zero;
            projectileCollider.enabled = false;
            Destroy(self, 0.2f);
        }
        
        if (collision.CompareTag("EnemyHitbox"))
        {
            OnEnemyHit?.Invoke();
            rb.linearVelocity = Vector2.zero;
            projectileCollider.enabled = false;

            // Get the enemy script and apply the damage
            enemyShoot = collision.gameObject.transform.parent.gameObject;
            enemyScript = enemyShoot.GetComponent<EnemyCrosshair>();
            if (enemyScript != null)
            {
                // Apply damage to the enemy (pass BulletDamage)
                enemyScript.Knockback(gameObject, GAME.playerBasicAttackDamage);  // Pass BulletDamage to Knockback method
            }else
            {
                collision.gameObject.transform.parent.gameObject.GetComponent<DamageEnemyShooter>().LoseHP(GAME.playerBasicAttackDamage);
            }

                Destroy(self, 0.2f);
        }
    }
}