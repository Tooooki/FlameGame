using UnityEngine;
[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public Sprite cardImage; //obraz na karcie
    public string cardText; //tekst na karcie
    public CardEffect effectType; //Efekty kart
    public float effectValue; //ile np. 10% damage increase
    public bool isUnique; //Jesli abilitka ma sie pojawiac tylko raz
    public int unlockLevel;//
}

public enum CardEffect
{
    Powerup1
}

