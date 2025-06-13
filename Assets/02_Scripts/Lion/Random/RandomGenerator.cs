using System;
using System.Collections;
using Poker;
using UnityEngine;

namespace Poker
{
    public class RandomGenerator : MonoBehaviour
    {
        public Card[] myCards = new Card[5];
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
                myCards[i].frontSprite = PokerManager.instance.Deck[RandomDeck[index]].frontSprite;
            }
            // Debug.Log($"[{myCards[0]}] [{myCards[1]}] [{myCards[2]}] [{myCards[3]}] [{myCards[4]}]");
        }

        IEnumerator ShuffleCoroutine()
        {
            while (true)
            {
                Shuffle();
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
