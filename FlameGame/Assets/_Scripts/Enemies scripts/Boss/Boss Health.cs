using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] GameObject hpBar;   // World-space bar
    public float bossHP;
    public float bossMaxHP = 500f;

    public Slider bossSlider;              // assign in inspector
    private GAMEGLOBALMANAGEMENT GAME;

    private bool phaseTwoActivated = false;

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
        bossHP = bossMaxHP;

        if (bossSlider != null)
            bossSlider.gameObject.SetActive(false); // hidden initially
    }

    private void OnEnable()
    {
        if (bossSlider != null)
        {
            bossSlider.gameObject.SetActive(true);
            bossSlider.maxValue = bossMaxHP;
            bossSlider.value = bossHP;
        }
    }

    private void Update()
    {
        // Update world-space hpBar
        if (hpBar != null)
            hpBar.transform.localScale = new Vector3(bossHP / bossMaxHP, 1, 1);

        // Update UI slider
        if (bossSlider != null)
            bossSlider.value = bossHP;

        // Check for phase two trigger
        if (!phaseTwoActivated && bossHP <= bossMaxHP / 2f)
        {
            ActivatePhaseTwo();
        }

        if (bossHP <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        bossHP -= damage;
        if (bossHP < 0) bossHP = 0;
    }

        private void ActivatePhaseTwo()
    {
        phaseTwoActivated = true;

        var basicAttack = GetComponent<BasicAttack>();
        if (basicAttack != null)
        {
            basicAttack.StopAttack();   // stops InvokeRepeating
            basicAttack.enabled = false;
        }
        // Disable first phase attack
        var magicSpin = GetComponent<MagicSpin>();
        if (magicSpin != null)
            magicSpin.enabled = false;

        // Enable second phase attack
        var secondPhaseAttack = GetComponent<BossSecondPhaseAttack>();
        if (secondPhaseAttack != null)
            secondPhaseAttack.enabled = true;

        Debug.Log("Phase Two Activated!");
    }

    private void Die()
    {
        GAME.PlayerGetExperience(GAME.enemyAssassinExperienceDrop);

        // Hide the boss slider
        if (bossSlider != null)
            bossSlider.gameObject.SetActive(false);

        // Destroy all active boss projectiles
        GameObject[] bossProjectiles = GameObject.FindGameObjectsWithTag("BossProjectile");
        foreach (GameObject proj in bossProjectiles)
        {
            Destroy(proj);
        }

        // Destroy the boss
        Destroy(gameObject);
    }
}
