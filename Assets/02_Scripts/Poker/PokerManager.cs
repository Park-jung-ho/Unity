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
    public UIManager uiManager;
    
    public pokerCardData[] Deck;

    public List<Card> TopHand;
    public HandRank TopRank;
    public List<int> playerScores;
    // public Card card;
    public int handOpenCount;

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
        Application.targetFrameRate = 30;
        Application.runInBackground = true;
        QualitySettings.vSyncCount = 0;
    }
    //
    // public void CheckPlayerHand()
    // {
    //     
    // }

    IEnumerator ChangeTopHand(List<Card> cards)
    {
        if (TopHand[0].isFront)
        {
            foreach (var card in TopHand)
            {
                card.Flip();
                yield return new WaitForSeconds(0.05f);
            }
        }
        
        for (int i = 0; i < cards.Count; i++)
        {
            TopHand[i].cardData = cards[i].cardData;
        }
        yield return new WaitForSeconds(0.25f);
        foreach (var card in TopHand)
        {
            card.Flip();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
