using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class PlayerBasicProjectile : MonoBehaviour
{
    [SerializeField] private ParticleSystem fire;
    
    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("walls"))
        {
            GAME.audioManager.PlaySFX(GAME.audioManager.fireballHitWall);

            StartCoroutine(projectileExplosion());
        }


        if (collision.CompareTag("EnemyHitbox"))
        {
            if(collision.gameObject.transform.parent.gameObject.GetComponent<EnemyCrosshair>() != null)
                collision.gameObject.transform.parent.gameObject.GetComponent<EnemyCrosshair>().GetDamage(GAME.playerBasicAttackDamage); // Apply damage to Runner
            else if(collision.gameObject.transform.parent.gameObject.GetComponent<DamageEnemyShooter>() != null)
                collision.gameObject.transform.parent.gameObject.GetComponent<DamageEnemyShooter>().LoseHP(GAME.playerBasicAttackDamage); // Apply damage to Shooter

            StartCoroutine(projectileExplosion());
        }


        if (collision.CompareTag("Clutter"))
        {
            StartCoroutine(projectileExplosion());

            Destroy(collision.gameObject);
        }
    }

    private IEnumerator projectileExplosion()
    {
        float timer;
        var shape = fire.shape;
        var emission = fire.emission;
        var main = fire.main;

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        GetComponent<CircleCollider2D>().enabled = false;

        main.startLifetime = 0.5f;

        timer = 0.3f;

        while(timer > 0f)
        {
            timer -= Time.deltaTime;

            shape.radius = 1 / (timer + 0.3f);
            emission.rateOverTime = (timer * 2666) - 400;

            yield return null;
        }

        Destroy(this.gameObject);
    }
}