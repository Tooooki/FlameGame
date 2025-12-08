using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    [Header("Avoidance Settings")]
    [SerializeField] private float avoidanceRadius = 1f; // Radius around boss to avoid
    [SerializeField] private LayerMask bossLayer;        // Layer assigned to the boss

    private Rigidbody2D rb;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (GAME.Player == null) return;

        Vector3 direction = (GAME.Player.transform.position - transform.position).normalized;

        // Check for nearby boss to avoid
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius, bossLayer);
        foreach (var hit in hits)
        {
            Vector3 away = (transform.position - hit.transform.position).normalized;
            direction += away; // push away from boss
        }

        direction.Normalize();

        // Apply movement
        rb.linearVelocity = direction * GAME.enemyRunnerMoveVelocity;

        // Flip sprite based on horizontal movement
        if (direction.x < 0)
            transform.GetChild(0).localRotation = Quaternion.Euler(0, 180, 0);
        else
            transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the avoidance radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, avoidanceRadius);
    }
}
