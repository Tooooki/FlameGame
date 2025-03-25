using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class PlayerDeath : MonoBehaviour
{
    public float playerHealth;
    public float playerMaxHealth = 100f;
    public float healingValue = 15f;
    [SerializeField] private float basicEnemyDamage = 20f;
    [SerializeField] private float passiveDegeneration = 1f;
    
    [SerializeField] private float knockbackStrength;

    [SerializeField] private Image healthBar;

    private Rigidbody2D rb;

    PlayerIframes IframesScript;


    void Start()
    {
        playerHealth = playerMaxHealth;
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
            rb.transform.position = new Vector3(0, 0, 0);
            playerHealth = playerMaxHealth;
        }

        healthBar.transform.localScale = new Vector3(playerHealth / playerMaxHealth, healthBar.transform.localScale.y);
    }


    private void OncePerSecound()
    {
        //playerHealth = playerHealth - passiveDegeneration;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHitbox"))
        {
            playerHealth = playerHealth - basicEnemyDamage;
            //IframesScript.Iframes();
            Vector3 enemyPos = new Vector3(collision.transform.position.x, collision.transform.position.y).normalized;
            rb.AddForce((rb.transform.position.normalized - enemyPos) * knockbackStrength, ForceMode2D.Impulse);
        }
        
        if (collision.CompareTag("Healing"))
        {
            if(playerMaxHealth - playerHealth >= 15 && playerMaxHealth - playerHealth < playerMaxHealth)
            {
                playerHealth = playerHealth + healingValue;
                Destroy(collision.gameObject);
            } 
            else if(playerMaxHealth - playerHealth < 15)
            {
                playerHealth = playerMaxHealth;
                Destroy(collision.gameObject);
            }
        }
    }
}
