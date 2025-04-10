using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCrosshair : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject crosshair;
    private GameObject gameManager;

    [SerializeField] private float dashStr = 20f;
    [SerializeField] private float delay = 1f;

    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyStartingHealth = 100f;

    public float xpFromBasicEnemy = 30f;

    private Vector3 crosshairPosition = Vector3.zero;

    private Rigidbody2D rb;

    public UnityEvent OnBegin;
    public UnityEvent OnDone;

    Experience expScript;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = enemyStartingHealth;

        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        expScript = gameManager.GetComponent<Experience>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            crosshairPosition = player.transform.position;
            Debug.Log("attack incoming");
            OnBegin?.Invoke();
            rb.linearVelocity = Vector3.zero;
            StartCoroutine(Reset());
        }
    }

    private void Update()
    {
        if(enemyHealth <= 0)
        {
            expScript.GetExp(xpFromBasicEnemy);
            Destroy(gameObject);
        }
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

    public void Knockback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        rb.linearVelocity = Vector3.zero;
        StartCoroutine(GotHit(sender));
    }

    private IEnumerator GotHit(GameObject sendder)
    {
        Vector3 knockbackDirection = (transform.position - sendder.transform.position).normalized;
        rb.AddForce(knockbackDirection * 10f, ForceMode2D.Impulse);
        enemyHealth = enemyHealth - 25f;
        yield return new WaitForSeconds(1f);
        //transform.position = new Vector3(Random.Range(-32.0f, 32.0f), Random.Range(-17.0f, 17.0f));
        OnDone?.Invoke();
        //Destroy()
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
