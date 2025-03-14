using UnityEngine;
using TMPro;
public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardRenderer;
    [SerializeField] TextMeshPro cardTextRenderer;
    private CardSO cardInfo;
    public void Setup(CardSO card)
    {
        cardInfo = card;
        cardImageRenderer.sprite = card.cardImage;
        cardTextRenderer.text = card.cardText;
    }
}
