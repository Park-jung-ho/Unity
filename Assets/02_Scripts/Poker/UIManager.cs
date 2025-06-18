using System;
using TMPro;
using UnityEngine;

namespace Poker
{
    public class UIManager : MonoBehaviour
    {
        public TMP_Text[] scores;
        public TMP_Text playTime;
        public TMP_Text OpenHand;
        private double playtime;
        private float UIUpdateTimer;
        private int RankLength;
        
        private void Start()
        {
            RankLength = Enum.GetValues(typeof(HandRank)).Length -1;
            foreach (var rank in Enum.GetValues(typeof(HandRank)))
            {
                scores[RankLength - (int)rank].text = $"{rank} : {0}";
            }

            UpdateOpenHandUI();
        }

        private void Update()
        {
            playtime += Time.deltaTime;
            UIUpdateTimer += Time.deltaTime;
            if (UIUpdateTimer >= 1.0f)
            {
                UIUpdateTimer -= 1.0f;
                TimeSpan timeSpan = TimeSpan.FromSeconds(playtime);
                playTime.text = timeSpan.ToString(@"hh\:mm\:ss");
            }
            
        }

        public void UpdateOpenHandUI()
        { 
            OpenHand.text = string.Format("Open : {0}", PokerManager.instance.handOpenCount.ToString());
        }

        public void updateScores(HandRank rank)
        {
            int score = PokerManager.instance.playerScores[(int)rank];
            scores[RankLength - (int)rank].text = $"{rank} : {score}";
        }
    }
}