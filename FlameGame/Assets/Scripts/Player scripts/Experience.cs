using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    [SerializeField] private Image expBar;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] GameObject HitBox;

    PlayerDeath healthScript;

    GAMEGLOBALMANAGEMENT GAME;

    void Start()
    {
        healthScript = HitBox.GetComponent<PlayerDeath>();
    }

    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }

    void Update()
    {
        textLevel.SetText(GAME.playerLevel.ToString());
        if (GAME.playerCurrentExperience >= GAME.playerExperienceToNextLevel)
        {
            GAME.playerLevel++;
            GAME.playerCurrentExperience -= GAME.playerExperienceToNextLevel;
            GAME.playerExperienceToNextLevel = 100 + (GAME.playerLevel * 50);
            healthScript.playerMaxHealth = healthScript.playerMaxHealth + 20;
            healthScript.playerHealth = healthScript.playerMaxHealth;

            GameManager.Instance.ChangeState(GameManager.GameState.CardSelection); // show cards!
        }
        else
        {
            expBar.transform.localScale = new Vector3(GAME.playerCurrentExperience / GAME.playerExperienceToNextLevel, 1f);
        }
    }
}
