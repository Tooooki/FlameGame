using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
public class PlayerDeath : MonoBehaviour
{
    public float playerHealth;
    public float playerMaxHealth = 100f;
    public float healingValue = 15f;
    public float basicEnemyDamage = 20f;
    public float shooterEnemyDamage = 25f;
    //public float passiveDegeneration = 1f;
    
    [SerializeField] private float knockbackStrength;

    [SerializeField] private Image healthBar;

    [SerializeField] private GameObject healingPotion;

    private Rigidbody2D rb;

    PlayerIframes IframesScript;
    PlayerInRooms CameraScript;


    void Start()
    {
        playerHealth = playerMaxHealth;
    }


    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        IframesScript = GetComponentInParent<PlayerIframes>();
        CameraScript = GetComponentInParent<PlayerInRooms>();
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
            StartCoroutine(cameraShake());
            GameObject healItem = Instantiate(healingPotion, transform.position, Quaternion.identity);
            StartCoroutine(healPotionSpawnDelay(healItem));
        }

        if (collision.CompareTag("EnemyAttack"))
        {

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

    private IEnumerator healPotionSpawnDelay(GameObject healItem)
    {
        yield return new WaitForSeconds(1f);
        healItem.GetComponent<BoxCollider2D>().enabled = true;
    }

    private IEnumerator cameraShake()
    {
        CameraScript.isCameraShaking = true;
        yield return new WaitForSeconds(0.2f);
        CameraScript.isCameraShaking = false;
    }
}
