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
    [SerializeField] private CardSO blankCardSO; // ADD THIS

    GameObject cardOne, cardTwo, cardThree;
    List<CardSO> availableCards = new List<CardSO>();

    public List<CardSO> alreadySelectedCards = new List<CardSO>();
    public static CardManager Instance;

    private bool isSpinning = false;
    private float spinSpeed = 100f;
    private float slowDownTime = 2f;
    private float moveThreshold = 50f;


    GAMEGLOBALMANAGEMENT GAME;

    void Awake()
    {
        Instance = this;

        GAME = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GAMEGLOBALMANAGEMENT>();

        if (GameManager.Instance != null)
            GameManager.Instance.OnStateChanged += HandleGameStateChanged;
    }

    private void Update()
    {
        if(isSpinning)
        {
            //ostrze¿enie mnie wkurza³o wiêc napisa³em to if
        }
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
            StartSpin();
        }
    }

    void StartSpin()
    {
        isSpinning = true;

        // DESTROY old blank cards first
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        // Then instantiate the blank cards
        InstantiateBlankCards();

        StartCoroutine(SpinCards());
    }

    void InstantiateBlankCards()
    {
        // Instantiate blank card prefabs
        cardOne = Instantiate(cardPrefab, cardPositionOne.position, Quaternion.identity, cardPositionOne);
        cardTwo = Instantiate(cardPrefab, cardPositionTwo.position, Quaternion.identity, cardPositionTwo);
        cardThree = Instantiate(cardPrefab, cardPositionThree.position, Quaternion.identity, cardPositionThree);

        // Setup them as blank
        cardOne.GetComponent<Card>().Setup(blankCardSO);
        cardTwo.GetComponent<Card>().Setup(blankCardSO);
        cardThree.GetComponent<Card>().Setup(blankCardSO);
    }

    IEnumerator SpinCards()
    {
        yield return new WaitForSecondsRealtime(0.15f);

        isSpinning = true;
        float elapsed = 0f;

        while (elapsed < slowDownTime)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / slowDownTime;
            t = t * t * (3f - 2f * t);  // Smoothstep

            float moveSpeed = Mathf.Lerp(spinSpeed, 0, t);

            MoveCard(cardOne, cardPositionOne, moveSpeed);
            MoveCard(cardTwo, cardPositionTwo, moveSpeed);
            MoveCard(cardThree, cardPositionThree, moveSpeed);

            yield return null;
        }

        isSpinning = false;
        SnapCards();
        RandomizeNewCards();  // AFTER spin ends -> pick real cards
    }

    void MoveCard(GameObject cardObj, Transform startPosition, float speed)
    {
        if (cardObj == null) return;

        cardObj.transform.Translate(Vector3.up * speed * Time.unscaledDeltaTime);

        if (cardObj.transform.position.y >= startPosition.position.y + moveThreshold)
        {
            cardObj.transform.position = startPosition.position - new Vector3(0, moveThreshold, 0);
        }
    }

    void SnapCards()
    {
        cardOne.transform.position = cardPositionOne.position;
        cardTwo.transform.position = cardPositionTwo.position;
        cardThree.transform.position = cardPositionThree.position;
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
            Debug.LogWarning("Not enough available cards to spin!");
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

        // Finally, assign real cards after spin
        cardOne.GetComponent<Card>().Setup(selectedCards[0]);
        cardTwo.GetComponent<Card>().Setup(selectedCards[1]);
        cardThree.GetComponent<Card>().Setup(selectedCards[2]);
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
        PlayerDeath health = GAME.Player.transform.Find("Hitbox").GetComponent<PlayerDeath>();

        switch (selectedCard.effectType)
        {
            case CardEffect.Damage:
                GAME.playerBasicAttackDamage += selectedCard.effectValue;
                break;
            case CardEffect.Health:
                health.playerHealth = Mathf.Min(health.playerHealth + selectedCard.effectValue, health.playerMaxHealth);
                break;
            case CardEffect.Reload:
                GAME.playerBasicAttackCooldown = Mathf.Max(GAME.playerBasicAttackCooldown - selectedCard.effectValue, 0.2f);
                break;
        }
            
        
    }

    public void ShowCardSelection()
    {
        cardSelectionUI.SetActive(true);
        StartSpin();
        Time.timeScale = 0;
    }

    public void HideCardSelection()
    {
        cardSelectionUI.SetActive(false);
        Time.timeScale = 1;
    }
}