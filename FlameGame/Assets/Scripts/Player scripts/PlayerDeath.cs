using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    public float playerHealth;

    [SerializeField] private GameObject playerFloatingHealthBar;

    [SerializeField] private float basicEnemyDamage = 20f;
    [SerializeField] private float passiveDegeneration = 1f;
    [SerializeField] private float playerStartingHealth = 100f;




    void Start()
    {
        playerHealth = playerStartingHealth;
        InvokeRepeating("OncePerSecound", 0, 1.0f);
    }

    void Update()
    {
        if(playerHealth <= 0)
        {
            Debug.Log("player died");
            transform.position = Vector3.zero;
            playerHealth = playerStartingHealth;
        }

        //playerFloatingHealthBar.transform.localScale = new Vector3 (playerHealth / (100 / 3), 0.5f, 1f);
    }

    private void OncePerSecound()
    {
        playerHealth = playerHealth - passiveDegeneration; //candle mechanic
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyHitbox"))
        {
            Debug.Log("player got hit");
            playerHealth = playerHealth - basicEnemyDamage;
        }
    }
}
