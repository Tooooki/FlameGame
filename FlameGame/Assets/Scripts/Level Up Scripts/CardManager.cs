using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardSelectionUI;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardPostionOne;
    [SerializeField] Transform cardPostionTwo;
    [SerializeField] Transform cardPostionThree;
    [SerializeField] List<CardSO> deck;

    GameObject cardOne, cardTwo, cardThree;

    List<CardSO> alreadySelectedCards = new List<CardSO>();
    void RandomizeNewCards()
    {
        if(cardOne != null) Destroy(cardOne);
        if(cardTwo != null) Destroy(cardTwo);
        if(cardThree != null) Destroy(cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();

        List<CardSO> availableCards = new List<CardSO>();
        availableCards.RemoveAll(cardOne => 
            cardOne.isUnique && alreadySelectedCards.Contains(card) 
            // || card.unlockLevel > GameManager.Instance.GetCurrentLevel() to jest jesli dodamy prog levelowy dla niektorych kart
        );

        if(availableCards.Count < 3)
        {
            Debug.Log("Brakuje dostępnych kart");
            return;
        }

        while (randomizedCards.count < 3)
        {
            CardSO randomCard = availableCards[Random.Range(0, availableCards.Count)];
            if(!alreadySelectedCards.Contains(randomCard))
            {
                randomizedCards.Add(randomCard);
            }
        }

        cardOne = InstantiateCard(randomizedCards[0], cardPositionOne);
        cardTwo = InstantiateCard(randomizedCards[1], cardPositionOne);
        cardThree = InstantiateCard(randomizedCards[2], cardPositionOne);
    }

    GameObject InstantiateCard(CardSO cardSO, Transform position)
    {
        GameObject card = Instantiate(cardPrefab, position.positon, Quaternion.identity, position);
        Card card = cardGo.GetComponent<Card>();
        card.Setup(cardSO);
        return cardGo;

    }
} 
