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

            bool isFlush = IsFlush(sortedHand);
            bool isStraight = IsStraight(sortedHand);
            
            if (isFlush && isStraight && sortedHand[0].Rank == CardRank.Ten) return HandRank.RoyalFlush;
            if (isFlush && isStraight) return HandRank.StraightFlush;
            if (IsFourofAKind(sortedHand)) return HandRank.FourOfAKind;
            if (IsFullHouse(sortedHand)) return HandRank.FullHouse;
            if (isFlush) return HandRank.Flush;
            if (isStraight) return HandRank.Straight;
            if (IsThreeOfAKind(sortedHand)) return HandRank.ThreeOfAKind;
            if (IsTwoPair(sortedHand)) return HandRank.TwoPair;
            if (IsOnePair(sortedHand)) return HandRank.OnePair;
            
            return HandRank.HighCard;
        }
        
        bool IsFlush(List<Card> hand)
        {
            CardSuit suit = hand.First().Suit;
            return hand.All(c => c.Suit == suit);
        }
        
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
            
            for (int i = 1; i < hand.Count; i++)
            {
                if (hand[i].Rank != hand[i-1].Rank + 1)
                {
                    return false;
                }
            }
            
            return true;
        }

        bool IsFourofAKind(List<Card> hand)
        {
            var groups = hand.GroupBy(c => c.Rank).ToList();
            return groups.Any(g => g.Count() == 4);
        }

        bool IsFullHouse(List<Card> hand)
        {
            var groups = hand.GroupBy(c => c.Rank).ToList();
            return groups.Count == 2 &&
                   groups.Any(g => g.Count() == 3) &&
                   groups.Any(g => g.Count() == 2);
        }

        bool IsThreeOfAKind(List<Card> hand)
        {
            var groups = hand.GroupBy(c => c.Rank).ToList();
            return groups.Any(g => g.Count() == 3);
        }
        bool IsTwoPair(List<Card> hand)
        {
            var groups = hand.GroupBy(c => c.Rank).ToList();
            return groups.Count(g => g.Count() == 2) == 2;
        }

        bool IsOnePair(List<Card> hand)
        {
            var groups = hand.GroupBy(c => c.Rank).ToList();
            return groups.Count(g => g.Count() == 2) == 1;
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
