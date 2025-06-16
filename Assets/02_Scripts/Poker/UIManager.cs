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
        private float playtime;
        private int RankLength;
        
        private void Start()
        {
            RankLength = Enum.GetValues(typeof(HandRank)).Length -1;
            foreach (var rank in Enum.GetValues(typeof(HandRank)))
            {
                scores[RankLength - (int)rank].text = $"{rank} : {0}";
            }
        }

        private void Update()
        {
            playtime += Time.deltaTime;
            int hours = Mathf.FloorToInt(playtime / 3600);
            int minutes = Mathf.FloorToInt((playtime % 3600) / 60);
            int seconds = Mathf.FloorToInt(playtime % 60);

            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}", hours, minutes, seconds);
            
            playTime.text = formattedTime;
            OpenHand.text = string.Format("Open : {0}", PokerManager.instance.handOpenCount.ToString());
        }

        public void updateScores(HandRank rank)
        {
            int score = PokerManager.instance.playerScores[(int)rank];
            scores[RankLength - (int)rank].text = $"{rank} : {score}";
        }
    }
}