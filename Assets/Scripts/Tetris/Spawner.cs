using System.Collections.Generic;
using UnityEngine;
using TetrisGame;

namespace TetrisGame
{
public class Spawner : MonoBehaviour
{
    public List<GameObject> blocks;
    public Vector3 spawnPos;

    [SerializeField] private int rollIdx;
    void Start()
    {
        rollIdx = blocks.Count;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnNextBlock();
        }
    }

    public void spawnNextBlock()
    {
        int idx = Random.Range(0, rollIdx);
        GameObject newBlock = Instantiate(blocks[idx], spawnPos, Quaternion.identity);
        
        blocks[idx] = blocks[rollIdx-1];
        blocks[rollIdx-1] = newBlock;
        rollIdx--;

        if (rollIdx == 0)
        {
            rollIdx = blocks.Count;
        }
    }
}
    
}
