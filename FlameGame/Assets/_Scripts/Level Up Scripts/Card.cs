using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardImageRenderer;
    [SerializeField] TextMeshPro cardTextRenderer;
    private CardSO cardInfo;

    public void Setup(CardSO card)
    {
        if (card == null)
        {
            cardInfo = null;
            cardImageRenderer.sprite = null;
            cardTextRenderer.text = "";
            return;
        }

        cardInfo = card;
        cardImageRenderer.sprite = card.cardImage;
        cardTextRenderer.text = card.cardText;
    }

    void OnMouseDown()
    {
        if (cardInfo == null)
        {
            Debug.Log("Blank card clicked. Ignoring.");
            return;
        }

        CardManager.Instance.SelectCard(cardInfo);
    }
}