using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class EnemyCrosshair : MonoBehaviour
{
    [SerializeField] private GameObject healthBarPrefab;
    private Slider healthSlider;
    private GameObject healthBarInstance;

    public bool isAttacking, isDashing, canDamage;

    public float enemyHealth;

    private float dashTimer;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        enemyHealth = GAME.enemyRunnerMaxHealth;

        //Set up health bar
        healthBarInstance = Instantiate(healthBarPrefab, transform);
        healthBarInstance.transform.localPosition = new Vector3(0, 2f, 0);

        healthSlider = healthBarInstance.GetComponentInChildren<Slider>();
        healthSlider.maxValue = GAME.enemyRunnerMaxHealth;

        isAttacking = false;
        canDamage = false;

        GetComponentInChildren<Light2D>().intensity = 1f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;

            Vector3 AttackDirection;
            AttackDirection = (GAME.Player.transform.position - transform.position);

            GetComponent<EnemyAI>().enabled = false;

            StartCoroutine(PlayAttack(AttackDirection.normalized));
        }
    }

    private void Update()
    {
        healthSlider.value = enemyHealth;

        GetComponentInChildren<Light2D>().intensity = Mathf.Lerp(GetComponentInChildren<Light2D>().intensity, 0f, 10f * Time.deltaTime);

        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    private IEnumerator PlayAttack(Vector3 AttackDirection)
    {
        yield return new WaitForSeconds(0.25f);

        dashTimer = GAME.enemyRunnerDashDuration;

        canDamage = true;

        while (dashTimer > 0)
        {
            GetComponent<Rigidbody2D>().linearVelocity = AttackDirection * GAME.enemyRunnerDashVelocity;
            dashTimer -= Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        canDamage = false;
        GetComponent<EnemyAI>().enabled = true;

        isAttacking = false;
    }

    public void GetDamage(float damage)
    {
        enemyHealth -= damage;
        GAME.Player.GetComponent<PlayerInRooms>().PlayCameraShake(0.1f);
    }

    private void Die()
    {
        // Handle death (e.g., award XP, destroy enemy object)
        GAME.PlayerGetExperience(GAME.enemyRunnerExperienceDrop);
        Destroy(healthBarInstance);
        Destroy(gameObject);
    }
}