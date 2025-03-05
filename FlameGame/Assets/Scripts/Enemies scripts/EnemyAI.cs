using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    [SerializeField] private float enemyFollowSpeed = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;

        rb.linearVelocity = direction * enemyFollowSpeed;
    }
}
