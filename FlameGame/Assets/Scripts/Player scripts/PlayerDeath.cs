using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class PlayerDeath : MonoBehaviour
{
    public float playerHealth;

    [SerializeField] private float basicEnemyDamage = 20f;
    [SerializeField] private float passiveDegeneration = 1f;
    [SerializeField] private float playerStartingHealth = 100f;
    [SerializeField] private float knockbackStrength;

    [SerializeField] private Image healthBar;

    private Rigidbody2D rb;

    PlayerIframes IframesScript;


    void Start()
    {
        playerHealth = playerStartingHealth;
    }


    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        IframesScript = GetComponentInParent<PlayerIframes>();
        InvokeRepeating("OncePerSecound", 0, 1.0f);
    }


    void Update()
    {
        if(playerHealth <= 0)
        {
            Debug.Log("player died");
            rb.transform.position = new Vector3(0, 0, 0);
            playerHealth = playerStartingHealth;
        }

        healthBar.transform.localScale = new Vector3(playerHealth / 100, healthBar.transform.localScale.y);
    }


    private void OncePerSecound()
    {
        playerHealth = playerHealth - passiveDegeneration;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHitbox"))
        {
            Debug.Log("player got hit");
            playerHealth = playerHealth - basicEnemyDamage;
            IframesScript.Iframes();
            //Vector3 enemyPos = new Vector3(collision.transform.position.x, collision.transform.position.y).normalized;
            //rb.AddForce((rb.transform.position.normalized - enemyPos) * knockbackStrength, ForceMode2D.Impulse);
        }
    }
}
