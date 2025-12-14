using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float bossMaxHP = 500f;
    public float bossHP;

    [Header("UI")]
    public Slider bossSlider; // assigned dynamically at runtime

    [Header("Phase Control")]
    private bool phaseTwoActivated = false;

    private GAMEGLOBALMANAGEMENT GAME;

        private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
        bossHP = bossMaxHP;

        // Only hide if slider is already assigned dynamically
        if (bossSlider != null)
            bossSlider.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        if (bossSlider != null)
        {
            bossSlider.gameObject.SetActive(true); // show when boss spawns
            bossSlider.value = bossHP;
        }
    }

    private void Update()
    {
        if (bossSlider != null)
            bossSlider.value = bossHP;

        if (!phaseTwoActivated && bossHP <= bossMaxHP / 2f)
            ActivatePhaseTwo();

        if (bossHP <= 0)
            Die();
    }

    public void TakeDamage(float damage)
    {
        bossHP -= damage;
        if (bossHP < 0) bossHP = 0;

        if (bossSlider != null)
            bossSlider.value = bossHP;
    }

    private void ActivatePhaseTwo()
    {
        phaseTwoActivated = true;

        var basicAttack = GetComponent<BasicAttack>();
        if (basicAttack != null)
        {
            basicAttack.StopAttack();
            basicAttack.enabled = false;
        }

        var magicSpin = GetComponent<MagicSpin>();
        if (magicSpin != null)
            magicSpin.enabled = false;

        var secondPhaseAttack = GetComponent<BossSecondPhaseAttack>();
        if (secondPhaseAttack != null)
            secondPhaseAttack.enabled = true;

        Debug.Log("Phase Two Activated!");
    }

    private void Die()
    {
        GAME.PlayerGetExperience(GAME.bossExperienceDrop);

        if (bossSlider != null)
            bossSlider.gameObject.SetActive(false);

        // Destroy boss projectiles
        foreach (var proj in GameObject.FindGameObjectsWithTag("BossProjectile"))
            Destroy(proj);

        BossSceneLoader.Instance.LoadMainMenuWithFade();
        Destroy(gameObject);
    }
}
