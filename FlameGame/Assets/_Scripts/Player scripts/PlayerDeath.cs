using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [Header("Player Settings")]
    public float passiveDegeneration = 0.1f;

    [Header("UI Elements")]
    [SerializeField] private Image deathFadeImage;   // Fullscreen black image
    [SerializeField] private Image healthBar;

    private GAMEGLOBALMANAGEMENT GAME;
    private bool isDead = false;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager")
            .GetComponent<GAMEGLOBALMANAGEMENT>();

        // Ensure player starts with full health
        GAME.playerCurrentHealth = GAME.playerMaxHealth;

        // Ensure fade image starts invisible
        if (deathFadeImage != null)
        {
            Color c = deathFadeImage.color;
            c.a = 0f;
            deathFadeImage.color = c;
        }

        InvokeRepeating(nameof(PassiveDegeneration), 0, 1f);
    }

    private void Update()
    {

        // Update health bar scale
        if (healthBar != null)
            healthBar.transform.localScale =
                new Vector3(GAME.playerCurrentHealth / GAME.playerMaxHealth, 1, 1);

        // Trigger death sequence
        if (!isDead && GAME.playerCurrentHealth <= 0)
        {
            StartCoroutine(PlayerDeathSequence());
        }
    }

    private void PassiveDegeneration()
    {
        GAME.playerCurrentHealth -= passiveDegeneration;
    }

    private IEnumerator PlayerDeathSequence()
    {
        isDead = true;
        Debug.Log("DEATH STARTED");

        // 1. Block player movement/input
        BlockPlayerMovement();

        // 2. Fade-in screen
        if (deathFadeImage != null)
        {
            float fadeTime = 2f;
            float t = 0f;

            while (t < fadeTime)
            {
                t += Time.deltaTime;
                float alpha = Mathf.Clamp01(t / fadeTime);

                Color c = deathFadeImage.color;
                c.a = alpha;
                deathFadeImage.color = c;

                yield return null;
            }
        }

        // Optional pause before scene load
        yield return new WaitForSeconds(1f);

        // 3. Load Main Menu asynchronously
        string sceneName = "Main Menu";
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' cannot be found. Check Build Settings!");
        }
    }

        private void BlockPlayerMovement()
    {
        if (GAME.Player == null) return;

        // Disable health reaction
        var healthReaction = GAME.Player.GetComponent<PlayerHealthReaction>();
        if (healthReaction != null)
            healthReaction.enabled = false;

        // Disable movement scripts
        var movement = GAME.Player.GetComponent<PlayerMovement>();
        if (movement != null)
            movement.enabled = false;

        // Stop Rigidbody movement immediately
        Rigidbody2D rb = GAME.Player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        Debug.Log("Player movement + PlayerHealthReaction disabled.");
    }


    // Optional: play camera shake on damage
    public void DamageResult()
    {
        GAME.Player.GetComponent<PlayerInRooms>()?.PlayCameraShake(0.2f);
    }
}
