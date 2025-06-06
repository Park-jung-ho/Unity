using System;
using UnityEngine;

namespace CatGame
{
    
public class GameManager : MonoBehaviour
{
    public GameObject[] IntroUI;
    public GameObject GameOverUI;
    public GameObject PlayScene;
    public Transform_LoopMap spawner;

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
        
    }

    public void StartGame()
    {
        GameOverUI.SetActive(false);
        PlayScene.SetActive(true);
        spawner.init();
        
        foreach (var ui in IntroUI)
        {
            ui.SetActive(false);
        }
    }
    public void GameOver()
    {
        GameOverUI.SetActive(true);
        Invoke(nameof(init), 2f);
    }
}
}
