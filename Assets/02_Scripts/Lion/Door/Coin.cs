using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // LionStudy.Player.coinCount++;
            Debug.Log($"1 코인 획득!! 총 코인 {++LionStudy.Player.coinCount}");
            gameObject.SetActive(false);
        }
    }
}
