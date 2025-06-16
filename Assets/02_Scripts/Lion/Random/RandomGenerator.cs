using System;
using System.Collections;
using System.Collections.Generic;
using Poker;
using UnityEngine;

namespace Poker
{
    public class RandomGenerator : MonoBehaviour
    {
        public List<Card> myCards;
        public int[] RandomDeck = new int[52];
        public bool[] ListChecker = new bool[52];
        

        private void Awake()
        {
            for (int i = 0; i < 52; i++)
            {
                RandomDeck[i] = i;
            }
        }

        void Start()
        {
            StartCoroutine(nameof(ShuffleCoroutine));
        }

        void Shuffle()
        {
            Array.Clear(ListChecker, 0, ListChecker.Length);
            for (int i = 0; i < 5; i++)
            {
                int index;
                do
                {
                    index = UnityEngine.Random.Range(0, 52);
                } while (ListChecker[index]);

                ListChecker[index] = true;
                myCards[i].cardData = PokerManager.instance.Deck[RandomDeck[index]];
            }
            // Debug.Log($"[{myCards[0]}] [{myCards[1]}] [{myCards[2]}] [{myCards[3]}] [{myCards[4]}]");
        }

        IEnumerator ShuffleCoroutine()
        {
            while (true)
            {
                Shuffle();
                foreach (Card card in myCards)
                {
                    card.Flip();
                    yield return new WaitForSeconds(0.1f);
                }
                HandRank currentRank = PokerManager.instance.handChecker.CheckHand(myCards);
                PokerManager.instance.playerScores[(int)currentRank] += 1;
                PokerManager.instance.uiManager.updateScores(currentRank);
                if (currentRank >= PokerManager.instance.TopRank)
                {
                    PokerManager.instance.TopRank = currentRank;
                    PokerManager.instance.StartCoroutine("ChangeTopHand",myCards);
                }
                PokerManager.instance.handOpenCount++;
                Debug.Log(currentRank);
                yield return new WaitForSeconds(1f);
                
                for (int i = 4; i >= 0; i--)
                {
                    myCards[i].Flip();
                    yield return new WaitForSeconds(0.1f);
                }
                
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
