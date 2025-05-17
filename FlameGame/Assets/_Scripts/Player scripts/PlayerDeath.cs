using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class PlayerDeath : MonoBehaviour
{
    public float healingValue = 10f;

    public float passiveDegeneration;

    [SerializeField] private Image healthBar;

    [SerializeField] private GameObject healingPotionPrefab;


    GAMEGLOBALMANAGEMENT GAME;


    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        passiveDegeneration = 0.1f;
        
        InvokeRepeating("PassiveDegeneration", 0, 1.0f);

        GAME.playerCurrentHealth = GAME.playerMaxHealth;
    }

    void Update()
    {
        if (GAME.playerCurrentHealth <= 0)
        {
            GetComponentInParent<Rigidbody2D>().transform.position = new Vector3(0, 0, 0);
            GAME.playerCurrentHealth = GAME.playerMaxHealth;
            //die
        }

        healthBar.transform.localScale = new Vector3(GAME.playerCurrentHealth / GAME.playerMaxHealth, healthBar.transform.localScale.y);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Healing"))
        {
            if (GAME.playerMaxHealth - GAME.playerCurrentHealth >= 15 && GAME.playerMaxHealth - GAME.playerCurrentHealth < GAME.playerMaxHealth)
            {
                GAME.playerCurrentHealth += healingValue;
                Destroy(collision.gameObject);
            }
            else if (GAME.playerMaxHealth - GAME.playerCurrentHealth < 15)
            {
                GAME.playerCurrentHealth = GAME.playerMaxHealth;
                Destroy(collision.gameObject);
            }
        }
    }



    private void PassiveDegeneration()
    {
        GAME.playerCurrentHealth -= passiveDegeneration;
    }

    public void DamageResult()
    {
        GameObject healItem = Instantiate(healingPotionPrefab, transform.position, Quaternion.identity);
        StartCoroutine(healPotionSpawnDelay(healItem));
        GAME.Player.GetComponent<PlayerInRooms>().PlayCameraShake(0.2f);
    }

    private IEnumerator healPotionSpawnDelay(GameObject healItem)
    {
        yield return new WaitForSeconds(2f);
        healItem.GetComponent<BoxCollider2D>().enabled = true;
    }
}
