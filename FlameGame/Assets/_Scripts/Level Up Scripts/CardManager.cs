using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardSelectionUI;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardPositionOne;
    [SerializeField] Transform cardPositionTwo;
    [SerializeField] Transform cardPositionThree;
    [SerializeField] List<CardSO> deck;
    [SerializeField] private CardSO blankCardSO;

    private audioManager audioManager;
    GameObject cardOne, cardTwo, cardThree;

    List<CardSO> availableCards = new List<CardSO>();
    public List<CardSO> alreadySelectedCards = new List<CardSO>();
    public static CardManager Instance;

    GAMEGLOBALMANAGEMENT GAME;

    void Awake()
    {
        Instance = this;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audioManager>();
        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += HandleGameStateChanged;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged -= HandleGameStateChanged;
    }

    public void HandleGameStateChanged(GameManager.GameState state)
    {
        if (state == GameManager.GameState.CardSelection)
        {
            ShowCardSelection();
        }
    }

    void InstantiateRealCards(List<CardSO> selectedCards)
    {
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        cardOne = Instantiate(cardPrefab, cardPositionOne.position, Quaternion.identity, cardPositionOne);
        cardTwo = Instantiate(cardPrefab, cardPositionTwo.position, Quaternion.identity, cardPositionTwo);
        cardThree = Instantiate(cardPrefab, cardPositionThree.position, Quaternion.identity, cardPositionThree);

        cardOne.GetComponent<Card>().Setup(selectedCards[0]);
        cardTwo.GetComponent<Card>().Setup(selectedCards[1]);
        cardThree.GetComponent<Card>().Setup(selectedCards[2]);
    }

    void RandomizeNewCards()
    {
        availableCards = new List<CardSO>(deck);
        availableCards.RemoveAll(card =>
            (card.isUnique && alreadySelectedCards.Contains(card)) ||
            card.unlockLevel > GameManager.Instance.GetCurrentLevel()
        );

        if (availableCards.Count < 3)
        {
            Debug.LogWarning("Not enough available cards to select!");
            return;
        }

        List<CardSO> selectedCards = new List<CardSO>();
        while (selectedCards.Count < 3 && availableCards.Count > 0)
        {
            int randomIndex = Random.Range(0, availableCards.Count);
            CardSO selectedCard = availableCards[randomIndex];

            if (!selectedCards.Contains(selectedCard))
            {
                selectedCards.Add(selectedCard);
                availableCards.RemoveAt(randomIndex);
            }
        }

        if (selectedCards.Count < 3)
        {
            Debug.LogError("Still not enough cards selected!");
            return;
        }

        InstantiateRealCards(selectedCards);
    }

    public void SelectCard(CardSO selectedCard)
    {
        if (!alreadySelectedCards.Contains(selectedCard))
        {
            alreadySelectedCards.Add(selectedCard);
            OnSelect(selectedCard);
        }

        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }

    public void OnSelect(CardSO selectedCard)
    {
        switch (selectedCard.effectType)
        {
            case CardEffect.BasicAttackDamage:
                GAME.playerBasicAttackDamage += selectedCard.effectValue;
                break;
            case CardEffect.Heal:
                GAME.playerCurrentHealth = Mathf.Min(GAME.playerCurrentHealth + selectedCard.effectValue, GAME.playerMaxHealth);
                break;
            case CardEffect.BasicAttackCooldown:
                GAME.playerBasicAttackCooldown = Mathf.Max(GAME.playerBasicAttackCooldown * (1 - (selectedCard.effectValue / 100)), 0.2f);
                break;
            case CardEffect.MaxHealth:
                GAME.playerMaxHealth *= 1 + (selectedCard.effectValue / 100);
                break;
            case CardEffect.MoveVelocity:
                GAME.playerMoveVelocity *= 1 + (selectedCard.effectValue / 100);
                break;
            case CardEffect.DashAbility:
                GAME.dashAbility = true;
                break;
            case CardEffect.DashCooldown:
                GAME.playerDashCooldown += selectedCard.effectValue;
                break;
            case CardEffect.DashDuration:
                GAME.playerDashDuration += selectedCard.effectValue;
                break;
            case CardEffect.DashVelocity:
                GAME.playerDashVelocity += selectedCard.effectValue;
                break;
            case CardEffect.BasicAttackVelocity:
                GAME.playerBasicAttackVelocity += selectedCard.effectValue;
                break;
            case CardEffect.Test:
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Rogas = !GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().Rogas;
                break;
        }
    }

    public void ShowCardSelection()
    {
        cardSelectionUI.SetActive(true);
        RandomizeNewCards();
        Time.timeScale = 0;
        audioManager.PlaySFX(audioManager.cardsShowing);
    }

    public void HideCardSelection()
    {
        cardSelectionUI.SetActive(false);
        Time.timeScale = 1;
        audioManager.PlaySFX(audioManager.cardChoosed);
    }
}