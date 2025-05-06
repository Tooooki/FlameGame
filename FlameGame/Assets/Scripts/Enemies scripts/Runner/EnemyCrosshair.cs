using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyCrosshair : MonoBehaviour
{
    [SerializeField] private GameObject healthBarPrefab;
    private Slider healthSlider;
    private GameObject healthBarInstance;

    public float enemyHealth;

    private Vector3 AttackTargetPosition = Vector3.zero;

    public UnityEvent OnBegin;
    public UnityEvent OnDone;

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
        healthSlider.value = enemyHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //KOD DO PRZEPISANIA
            StopAllCoroutines();
            AttackTargetPosition = collision.transform.position;
            OnBegin?.Invoke();
            StartCoroutine(Reset());
        }
    }

    private void Update()
    {
        healthSlider.value = enemyHealth;
    }

    private IEnumerator Reset()
    {
        //KOD DO PRZEPISANIA
        yield return new WaitForSeconds(0.25f);

        Vector3 direction = (AttackTargetPosition - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(direction * GAME.enemyRunnerDashVelocity, ForceMode2D.Impulse);

        yield return new WaitForSeconds(2f);
        OnDone?.Invoke();
    }

    public void GetDamage(float damage)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
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