using System;
using TMPro;
using UnityEngine;

namespace CatGame
{
    
public class GameManager : MonoBehaviour
{
    public GameObject[] IntroUI;
    public GameObject GameOverUI;
    public GameObject PlayScene;
    public TextMeshProUGUI playTime;
    public Transform_LoopMap spawner;

    private float timer;
    private bool isStarted;

    private void Start()
    {
    }

    void init()
    {
        GameOverUI.SetActive(false);
        PlayScene.SetActive(false);
        spawner.init();
        
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
        }
    }

    public void StartGame()
    {
        GameOverUI.SetActive(false);
        PlayScene.SetActive(true);
        spawner.init();
        isStarted = true;
        foreach (var ui in IntroUI)
        {
            ui.SetActive(false);
        }
    }
    public void GameOver()
    {
        GameOverUI.SetActive(true);
        isStarted = false;  
        Invoke(nameof(init), 2f);
    }
}
}
