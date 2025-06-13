using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Poker
{
    public class HandCheck : MonoBehaviour
    {
        public HandRank CheckHand(List<Card> hand)
        {
            if (hand.Count  < 5)
            {
                Debug.LogWarning("Hand count is less than 5");
                return HandRank.HighCard;
            }

            var sortedHand = hand.OrderBy(c => (int)c.Rank).ToList();
            
            if (IsFlush(sortedHand)) return HandRank.Flush;
            if (IsStraight(sortedHand)) return HandRank.Straight;
            
            return HandRank.HighCard;
        }
        // Straight flush  : Five cards of the same suit in sequence (if those five are A, K, Q, J, 10; it is a Royal Flush)
        // Four of a kind  : Four cards of the same rank and any one other card
        // Full house      : Three cards of one rank and two of another
        // Flush           : Five cards of the same suit
        // Straight        : Five cards in sequence (for example, 4, 5, 6, 7, 8)
        // Three of a kind : Three cards of the same rank
        // Two pair        : Two cards of one rank and two cards of another
        // One pair        : Two cards of the same rank
        // High card       : If no one has a pair, the highest card wins
        
        /// <summary>
        /// Flush           : Five cards of the same suit
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        bool IsFlush(List<Card> hand)
        {
            CardSuit suit = hand.First().Suit;
            return hand.All(c => c.Suit == suit);
        }
        
        /// <summary>
        /// Straight        : Five cards in sequence (for example, 4, 5, 6, 7, 8)
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        bool IsStraight(List<Card> hand)
        {
            // Back Straight!!
            if (hand[0].Rank == CardRank.Two   &&
                hand[1].Rank == CardRank.Three &&
                hand[2].Rank == CardRank.Four  &&
                hand[3].Rank == CardRank.Five  &&
                hand[4].Rank == CardRank.Ace)
            {
                return true;
            }
            
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Rank == hand[i-1].Rank + 1)
                {
                    return false;
                }
            }
            
            return true;
        }
    }

    public enum HandRank
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush
    }
}
