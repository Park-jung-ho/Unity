using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Poker;

public class PokerManager : MonoBehaviour
{
    public static PokerManager instance;
    [Header("Poker Managers")]
    public RandomGenerator randomGenerator;
    public HandCheck handChecker;
    
    public pokerCardData[] Deck;
    
    public List<Card> playerHand;
    public Card card;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(deckCoroutine());
    }
    
    void Update()
    {
        
    }

    public void CheckPlayerHand()
    {
        handChecker.CheckHand(playerHand);
    }

    IEnumerator deckCoroutine()
    {
        while (true)
        {
            foreach (var c in Deck)
            {
                card.frontSprite = c.frontSprite;
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
