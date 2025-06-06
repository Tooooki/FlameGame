using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ProjectileEnemyShooter : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;

    [SerializeField] private ParticleSystem waterBall;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("PlayerHitbox"))
        {
            GAME.PlayerGetDamage(GAME.enemyShooterProjectileDamage);
            StartCoroutine(ProjectileSplash()); 
        }

        if(collision.CompareTag("walls"))
        {
            StartCoroutine(ProjectileSplash());
        }

        if (collision.CompareTag("Clutter"))
        {
            StartCoroutine(ProjectileSplash());
        }

        if (collision.CompareTag("Chest"))
        {
            StartCoroutine(ProjectileSplash());
        }
    }

    public void Explode()
    {
        StartCoroutine(ProjectileSplash());
    }

    private IEnumerator ProjectileSplash()
    {
        float timer;
        var shape = waterBall.shape;
        var emission = waterBall.emission;
        var main = waterBall.main;

        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        GetComponent<CircleCollider2D>().enabled = false;

        GetComponentInChildren<Light2D>().intensity = 0.5f;

        main.startLifetime = 0.5f;

        timer = 0.3f;

        while (timer > 0f)
        {
            timer -= Time.deltaTime;

            shape.radius = 1 / (timer + 0.3f);
            emission.rateOverTime = (timer * 2666) - 400;

            GetComponentInChildren<Light2D>().intensity = Mathf.Lerp(GetComponentInChildren<Light2D>().intensity, 0f, 10f * Time.deltaTime);

            yield return null;
        }

        Destroy(this.gameObject);
    }
}
