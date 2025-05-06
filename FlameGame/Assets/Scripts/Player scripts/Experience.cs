using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Experience : MonoBehaviour
{
    [SerializeField] private Image expBar;
    [SerializeField] private TMP_Text textLevel;
    [SerializeField] GameObject HitBox;

    GAMEGLOBALMANAGEMENT GAME;

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

            GameManager.Instance.ChangeState(GameManager.GameState.CardSelection); // show cards!
        }
        else
        {
            expBar.transform.localScale = new Vector3(GAME.playerCurrentExperience / GAME.playerExperienceToNextLevel, 1f);
        }
    }
}
