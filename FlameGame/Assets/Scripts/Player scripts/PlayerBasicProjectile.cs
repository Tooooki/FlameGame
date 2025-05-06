using UnityEngine;
using UnityEngine.Events;
public class PlayerBasicProjectile : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private GameObject self;
    [SerializeField] private CircleCollider2D projectileCollider;


    public UnityEvent OnWallHit;
    public UnityEvent OnEnemyHit;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("walls"))
        {
            OnWallHit?.Invoke();
            rb.linearVelocity = Vector2.zero;
            projectileCollider.enabled = false;

            Destroy(this.gameObject, 0.2f);
        }

        if (collision.CompareTag("EnemyHitbox"))
        {
            OnEnemyHit?.Invoke();
            rb.linearVelocity = Vector2.zero;
            projectileCollider.enabled = false;

            if(collision.gameObject.transform.parent.gameObject.GetComponent<EnemyCrosshair>() != null)
                collision.gameObject.transform.parent.gameObject.GetComponent<EnemyCrosshair>().GetDamage(GAME.playerBasicAttackDamage); // Apply damage to Runner
            else if(collision.gameObject.transform.parent.gameObject.GetComponent<DamageEnemyShooter>() != null)
                collision.gameObject.transform.parent.gameObject.GetComponent<DamageEnemyShooter>().LoseHP(GAME.playerBasicAttackDamage); // Apply damage to Shooter

            Destroy(self, 0.2f);
        }
    }
}