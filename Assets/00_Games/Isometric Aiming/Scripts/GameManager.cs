using System;
using TMPro;
using UnityEngine;
using IsometricAiming;

namespace IsometricAiming
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public TMP_Text TimerText;
        public TMP_Text scoreText;
        public GameObject ArcheryRoot;
        public int archeryCount;
        public float limitTime;
        public float getTime;
        public GameObject MainUI;
        
        public bool isStarted { get; private set; }
        private float timer;
        private int score;
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

            timer = limitTime;
        }

        private void Start()
        {
        }

        void Update()
        {
            if (!isStarted) return;
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Debug.Log("Game Over");
                StopGame();
                timer = 0;
            }
            TimerText.text = string.Format("Time Left : {0}", timer.ToString("F2"));
        }

        public void getScore()
        {
            timer += getTime;
            score++;
            scoreText.text = string.Format("Score : {0}",score.ToString());
        }

        public void StartGame()
        {
            ArcheryRoot.SetActive(true);
            timer = limitTime;
            score = 0;
            getScore();
            MainUI.SetActive(false);
            isStarted = true;
        }

        public void StopGame()
        {
            isStarted = false;
            MainUI.SetActive(true);
            ArcheryRoot.SetActive(false);
            MainUI.SetActive(true);
        }
    }
}