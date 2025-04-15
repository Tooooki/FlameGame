using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardSelectionUI;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardPositionOne;
    [SerializeField] Transform cardPositionTwo;
    [SerializeField] Transform cardPositionThree;
    [SerializeField] List<CardSO> deck;

    GameObject cardOne, cardTwo, cardThree;

    public List<CardSO> alreadySelectedCards = new List<CardSO>();

    public static CardManager Instance;


    void Awake()
    {
        Instance = this;

        if(GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += HandleGameStateChanged;
    }

    void OnDisable()
    {
         if(GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
    }
    
    
    public void HandleGameStateChanged(GameManager.GameState state)
    {
        if(state == GameManager.GameState.CardSelection)
        {
            RandomizeNewCards();
        }
    }
    

    void RandomizeNewCards()
    {
        if(cardOne != null) Destroy(cardOne);
        if(cardTwo != null) Destroy(cardTwo);
        if(cardThree != null) Destroy(cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();

        List<CardSO> availableCards = new List<CardSO>(deck);
        availableCards.RemoveAll(card => 
            card.isUnique && alreadySelectedCards.Contains(card) ||
            card.unlockLevel > GameManager.Instance.GetCurrentLevel() //to jest jesli dodamy prog levelowy dla niektorych kart
        );

        if(availableCards.Count < 3)
        {
            Debug.Log("Brakuje dostępnych kart");
            return;
        }

        while (randomizedCards.Count < 3)
        {
            CardSO randomCard = availableCards[Random.Range(0, availableCards.Count)];
            if(!randomizedCards.Contains(randomCard))
            {
                randomizedCards.Add(randomCard);
            }
        }

        cardOne = InstantiateCard(randomizedCards[0], cardPositionOne);
        cardTwo = InstantiateCard(randomizedCards[1], cardPositionTwo);
        cardThree = InstantiateCard(randomizedCards[2], cardPositionThree);
    }

    GameObject InstantiateCard(CardSO cardSO, Transform position)
    {
        GameObject cardGo = Instantiate(cardPrefab, position.position, Quaternion.identity, position);
        Card card = cardGo.GetComponent<Card>();
        card.Setup(cardSO);
        return cardGo;
    } 

    public void SelectCard(CardSO selectedCard)
    {
        if(!alreadySelectedCards.Contains(selectedCard))
        {
            alreadySelectedCards.Add(selectedCard);
            OnSelect(selectedCard);
        }
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }

public void OnSelect(CardSO selectedCard)
{
    GameObject playerGO = GameObject.FindWithTag("Player"); // Find the player GameObject

    if (playerGO != null)
    {
        PlayerStats playerStats = playerGO.GetComponent<PlayerStats>(); // Get the PlayerStats component from the Player
        GameObject hitbox = playerGO.transform.Find("Hitbox").gameObject; // Find the Hitbox GameObject
        PlayerDeath playerHealth = hitbox.GetComponent<PlayerDeath>(); // Get the PlayerDeath component from Hitbox

        if (playerStats != null && playerHealth != null)
        {
            switch (selectedCard.effectType)
            {
                case CardEffect.Damage:
                    playerStats.BulletDamage += selectedCard.effectValue;  // Increase the player's bullet damage
                    Debug.Log($"Increased Bullet Damage to {playerStats.BulletDamage}");
                    break;

                case CardEffect.Health:
                    playerHealth.playerHealth += selectedCard.effectValue;  // Increase the player's health
                    if (playerHealth.playerHealth > playerHealth.playerMaxHealth)
                    {
                        playerHealth.playerHealth = playerHealth.playerMaxHealth; // Ensure health doesn't exceed max health
                    }
                    Debug.Log($"Increased Player Health to {playerHealth.playerHealth}");
                    break;

                case CardEffect.Reload:
                    // Handle reload effect if needed (you can add logic for reload if you want)
                    break;
            }
        }
        else
        {
            Debug.LogWarning("PlayerStats or PlayerHealth component not found on Player GameObject or Hitbox GameObject.");
        }
    }
    else
    {
        Debug.LogWarning("Player GameObject not found.");
    }
}
    public void ShowCardSelection()
    {
        cardSelectionUI.SetActive(true);
        Time.timeScale = 0;
    }


    public void HideCardSelection()
    {
        cardSelectionUI.SetActive(false);
        Time.timeScale = 1;
    }

} 
