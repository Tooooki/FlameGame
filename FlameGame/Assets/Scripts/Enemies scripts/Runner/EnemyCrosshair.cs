using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyCrosshair : MonoBehaviour
{
    [SerializeField] private GameObject healthBarPrefab;
    private Slider healthSlider;
    private GameObject healthBarInstance;

    private GameObject player;
    private GameObject gameManager;
    [SerializeField] private GameObject crosshair;

    [SerializeField] private float dashStr = 20f;
    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyStartingHealth = 100f;

    public float xpFromBasicEnemy = 30f;
    private Vector3 crosshairPosition = Vector3.zero;

    private Rigidbody2D rb;
    public UnityEvent OnBegin;
    public UnityEvent OnDone;
    Experience expScript;

    GAMEGLOBALMANAGEMENT GAME;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = enemyStartingHealth;

        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        expScript = gameManager.GetComponent<Experience>();
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        healthBarInstance = Instantiate(healthBarPrefab, transform);
        healthBarInstance.transform.localPosition = new Vector3(0, 2f, 0); // adjust above enemy head

        healthSlider = healthBarInstance.GetComponentInChildren<Slider>();
        healthSlider.maxValue = enemyStartingHealth;
        healthSlider.value = enemyHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            crosshairPosition = player.transform.position;
            OnBegin?.Invoke();
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(Reset());
        }
    }

    private void Update()
    {
        if (healthSlider != null)
            healthSlider.value = enemyHealth;
    }

    private void FixedUpdate()
    {
        crosshair.transform.position = crosshairPosition;
        rb.AddForce(rb.linearVelocity.normalized * -1 * 30f, ForceMode2D.Force);
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.25f);
        Vector3 direction = (crosshairPosition - transform.position).normalized;
        rb.AddForce(direction * dashStr, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);
        OnDone?.Invoke();
    }

    public void Knockback(GameObject sender, float damage)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(GotHit(sender, damage));  // Pass damage to GotHit
    }

    private IEnumerator GotHit(GameObject sender, float damageToTake)
    {
        // Calculate knockback direction
        Vector3 knockbackDirection = (transform.position - sender.transform.position).normalized;
        rb.AddForce(knockbackDirection * 10f, ForceMode2D.Impulse);

        // Apply the damage to the enemy's health
        enemyHealth -= damageToTake;

        GAME.Player.GetComponent<PlayerInRooms>().PlayCameraShake(0.1f);

        // If enemy health is 0 or less, handle the enemy's death
        if (enemyHealth <= 0)
        {
            Die();
        }

        yield return new WaitForSeconds(1f);  // Simulate delay after hit
        OnDone?.Invoke();
    }

    private void Die()
    {
        // Handle death (e.g., award XP, destroy enemy object)
        GAME.PlayerGetExperience(GAME.enemyRunnerExperienceDrop);
        Destroy(healthBarInstance);
        Destroy(gameObject);
    }
}