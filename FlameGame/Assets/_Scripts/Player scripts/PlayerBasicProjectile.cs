using UnityEngine;
using UnityEngine.Events;
public class PlayerBasicProjectile : MonoBehaviour
{
    public UnityEvent OnWallHit;
    public UnityEvent OnEnemyHit;
    audioManager audioManager;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("walls"))
        {
            OnWallHit?.Invoke();

            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            GetComponent<CircleCollider2D>().enabled = false;
            GAME.audioManager.PlaySFX(GAME.audioManager.fireballHitWall);
            Destroy(this.gameObject, 0.2f);
        }


        if (collision.CompareTag("EnemyHitbox"))
        {
            OnEnemyHit?.Invoke();

            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            GetComponent<CircleCollider2D>().enabled = false;

            if(collision.gameObject.transform.parent.gameObject.GetComponent<EnemyCrosshair>() != null)
                collision.gameObject.transform.parent.gameObject.GetComponent<EnemyCrosshair>().GetDamage(GAME.playerBasicAttackDamage); // Apply damage to Runner
            else if(collision.gameObject.transform.parent.gameObject.GetComponent<DamageEnemyShooter>() != null)
                collision.gameObject.transform.parent.gameObject.GetComponent<DamageEnemyShooter>().LoseHP(GAME.playerBasicAttackDamage); // Apply damage to Shooter

            Destroy(this.gameObject, 0.2f);
        }


        if (collision.CompareTag("Clutter"))
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            GetComponent<CircleCollider2D>().enabled = false;

            Destroy(this.gameObject, 0.2f);

            Destroy(collision.gameObject);
        }
    }
}