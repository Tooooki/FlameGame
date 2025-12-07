using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [Header("Player Settings")]
    public float passiveDegeneration = 0.1f;

    [Header("UI Elements")]
    [SerializeField] private Image deathFadeImage;
    [SerializeField] private Image healthBar;

    private GAMEGLOBALMANAGEMENT GAME;
    private bool isDead = false;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager")
            .GetComponent<GAMEGLOBALMANAGEMENT>();

        // Start with full health
        GAME.playerCurrentHealth = GAME.playerMaxHealth;

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
        // Update health bar safely
        if (healthBar != null)
        {
            float healthNormalized = Mathf.Clamp(GAME.playerCurrentHealth / GAME.playerMaxHealth, 0f, 1f);
            healthBar.transform.localScale = new Vector3(healthNormalized, 1f, 1f);
        }

        // Trigger death
        if (!isDead && GAME.playerCurrentHealth <= 0)
        {
            StartCoroutine(PlayerDeathSequence());
        }
    }

        private void PassiveDegeneration()
    {
        GAME.playerCurrentHealth -= passiveDegeneration;
        GAME.playerCurrentHealth = Mathf.Max(GAME.playerCurrentHealth, 0);
        
        // Do NOT call DamageResult() here!
    }


    private IEnumerator PlayerDeathSequence()
    {
        isDead = true;
        Debug.Log("DEATH STARTED");

        BlockPlayerMovement();

        // Fade to black
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

        yield return new WaitForSeconds(1f);

        string sceneName = "Main Menu";
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncLoad.isDone) yield return null;
        }
        else
        {
            Debug.LogError($"Scene '{sceneName}' not found in Build Settings!");
        }
    }

    private void BlockPlayerMovement()
    {
        if (GAME.Player == null) return;

        // Disable health reaction
        var healthReaction = GAME.Player.GetComponent<PlayerHealthReaction>();
        if (healthReaction != null) healthReaction.enabled = false;

        // Disable movement
        var movement = GAME.Player.GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        // Disable attack
        var attack = GAME.Player.GetComponent<PlayerAttack>();
        if (attack != null) attack.enabled = false;

        // Disable dash
        var dash = GAME.Player.GetComponent<Dash>();
        if (dash != null) dash.enabled = false;

        // Freeze Rigidbody
        Rigidbody2D rb = GAME.Player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // Mute all SFX
        if (GAME.audioManager != null && GAME.audioManager.sfxSource != null)
        {
            GAME.audioManager.sfxSource.volume = 0f;
        }

        Debug.Log("Player frozen, attacks/dash disabled, walking sound muted.");
    }

    public void DamageResult()
    {
        if (isDead) return;

        GAME.Player.GetComponent<PlayerInRooms>()?.PlayCameraShake(0.2f);
    }
}
