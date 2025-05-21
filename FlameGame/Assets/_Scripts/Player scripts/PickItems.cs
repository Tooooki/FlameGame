using Unity.VisualScripting;
using UnityEngine;

public class PickItems : MonoBehaviour
{
    GAMEGLOBALMANAGEMENT GAME;
    private void Awake()
    {
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Healing"))
        {
            if (GAME.playerMaxHealth - GAME.playerCurrentHealth >= GAME.waxPotionHealingValue && GAME.playerMaxHealth - GAME.playerCurrentHealth < GAME.playerMaxHealth)
            {
                GAME.playerCurrentHealth += GAME.waxPotionHealingValue;
                Destroy(collision.gameObject);
            }
            else if (GAME.playerMaxHealth - GAME.playerCurrentHealth < GAME.waxPotionHealingValue)
            {
                GAME.playerCurrentHealth = GAME.playerMaxHealth;
                Destroy(collision.gameObject);
            }
        }

        if(collision.CompareTag("Exp"))
        {
            GAME.PlayerGetExperience(GAME.expCrystalExperienceValue);
            Destroy(collision.gameObject);
        }
    }
}
