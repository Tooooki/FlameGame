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

        //Set up health bar
        healthBarInstance = Instantiate(healthBarPrefab, transform);
        healthBarInstance.transform.localPosition = new Vector3(0, 2f, 0);

        healthSlider = healthBarInstance.GetComponentInChildren<Slider>();
        healthSlider.maxValue = enemyStartingHealth;
        healthSlider.value = enemyHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Player entered detection zone
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
        //set health bar value
        if (healthSlider != null)
            healthSlider.value = enemyHealth;
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.25f);
        Vector3 direction = (crosshairPosition - transform.position).normalized;
        rb.AddForce(direction * dashStr, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2f);
        OnDone?.Invoke();
    }

    public void GetDamage(float damage)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        rb.linearVelocity = Vector2.zero;
        StartCoroutine(GotHit(damage));  // Pass damage to GotHit
    }

    private IEnumerator GotHit(float damageToTake)
    {
        // Apply the damage to the enemy's health
        enemyHealth -= damageToTake;

        // If enemy health is 0 or less, handle the enemy's death
        if (enemyHealth <= 0)
        {
            Die();
        }

        GAME.Player.GetComponent<PlayerInRooms>().PlayCameraShake(0.1f);

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