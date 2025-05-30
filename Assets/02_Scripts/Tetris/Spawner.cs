using System;
using System.Collections.Generic;
using UnityEngine;
using TetrisGame;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace TetrisGame
{
public class Spawner : MonoBehaviour
{
    public List<Block> blocks;
    [SerializeField] private List<int> randomList;
    public List<blockInfo> blockInfos;
    public WallKickDataSO wallKickData;
    public Vector3 spawnPos;
    public Vector2 rowcol;
    public float stopTime;
    
    public bool[,] maps;
    public Transform visualBlockRoot;
    public VisualBlock[,] visualBlocks;
    
    public VisualBlock visualBlock;
    public GameObject DropPointBlock;
    public GameObject[,] debugBlocks;

    [SerializeField] private int rollIdx;

    private void Awake()
    {
        rollIdx = blocks.Count;
        maps = new bool[(int)rowcol.x, (int)rowcol.y];
        visualBlocks = new VisualBlock[(int)rowcol.x, (int)rowcol.y];
        debugBlocks = new GameObject[(int)rowcol.x, (int)rowcol.y];
        for (int i = 0; i < blocks.Count; i++)
        {
            randomList.Add(i);
        }
    }

    void Start()
    {
        mapInit();
        visualMapping();
        
        Invoke(nameof(spawnNextBlock),1f);
    }

    void Update()
    {
        
    }

    void mapInit()
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
        Debug.Log("MAPPING NEW BLOCK");
        
        Material mat = block[0].GetComponent<MeshRenderer>().material;
        foreach (var b in block)
        {
            int x = (int)b.position.x;
            int y = (int)b.position.y;
            if (x < 0 || x >= rowcol.x || y < 0 || y >= rowcol.y) continue;
            maps[x,y] = true;
            visualBlocks[x,y].changeMat(mat);
            visualBlocks[x,y].gameObject.SetActive(true);
        }
        block[0].parent.gameObject.SetActive(false);
        checkClearLine();
    }

    void checkClearLine()
    {
        // check List
        int[] chk = new int[(int)rowcol.y];
        // check clearLine
        for (int j = 0; j < rowcol.y; j++)
        {
            bool clear = true;
            for (int i = 0; i < rowcol.x; i++)
            {
                if (maps[i, j] == false)
                {
                    clear = false;
                    break;
                }
            }
            if (clear) chk[j] = 1;
        }
        // clear line
        for (int j = 0; j < rowcol.y; j++)
        {
            if (chk[j] == 0) continue;
            for (int i = 0; i < rowcol.x; i++)
            {
                maps[i,j] = false;
                visualBlocks[i,j].gameObject.SetActive(false);
            }
        }
        // movedown lines
        int cnt = 0;
        for (int j = 1; j < rowcol.y; j++)
        {
            cnt += chk[j-1];
            for (int i = 0; i < rowcol.x; i++)
            {
                if (maps[i,j] == false) continue;
                maps[i,j] = false;
                visualBlocks[i,j].gameObject.SetActive(false);
                maps[i,j-cnt] = true;
                visualBlocks[i,j-cnt].changeMat(visualBlocks[i,j].renderer.material);
                visualBlocks[i,j-cnt].gameObject.SetActive(true);
            }
        }
        
        spawnNextBlock();
    }
    void visualMapping()
    {
        for (int j = 0; j < rowcol.y; j++)
        {
            for (int i = 0; i < rowcol.x; i++)
            {
                visualBlocks[i,j] = Instantiate(visualBlock, new Vector3(i,j,0), Quaternion.identity, visualBlockRoot);
                visualBlocks[i,j].gameObject.SetActive(false);
            }
        }
    }
    // void debugMapping()
    // {
    //     for (int j = 0; j < rowcol.y; j++)
    //     {
    //         for (int i = 0; i < rowcol.x; i++)
    //         {
    //             debugBlocks[i,j] = Instantiate(visualBlock, new Vector3(i-11,j,0), Quaternion.identity);
    //         }
    //     }
    // }

    public string getRotationString(int id,int idx)
    {
        if (id > blockInfos.Count || idx >= blockInfos[id].blockRot.Length)
        {
            return null;
        }
        return blockInfos[id].blockRot[idx];
    }

    public List<Vector2Int> getWallKicksData(int before, int after, bool isIBlock = false)
    {
        // [0 = 0, R = 1, 2 = 2 ,L = 3]
        int idx = 0;
        for (int i = 0; i < wallKickData.WallKicks_Keys.Count; i++)
        {
            if (wallKickData.WallKicks_Keys[i].x == before && wallKickData.WallKicks_Keys[i].y == after)
            {
                idx = i;
                break;
            }
        }

        if (isIBlock)
        {
            return wallKickData.I_WallKicks[idx].kicks;
        }
        else
        {
            return wallKickData.JLSZT_WallKicks[idx].kicks;
        }
    }
    
    public void spawnNextBlock()
    {
        int n = Random.Range(0, rollIdx);
        int idx = randomList[n];
        blocks[idx].transform.position = spawnPos;
        blocks[idx].spawner = this;
        blocks[idx].init();
        blocks[idx].gameObject.SetActive(true);
        
        Debug.Log($"{blocks[idx].gameObject.name} Block Spawned");
        
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
