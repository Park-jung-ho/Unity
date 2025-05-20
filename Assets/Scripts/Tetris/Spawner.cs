using System;
using System.Collections.Generic;
using UnityEngine;
using TetrisGame;
using Random = UnityEngine.Random;

namespace TetrisGame
{
public class Spawner : MonoBehaviour
{
    public List<GameObject> blocks;
    [SerializeField] private List<int> randomList;
    public List<blockInfo> blockInfos;
    public Vector3 spawnPos;
    public Vector2 rowcol;
    public bool[,] maps;
    public float stopTime;
    public GameObject debugBlock;
    public GameObject[,] debugBlocks;

    [SerializeField] private int rollIdx;

    private void Awake()
    {
        rollIdx = blocks.Count;
        maps = new bool[(int)rowcol.x, (int)rowcol.y];
        debugBlocks = new GameObject[(int)rowcol.x, (int)rowcol.y];
        for (int i = 0; i < blocks.Count; i++)
        {
            randomList.Add(i);
        }
    }

    void Start()
    {
        mappInit();
        debugMapping();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnNextBlock();
        }
    }

    void mappInit()
    {
        for (int i = 0; i < rowcol.x; i++)
        {
            for (int j = 0; j < rowcol.y; j++)
            {
                maps[i,j] = false;
            }
        }
    }

    public bool canMove(int x, int y)
    {
        if (x < 0 || x >= rowcol.x || y < 0 || y >= rowcol.y) return false;
        if (maps[x,y]) return false;
        
        return true;
    }

    public void mappingNewBlock(Transform[] block)
    {
        foreach (var b in block)
        {
            int x = (int)b.position.x;
            int y = (int)b.position.y;
            if (x < 0 || x >= rowcol.x || y < 0 || y >= rowcol.y) continue;
            maps[x,y] = true;
            debugBlocks[x,y].GetComponent<BoardCheckBlock>().changeColor(1);
        }
    }
    void debugMapping()
    {
        for (int j = 0; j < rowcol.y; j++)
        {
            for (int i = 0; i < rowcol.x; i++)
            {
                debugBlocks[i,j] = Instantiate(debugBlock, new Vector3(i-11,j,0), Quaternion.identity);
            }
        }
    }

    public string getRotationString(int id,int idx)
    {
        if (id > blockInfos.Count || idx >= blockInfos[id].blockRot.Length)
        {
            return null;
        }
        return blockInfos[id].blockRot[idx];
    }
    
    public void spawnNextBlock()
    {
        int n = Random.Range(0, rollIdx);
        int idx = randomList[n];
        GameObject newBlock = Instantiate(blocks[idx], spawnPos, Quaternion.identity);
        Block newB = newBlock.GetComponent<Block>();
        newB.spawner = this;
        randomList[n] = randomList[rollIdx-1];
        randomList[rollIdx - 1] = idx;
        rollIdx--;
        
        if (rollIdx == 0)
        {
            rollIdx = blocks.Count;
        }
    }
}
    
}
