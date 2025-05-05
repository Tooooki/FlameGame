using UnityEngine;

public class ProjectileEnemyShooter : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerHitbox"))
        {
            GAME.Player.GetComponentInChildren<PlayerDeath>().GetDamage(GAME.enemyShooterProjectileDamage);
            Destroy(this.gameObject);
        }

        if(collision.CompareTag("walls"))
        {
            Destroy(this.gameObject);
        }
    }
}
