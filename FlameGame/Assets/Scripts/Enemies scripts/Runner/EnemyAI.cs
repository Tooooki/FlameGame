using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private GameObject Player;

    [SerializeField] private float enemyFollowSpeed = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector3 direction = (Player.transform.position - transform.position).normalized;

        rb.linearVelocity = direction * enemyFollowSpeed;
    }
}
