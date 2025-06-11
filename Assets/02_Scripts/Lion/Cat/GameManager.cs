using System;
using TMPro;
using UnityEngine;

namespace CatGame
{
    
public class GameManager : MonoBehaviour
{
    public GameObject[] IntroUI;
    public GameObject[] PlayScene;
    public GameObject GameOverUI;
    public TextMeshProUGUI playTime;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI maxScoreText;
    public Transform_LoopMap spawner;

    public int levelUpPoint;
    public int score;
    private int maxScore;
    private float timer;
    private bool isStarted;

    private void Start()
    {
        init();
    }

    void init()
    {
        GameOverUI.SetActive(false);
        spawner.init();
        score = 0;
        timer = 0;
        levelUpPoint = 5;
        
        foreach (var ui in PlayScene)
        {
            ui.SetActive(false);
        }
        foreach (var ui in IntroUI)
        {
            ui.SetActive(true);
        }
    }
    void Update()
    {
        if (isStarted)
        {
            timer += Time.deltaTime;
            playTime.text = timer.ToString("0.00");
            
            scoreText.text = score.ToString("X 0");
            if (levelUpPoint <= 0)
            {
                levelUpPoint = 5;
                spawner.moveSpeed *= 1.8f;
            }
        }
    }

    public void StartGame()
    {
        GameOverUI.SetActive(false);
        spawner.init();
        isStarted = true;
        foreach (var ui in PlayScene)
        {
            ui.SetActive(true);
        }
        foreach (var ui in IntroUI)
        {
            ui.SetActive(false);
        }
    }
    public void GameOver()
    {
        GameOverUI.SetActive(true);
        isStarted = false;
        if (maxScore < score)
        {
            maxScore = score;
        }
        maxScoreText.text = maxScore.ToString("최고점수 : 0");
        Invoke(nameof(init), 2f);
    }
}
}
