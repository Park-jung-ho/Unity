using System;
using UnityEngine;

namespace Poker
{
    public class Card : MonoBehaviour
    {
        public Sprite frontSprite;
        public Sprite backSprite;
        
        [Header("Card Data")]
        public pokerCardData cardData;
        public CardSuit Suit => cardData.suit;
        public CardRank Rank => cardData.rank;
        public Sprite CardFront => cardData.frontSprite;
        
        private Animator anim;
        private SpriteRenderer sr;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
        }

        public void TextureChange(int value)
        {
            bool isFront = value == 0 ? true : false;
            
            sr.sprite = isFront ? frontSprite : backSprite;
        }
        
        public void Flip()
        {
            anim.SetTrigger("Flip");
        }
    }
}
