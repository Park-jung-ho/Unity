using Poker;
using UnityEngine;

[CreateAssetMenu(fileName = "pokerCardData", menuName = "Poker/pokerCardData")]
public class pokerCardData : ScriptableObject
{
    public Sprite frontSprite;
    public CardSuit suit;
    public CardRank rank;
    // public Color color;
}

public enum CardSuit
{
    Clubs,
    Hearts,
    Diamonds,
    Spades    
}

public enum CardRank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 14
}