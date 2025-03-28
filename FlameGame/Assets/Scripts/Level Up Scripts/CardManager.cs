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

    List<CardSO> alreadySelectedCards = new List<CardSO>();

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
        if(alreadySelectedCards.Contains(selectedCard))
        {
            alreadySelectedCards.Add(selectedCard);
        }
        GameManager.Instance.ChangeState(GameManager.GameState.Playing);
    }

    public void ShowCardSelection()
    {
        cardSelectionUI.SetActive(true);
    }


    public void HideCardSelection()
    {
        cardSelectionUI.SetActive(false);
    }

} 
