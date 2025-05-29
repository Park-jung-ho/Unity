using System;
using UnityEngine;

public class Pinball : MonoBehaviour
{
    public PinballManager manager;
    private void OnCollisionEnter2D(Collision2D other)
    {
        int score = 0;
        switch (other.gameObject.tag)
        {
            case "score10":
                score = 10;
                break;
            case "score30":
                score = 30;
                break;
            case "score50":
                score = 50;
                break;
            default:
                return;
        }
        
        manager.score += score;
        Debug.Log($"Add score : {score}\n\t Total Score : {manager.score}");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("gameover"))
        {
            Debug.Log($"<color=red>Game over!</color>\nYour score: {manager.score}");
            manager.GameOver();
        }
    }
}
