using UnityEngine;

public class RunnerDealDamage : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            GAME.PlayerGetDamage(GAME.enemyRunnerDamage);
        }
    }
}
