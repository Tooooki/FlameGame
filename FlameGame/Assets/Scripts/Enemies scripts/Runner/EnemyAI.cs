using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = (GAME.Player.transform.position - transform.position).normalized;

        GetComponent<Rigidbody2D>().linearVelocity = direction * GAME.enemyRunnerMoveVelocity;
    }
}
